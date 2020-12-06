using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.Services
{
    /// <summary>
    /// Evaluate standard expressions.
    /// </summary>
    public partial class Expression
    {
        public const char VariablePrefix = '{';
        public const char VariablePostfix = '}';

        public const string AddressVariable = "address";
        public const string AddressProtocolVariable = "address-protocol";
        public const string AddressValueVariable = "address-value";

        public static readonly string[] VariableNames = new[] {
            AddressVariable,
            AddressProtocolVariable,
            AddressValueVariable                                                                
        };

        private readonly Token[] tokens;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Expression(IEnumerable<Token> tokens)
        {
            this.tokens = tokens.ToArray();
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Expression(string expression)
            : this(Parse(expression))
        {
        }

        /// <summary>
        /// Evaluate this expression in the context of the given entity.
        /// </summary>
        public string Evaluate(IEntity entity)
        {
            var sb = new StringBuilder();
            foreach (var token in tokens)
            {
                sb.Append(token.Evaluate(entity));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets all tokens.
        /// </summary>
        public IEnumerable<Token> Tokens { get { return tokens; } }

        /// <summary>
        /// Create an expression containing the given variable.
        /// </summary>
        public static string CreateVariableExpression(string variableName)
        {
            return VariablePrefix + variableName + VariablePostfix;
        }

        /// <summary>
        /// Parse the given expression into tokens.
        /// </summary>
        private static IEnumerable<Token> Parse(string expression)
        {
            var index = 0;
            var len = expression.Length;
            while (index < len)
            {
                var start = index;
                while ((index < len) && (expression[index] != VariablePrefix))
                {
                    index++;
                }
                if (start != index)
                {
                    // Found some literal text
                    yield return new LiteralText(expression.Substring(start, index - start));
                }
                if (index >= len)
                {
                    // Done
                    break;
                }

                if (expression[index] == VariablePrefix)
                {
                    // Found start of an expression;
                    index++;
                    start = index;
                    while ((index < len) && (expression[index] != VariablePostfix))
                    {
                        index++;
                    }
                    if (index >= len)
                    {
                        // Did not found postfix
                        start--;
                        yield return new LiteralText(expression.Substring(start, len - start));
                    }
                    else
                    {
                        // Found postfix
                        yield return new Variable(expression.Substring(start, index - start));
                        index++; // Skip postfix
                    }
                }
            }
        }
    }
}
