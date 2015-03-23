namespace DNNGamification
{
    using System;

    using System.Linq;
    using System.Linq.Expressions;

    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Expression tree visitor.
    /// </summary>
    internal class ExpressionTreeVisitor : ExpressionVisitor
    {
        #region Private Fields

        private readonly ParameterExpression _parameter;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Visits parameter expression.
        /// </summary>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(_parameter);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with specified parameter.
        /// </summary>
        internal ExpressionTreeVisitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        #endregion
    }

    /// <summary>
    /// Enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Private Methods

        /// <summary>
        /// Randomize iterator.
        /// </summary>
        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
        {
            var buffer = source.ToList();

            for (int i = 0; i < buffer.Count; i++) // iterate items
            {
                int j = rng.Next(i, buffer.Count); yield return buffer[j]; 

                buffer[j] = buffer[i];
            }
        }

        #endregion

        #region Public Methods : IEnumerable

        /// <summary>
        /// Shuffles IEnumerable items.
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.ShuffleIterator(new Random());
        }

        /// <summary>
        /// Executes action for each IEnumerable item.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source) action.Invoke(item);
        }

        #endregion
    }

    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Checks equals ignore case.
        /// </summary>
        public static bool EqualsIgnoreCase(this string source, string target)
        {
            return source.Equals(target, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}
