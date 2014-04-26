using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BinkyRailways.Core.Util
{
    /// <summary>
    /// Class for easily getting information about a type member,
    /// and referencing the member by name, or getting it as a
    /// <see cref="MemberInfo"/>, <see cref="PropertyInfo"/>,
    /// <see cref="FieldInfo"/>, or <see cref="MethodInfo"/>.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// The <see cref="MemberInfo"/> discovered for the member specified.
        /// </summary>
        public MemberInfo Info { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="Member"/> using the specified
        /// <see cref="MemberExpression"/>.
        /// </summary>
        /// <param name="expression">The expression that references the desired member.</param>
        public Member(MemberExpression expression)
        {
            this.Info = expression.Member;
        }

        /// <summary>
        /// Initializes a new <see cref="Member"/> using the specified
        /// <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="info">The member info that references the desired member.</param>
        public Member(MemberInfo info)
        {
            this.Info = info;
        }

        /// <summary>
        /// Gets the current member as a <see cref="PropertyInfo"/> instance.
        /// </summary>
        /// <returns>A <see cref="PropertyInfo"/> instance for the current member <see cref="Info"/>.</returns>
        /// <exception cref="InvalidCastException">
        /// When the current member cannot be cast to a <see cref="PropertyInfo"/>.
        /// </exception>
        public PropertyInfo AsProperty()
        {
            return (PropertyInfo)this.Info;
        }

        /// <summary>
        /// Gets the current member as a <see cref="FieldInfo"/> instance.
        /// </summary>
        /// <returns>A <see cref="FieldInfo"/> instance for the current member <see cref="Info"/>.</returns>
        /// <exception cref="InvalidCastException">
        /// When the current member cannot be cast to a <see cref="FieldInfo"/>.
        /// </exception>
        public FieldInfo AsField()
        {
            return (FieldInfo)this.Info;
        }

        /// <summary>
        /// Gets the current member as a <see cref="MethodInfo"/> instance.
        /// </summary>
        /// <returns>A <see cref="MethodInfo"/> instance for the current member <see cref="Info"/>.</returns>
        /// <exception cref="InvalidCastException">
        /// When the current member cannot be cast to a <see cref="MethodInfo"/>.
        /// </exception>
        public MethodInfo AsMethod()
        {
            return (MethodInfo)this.Info;
        }

        /// <summary>
        /// Returns the current member name as the string representation of a <see cref="Member"/>.
        /// </summary>
        /// <returns>The name of the current member <see cref="Info"/>.</returns>
        public override string ToString()
        {
            return Info.Name;
        }

        /// <summary>
        /// Implicitly cast a <see cref="Member"/> to a <see cref="String"/>, using the
        /// <see cref="ToString"/> method.
        /// </summary>
        /// <param name="member">The <see cref="Member"/> to cast to a string.</param>
        /// <returns>The name of the current member <see cref="Info"/>.</returns>
        public static implicit operator string(Member member)
        {
            return member.ToString();
        }

        /// <summary>
        /// Implicitly cast a <see cref="Member"/> to a <see cref="MemberInfo"/>, by
        /// returning the <see cref="Info"/> property value.
        /// </summary>
        /// <param name="member">The <see cref="Member"/> to cast to a <see cref="MemberInfo"/>.</param>
        /// <returns>The <see cref="Info"/> of the specified member, or <c>null</c>.</returns>
        public static implicit operator MemberInfo(Member member)
        {
            return member != null ? member.Info : null;
        }

        /// <summary>
        /// Implicitly cast a <see cref="Member"/> to a <see cref="MethodInfo"/>, by
        /// returning the <see cref="AsMethod"/> result.
        /// </summary>
        /// <param name="member">
        /// The <see cref="Member"/> to cast to a <see cref="MethodInfo"/>.
        /// </param>
        /// <returns>A <see cref="MethodInfo"/> for the specified member.</returns>
        /// <exception cref="InvalidCastException">
        /// When the current member cannot be cast to a <see cref="MethodInfo"/>.
        /// </exception>
        public static implicit operator MethodInfo(Member member)
        {
            return member != null ? member.AsMethod() : null;
        }

        /// <summary>
        /// Implicitly cast a <see cref="MemberInfo"/> to a <see cref="Member"/>.
        /// </summary>
        /// <param name="memberInfo">The member info to use as a member.</param>
        /// <returns>
        /// A <see cref="MemberInfo"/> representing the specified <paramref name="memberInfo"/>.
        /// </returns>
        public static implicit operator Member(MemberInfo memberInfo)
        {
            return new Member(memberInfo);
        }

        /// <summary>
        /// Create a <see cref="Member"/> using a method call expression.
        /// </summary>
        /// <remarks>
        /// This is used for <c>void</c> methods.
        /// </remarks>
        /// <param name="methodCall">The method call expression to use as a member.</param>
        /// <returns>A <see cref="Member"/> for the specified method call expression.</returns>
        public static Member Of(Expression<Action> methodCall)
        {
            return methodCall.AsMethod();
        }

        /// <summary>
        /// Create a <see cref="Member"/> using a method call expression
        /// for the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodCall"></param>
        /// <returns></returns>
        public static Member Of<T>(Expression<Action<T>> methodCall)
        {
            return methodCall.AsMethod();
        }

        /// <summary>
        /// Create a <see cref="Member"/> using a property, field, or non-void
        /// method call expression.
        /// </summary>
        /// <param name="member">The member expression to use as a member.</param>
        /// <returns>A <see cref="Member"/> for the specified member expression.</returns>
        public static Member Of(Expression<Func<object>> member)
        {
            return member.AsMemberOrMethod();
        }

        /// <summary>
        /// Create a <see cref="Member"/> using a property, field, or non-void
        /// method call expression for the specified type.
        /// </summary>
        /// <typeparam name="T">The type containing the member specified.</typeparam>
        /// <param name="member">The member expression to use as a member.</param>
        /// <returns>A <see cref="Member"/> for the specified member expression.</returns>
        public static Member Of<T>(Expression<Func<T, object>> member)
        {
            return member.AsMemberOrMethod();
        }
    }

    /// <summary>
    /// Helper class for converting expressions to members.
    /// </summary>
    public static class ExpressionMember
    {
        public static Member AsMethod(this LambdaExpression member)
        {
            if (member == null)
            {
                return null;
            }

            MethodCallExpression methodExpr = member.Body as MethodCallExpression;
            return methodExpr != null ? new Member(methodExpr.Method) : null;
        }

        public static Member AsMember(this LambdaExpression member)
        {
            if (member == null)
            {
                return null;
            }

            MemberExpression memberExpr = member.Body as MemberExpression;
            return memberExpr != null ? new Member(memberExpr) : null;
        }

        public static Member AsMemberOrMethod(this LambdaExpression member)
        {
            return member.AsMember() ?? member.AsMethod() ?? member.AsUnaryWrappedMember();
        }

        public static Member AsMemberOrMethod(this Expression member)
        {
            MemberExpression memberExpr = member as MemberExpression;
            if (memberExpr != null)
            {
                return memberExpr.Member;
            }

            MethodCallExpression methodExpr = member as MethodCallExpression;
            if (methodExpr != null)
            {
                return methodExpr.Method;
            }

            UnaryExpression unaryExpr = member as UnaryExpression;
            if (unaryExpr != null)
            {
                return unaryExpr.Operand.AsMemberOrMethod();
            }

            return null;
        }

        public static Member AsUnaryWrappedMember(this LambdaExpression member)
        {
            if (member == null)
            {
                return null;
            }

            UnaryExpression unaryExpr = member.Body as UnaryExpression;
            if (unaryExpr != null)
            {
                return unaryExpr.Operand.AsMemberOrMethod();
            }

            return null;
        }
    }
}
