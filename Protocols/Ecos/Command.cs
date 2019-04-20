using System;
using System.Linq;
using System.Text;

namespace BinkyRailways.Protocols.Ecos
{
    /// <summary>
    /// Represent a single command
    /// </summary>
    public class Command : IEquatable<Command>
    {
        private readonly string name;
        private readonly int id;
        private readonly Option[] options;

        public Command(string name, int id, params string[] options)
            : this(name, id, options.Where(x => x != null).Select(x => new Option(x)).ToArray())
        {
        }

        public Command(string name, int id, params Option[] options)
        {
            this.name = name;
            this.id = id;
            this.options = (options ?? Option.None).Where(x => x != null).OrderBy(x => x.Name).ToArray();
        }

        public string Name
        {
            get { return name; }
        }

        public int Id
        {
            get { return id; }
        }

        public Option[] Options
        {
            get { return options; }
        }

        /// <summary>
        /// Get the first option with matching name.
        /// </summary>
        public Option this[string name]
        {
            get { return options.FirstOrDefault(x => x.Name == name); }
        }

        public bool Equals(Command other)
        {
            if ((other == null) ||
                (other.id != id) ||
                (other.name != name) ||
                (other.options.Length != options.Length))
                return false;
            for (var i = 0; i < options.Length; i++)
            {
                if (!other.options[i].Equals(options[i]))
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Command;
            return (other != null) && Equals(other);
        }

        public override int GetHashCode()
        {
            var hash = name.GetHashCode() ^ (id << 4);
            for (var i = 0; i < options.Length; i++)
            {
                hash |= (options[i].GetHashCode() << i);
            }
            return hash;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(name);
            sb.Append('(');
            sb.Append(id);
            if (options.Length > 0)
            {
                foreach (var option in options)
                {
                    sb.Append(", ");
                    sb.Append(option);
                }

            }
            sb.Append(')');
            return sb.ToString();
        }
    }
}
