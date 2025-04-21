using System;

namespace Result.Simplified
{
    /// <summary>
    /// Provides a way to return a value and a boolean success indicator, 
    /// and (in case of an error) error description from a method.
    /// The <see cref="Result{T}"/> class overloads the &amp; and | operators to make it easy to use in validations.
    /// The &amp; operator returns the first failed operand (or the last operand tested),
    /// and the | operator returns the first succeesfull  operand (or the last operand tested).
    /// The &amp;&amp; operator and || operators will do the same, but in a short-circuit way.
    /// </summary>
    public class Result<T> : Result
    {
        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a success.
        /// </summary>
        /// <param name="value">The value to return from the method.</param>
        /// <returns>A new instance of the <see cref="Result{T}"/> class indicating success.</returns>
        public static Result<T> Success(T value)
            => new Result<T>(value);

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class based on the predicate.
        /// </summary>
        /// <param name="predicate">A condition to evaluate</param>
        /// <param name="value">The value to return from the method.</param>
        /// <param name="errorDescription">An error description in case <paramref name="predicate"/> evaluates to false</param>
        /// <param name="includeValueInFailResult">An optional <see cref="bool"/> value indicating wether to include value in failed result. default is false.</param>
        /// <returns>A new instance of the <see cref="Result{T}"/> class indicating success if <paramref name="predicate"/> evaluates to true, or failure otherwise.</returns>
        public static Result<T> SuccessIf(Predicate<T> predicate, T value, string errorDescription, bool includeValueInFailResult = false)
            => predicate.Invoke(value) ? Success(value) : Fail(errorDescription);

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a failure.
        /// </summary>
        /// <param name="errorDescription">Description of the error.</param>
        /// <returns>A new instance of the <see cref="Result{T}"/> class indicating a failure.</returns>
        public static new Result<T> Fail(string errorDescription)
            => new Result<T>(errorDescription, default);

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a failure,
        /// yet still returning the value.
        /// </summary>
        /// <param name="errorDescription">Description of the error.</param>
        /// <param name="value">The value to return from the method.</param>
        /// <returns>A new instance of the <see cref="Result{T}"/> class indicating a failure, but still have a value.</returns>
        public static Result<T> Fail(string errorDescription, T value)
            => new Result<T>(errorDescription, value);

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class based on the predicate.
        /// </summary>
        /// <param name="predicate">A condition to evaluate</param>
        /// <param name="value">The value to return from the method.</param>
        /// <param name="errorDescription">An error description in case <paramref name="predicate"/> evaluates to false</param>
        /// <param name="includeValueInFailResult">An optional <see cref="bool"/> value indicating wether to include value in failed result. default is false.</param>
        /// <returns></returns>
        public static Result<T> FailIf(Predicate<T> predicate, T value, string errorDescription, bool includeValueInFailResult = false)
            => predicate.Invoke(value) ? Fail(errorDescription, includeValueInFailResult ? value : default) : Success(value);
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a success.
        /// </summary>
        /// <param name="value">The value to return from the method.</param>
        protected Result(T value) : base()
            => Value = value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a failure,
        /// yet still returning the value.
        /// </summary>
        /// <param name="errorDescription">Description of the error.</param>
        /// <param name="value">The value to return from the method.</param>
        protected Result(string errorDescription, T value) : base(errorDescription)
            => Value = value;

        #endregion ctor

        #region properties

        /// <summary>
        /// The value to return from the method.
        /// </summary>
        public T Value { get; }

        #endregion properties

        #region operators

        /// <summary>
        /// Combines two results using the logical AND operation.
        /// Returns the first operand if its <see cref="IsSuccess"/> property is false,
        /// otherwise the second operand.
        /// This can be used to determine the first result that failed in a sequence of results.
        /// </summary>
        /// <example>
        /// The following code demonstrate how to ensure multiple results have succeeded
        /// <code>
        /// if(result1 &amp;&amp; result2 &amp;&amp; result3)
        /// { 
        ///     // All results succeeded.
        /// }
        /// </code>
        /// </example>
        /// <param name="self">An instance of the <see cref="Result{T}"/> class.</param>
        /// <param name="other">An instance of the <see cref="Result{T}"/> class.</param>
        /// <returns><paramref name="self"/> if not succeeded, <paramref name="other"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException">if any of the operands is null.</exception>
        public static Result<T> operator &(Result<T> self, Result<T> other)
        {
            if (self is null) throw new ArgumentNullException(nameof(self));
            if (other is null) throw new ArgumentNullException(nameof(other));

            return self.IsSuccess ? other : self;
        }

        /// <summary>
        /// Combines two results using the logical OR operation.
        /// Returns the first operand if its <see cref="IsSuccess"/> property is false,
        /// otherwise the second operand.
        /// This can be used to determine the first result that failed in a sequence of results.
        /// </summary>
        /// <example>
        /// The following code demonstrate how to ensure at least one result has succeeded
        /// <code>
        /// if(result1 || result2 || result3)
        /// { 
        ///     // At least one result has succeeded.
        /// }
        /// </code>
        /// </example>
        /// <param name="self">An instance of the <see cref="Result{T}"/> class.</param>
        /// <param name="other">An instance of the Result<typeparamref name="T"/> class.</param>
        /// <returns><paramref name="self"/> if succeeded, <paramref name="other"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException">if any of the operands is null.</exception>
        public static Result<T> operator |(Result<T> self, Result<T> other)
        {
            if (self is null) throw new ArgumentNullException(nameof(self));
            if (other is null) throw new ArgumentNullException(nameof(other));
            return self.IsSuccess ? self : other;
        }

        #endregion operators
    }
}
