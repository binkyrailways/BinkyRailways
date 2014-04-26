using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinkyRailways.Protocols.Ecos
{
    /// <summary>
    /// ECoS REPLY/EVENT parser
    /// </summary>
    internal static class Parser
    {
        internal static bool TryParse(byte[] buffer, int length, Action<Message> onMessage)
        {
            // Setup reader
            var byteStream = new MemoryStream(buffer, 0, length);
            var reader = new StreamReader(byteStream, Encoding.UTF8);
            string line;
            Message current = null;            

            while ((line = reader.ReadLine()) != null)
            {
                int index;
                line = line.Trim();
                if (line.Length == 0)
                    continue;

                if ((line[0] == '<') && (line[line.Length - 1] == '>'))
                {
                    // Begin or end marker
                    // Strip leading and trailing <>
                    line = line.Substring(1, line.Length - 2).Trim();
                    var parts = line.Split(new[] { ' ' }, 2);
                    if (parts.Length != 2)
                        continue;
                    var verb = parts[0];
                    switch (verb)
                    {
                        case "REPLY":
                            {
                                Command command;
                                var i = 0;
                                if (TryParseCommand(parts[1], ref i, out command) && !SkipSpaces(parts[1], ref i))
                                {
                                    current = new Reply { Command = command };
                                }
                            }
                            break;
                        case "EVENT":
                            {
                                int id;
                                var i = 0;
                                if (TryParseInt(parts[1], ref i, out id) && !SkipSpaces(parts[1], ref i))
                                {
                                    current = new Event { Id = id };
                                }
                            }
                            break;
                        case "END":
                            if (current != null)
                            {
                                index = parts[1].IndexOf(' ');
                                int errorCode;
                                if ((index > 0) && int.TryParse(parts[1].Substring(0, index), out errorCode))
                                {
                                    // Found correct END code
                                    current.ErrorCode = errorCode;
                                    current.ErrorMessage = parts[1].Substring(index).Trim();
                                    onMessage(current);
                                    current = null;
                                }
                                else
                                {
                                    // Invalid end code, ignore
                                }
                                current = null;
                            }
                            break;
                    }

                }
                else
                {
                    if (current != null)
                    {
                        MessageRow row;
                        if (TryParseRow(line, out row))
                        {
                            current.Rows.Add(row);
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Try to parse a single message line.
        /// </summary>
        private static bool TryParseRow(string line, out MessageRow row)
        {
            row = null;
            int id;
            var index = 0;
            if (!TryParseInt(line, ref index, out id))
                return false;

            // Start a row
            row = new MessageRow(id);

            // Parse options
            while (true)
            {
                if (!SkipSpaces(line, ref index))
                {
                    // Only an id
                    return true;
                }

                Option option;
                if (!TryParseOption(line, ref index, out option))
                {
                    // Invalid row
                    return false;
                }

                row.AddOption(option);
                if (!SkipSpaces(line, ref index))
                {
                    // We're done
                    return true;
                }

                if (line[index] != ',')
                {
                    // Comma expected; invalid row
                    return false;
                }
                index++; // Skip ','
            }
        }

        /// <summary>
        /// Try to parse a single command
        /// </summary>
        private static bool TryParseCommand(string s, ref int index, out Command command)
        {
            command = null;
            var start = index;
            if (!SkipSpaces(s, ref index))
                return false;

            string name;
            if (!TryParseIdentifier(s, ref index, out name))
            {
                index = start;
                return false;
            }

            if (!SkipSpaces(s, ref index))
            {
                index = start;
                return false;
            }

            if (s[index] != '(')
            {
                index = start;
                return false;                
            }
            index++; // Skip '('

            int id;
            if (!TryParseInt(s, ref index, out id))
            {
                index = start;
                return false;
            }

            var options = new List<Option>();
            while (true)
            {
                if (!SkipSpaces(s, ref index))
                {
                    index = start;
                    return false;
                }

                if (s[index] == ')')
                {
                    index++;
                    command = new Command(name, id, options.ToArray());
                    return true;
                }

                if (s[index] != ',')
                {
                    index = start;
                    return false;
                }
                index++; // Skip ','

                Option option;
                if (!TryParseOption(s, ref index, out option))
                {
                    index = start;
                    return false;
                }
                options.Add(option);
            }
        }

        /// <summary>
        /// Try to parse a single option
        /// </summary>
        private static bool TryParseOption(string s, ref int index, out Option option)
        {
            option = null;
            var start = index;
            if (!SkipSpaces(s, ref index))
                return false;

            string name;
            if (!TryParseIdentifier(s, ref index, out name))
                return false;

            if (!SkipSpaces(s, ref index))
            {
                // End of string
                option = new Option(name, null, false);
                return true;
            }

            if (s[index] != '[')
            {
                // Name only
                option = new Option(name, null, false);
                return true;
            }
            
            // Parse value
            index++; // Skip '['

            string value;
            var isString = true;
            if (!TryParseString(s, ref index, out value))
            {
                // Not a string, just find end ']'
                isString = false;
                var len = s.Length;
                var valueStart = index;
                while ((index < len) && (s[index] != ']'))
                    index++;

                if (index >= len)
                {
                    // Invalid value
                    index = start;
                    return false;
                }

                value = s.Substring(valueStart, index - valueStart);
            }

            index++; // Skip ']'
            SkipSpaces(s, ref index);

            option = new Option(name, value, isString);
            return true;
        }

        /// <summary>
        /// Try to parse an integer.
        /// </summary>
        private static bool TryParseInt(string s, ref int index, out int value)
        {
            SkipSpaces(s, ref index);
            var start = index;
            var len = s.Length;
            while ((index < len) && char.IsDigit(s[index]))
            {
                index++;
            }
            if (start == index)
            {
                value = -1;
                return false;
            }
            value = int.Parse(s.Substring(start, index - start));
            return true;
        }

        /// <summary>
        /// Try to parse an string.
        /// </summary>
        private static bool TryParseString(string s, ref int index, out string value)
        {
            value = null;
            if (!SkipSpaces(s, ref index)) 
                return false;
            if (s[index] != '\"')
                return false;
            var start = index;
            index++;
            var len = s.Length;

            var sb = new StringBuilder();
            while (true)
            {
                if (index >= len)
                {
                    index = start;
                    return false;
                }
                if (s[index] == '\"')
                {
                    // Duplicate quote?
                    if ((index + 1 < len) && (s[index + 1] == '\"'))
                    {
                        // Yes
                        index++;
                    }
                    else
                    {
                        // End of string
                        index++; // Past end quote
                        value = sb.ToString();
                        return true;
                    }
                }
                sb.Append(s[index++]);
            }
        }

        /// <summary>
        /// Try to parse an identifier.
        /// </summary>
        private static bool TryParseIdentifier(string s, ref int index, out string value)
        {
            value = null;
            if (!SkipSpaces(s, ref index))
                return false;
            if (!char.IsLetter(s[index]))
                return false;
            var start = index;
            index++;
            var len = s.Length;

            while ((index < len) && char.IsLetterOrDigit(s[index]))
            {
                index++;
            }
            value = s.Substring(start, index - start);
            return true;
        }

        /// <summary>
        /// Skip all spaces starting at index.
        /// </summary>
        /// <returns>True if there are characters left in the string, false otherwise</returns>
        private static bool SkipSpaces(string s, ref int index)
        {
            var len = s.Length;
            while ((index < len) && char.IsWhiteSpace(s[index]))
            {
                index++;
            }
            return (index < len);
        }
    }
}
