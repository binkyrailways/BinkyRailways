using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.Services
{
    /// <summary>
    /// Evaluate standard expressions.
    /// </summary>
    partial class Expression
    {
        /// <summary>
        /// Single element of an expression.
        /// </summary>
        public abstract class Token
        {
            /// <summary>
            /// Evaluate the value of this token in the context of the given entity.
            /// </summary>
            public abstract string Evaluate(IEntity entity);
        }

        /// <summary>
        /// Literal text
        /// </summary>
        public sealed class LiteralText : Token
        {
            private readonly string text;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LiteralText(string text)
            {
                this.text = text;
            }

            /// <summary>
            /// Evaluate the value of this token in the context of the given entity.
            /// </summary>
            public override string Evaluate(IEntity entity)
            {
                return text;
            }
        }

        /// <summary>
        /// Single variable.
        /// </summary>
        public sealed class Variable : Token
        {
            private readonly string variableName;

            /// <summary>
            /// Default ctor
            /// </summary>
            public Variable(string variableName)
            {
                this.variableName = variableName ?? string.Empty;
            }

            /// <summary>
            /// Evaluate the value of this token in the context of the given entity.
            /// </summary>
            public override string Evaluate(IEntity entity)
            {
                var aEntity = entity as IAddressEntity;
                var address = (aEntity != null) ? aEntity.Address : null;
                switch (variableName.Trim().ToLower())
                {
                    case AddressVariable:
                        return (address != null) ? address.ToString() : string.Empty;
                    case AddressProtocolVariable:
                        return (address != null) ? address.Type.ToString() : string.Empty;
                    case AddressValueVariable:
                        return (address != null) ? address.Value.ToString() : string.Empty;
                    default:
                        return CreateVariableExpression(variableName);
                }
            }
        }
    }
}
