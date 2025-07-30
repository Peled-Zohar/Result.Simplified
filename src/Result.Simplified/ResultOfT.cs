using System;

namespace Result.Simplified;

/// <summary>
/// Provides a way to return a value and a boolean success indicator, 
/// and (in case of an error) error description from a method.
/// The <see cref="Result{T}"/> class overloads the <c>&amp;</c> and <c>|</c> operators to make it easy to use in validations.
/// The <c>&amp;</c> operator returns the first failed operand (or the last operand tested),
/// and the <c>|</c> operator returns the first successful  operand (or the last operand tested).
/// The <c>&amp;&amp;</c> operator and <c>||</c> operators will do the same, but in a short-circuit way.
/// </summary>
public class Result<T> : VoidResult
{
    #region ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a success.
    /// </summary>
    /// <param name="value">The value to return from the method.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class indicating success.</returns>
    public static Result<T> Success(T value)
        => new(value);

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class based on the predicate.
    /// </summary>
    /// <param name="predicate">A condition to evaluate.</param>
    /// <param name="value">The value to return from the method.</param>
    /// <param name="errorDescription">An error description in case <paramref name="predicate"/> evaluates to <c>false</c></param>
    /// <param name="includeValueInFailResult">An optional <see cref="bool"/> value indicating whether to include value in failed result. default is <c>false</c>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> evaluates to <c>false</c> and <paramref name="errorDescription"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="predicate"/> evaluates to <c>false</c> and <paramref name="errorDescription"/> is empty or contains only white spaces.</exception>
    /// <returns>A new instance of the <see cref="Result{T}"/> class indicating success if <paramref name="predicate"/> evaluates to <c>true</c>, or failure otherwise.</returns>
    public static Result<T> SuccessIf(Predicate<T> predicate, T value, string errorDescription, bool includeValueInFailResult = false)
        => predicate?.Invoke(value) ?? throw new ArgumentNullException(nameof(predicate))
        ? Success(value)
        : Fail(errorDescription, includeValueInFailResult ? value : default);

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a failure.
    /// </summary>
    /// <param name="errorDescription">Description of the error.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="errorDescription"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="errorDescription"/> is empty or contains only white spaces.</exception>
    /// <returns>A new instance of the <see cref="Result{T}"/> class indicating a failure.</returns>
    public static new Result<T> Fail(string errorDescription)
        => new(errorDescription, default);

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class to indicate a failure,
    /// yet still returning the value.
    /// </summary>
    /// <param name="errorDescription">Description of the error.</param>
    /// <param name="value">The value to return from the method.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="errorDescription"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="errorDescription"/> is empty or contains only white spaces.</exception>
    /// <returns>A new instance of the <see cref="Result{T}"/> class indicating a failure, but still containing a value.</returns>
    public static Result<T> Fail(string errorDescription, T value)
        => new(errorDescription, value);

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class based on the negativePredicate.
    /// </summary>
    /// <param name="negativePredicate">A condition to evaluate.</param>
    /// <param name="value">The value to return from the method.</param>
    /// <param name="errorDescription">An error description in case <paramref name="negativePredicate"/> evaluates to <c>false</c></param>
    /// <param name="includeValueInFailResult">An optional <see cref="bool"/> value indicating whether to include value in failed result. Default is <c>false</c>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="negativePredicate"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="negativePredicate"/> evaluates to <c>true</c> and <paramref name="errorDescription"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="negativePredicate"/> evaluates to <c>true</c> and <paramref name="errorDescription"/> is empty or contains only white spaces.</exception>
    /// <returns>A new instance of the <see cref="Result{T}"/> class indicating success if <paramref name="predicate"/> evaluates to <c>false</c>, or failure otherwise.</returns>
    public static Result<T> FailIf(Predicate<T> negativePredicate, T value, string errorDescription, bool includeValueInFailResult = false)
        => negativePredicate?.Invoke(value) ?? throw new ArgumentNullException(nameof(negativePredicate))
        ? Fail(errorDescription, includeValueInFailResult ? value : default)
        : Success(value);

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
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="errorDescription"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="errorDescription"/> is empty or contains only white spaces.</exception>

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
    /// Returns the first operand if its <see cref="IsSuccess"/> property is <c>false</c>,
    /// otherwise the second operand.
    /// This can be used to determine the first result that failed in a sequence of results.
    /// </summary>
    /// <example>
    /// The following code demonstrates how to ensure multiple results have succeeded
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
    /// <exception cref="ArgumentNullException">If any of the operands are null.</exception>
    public static Result<T> operator &(Result<T> self, Result<T> other)
    {
        if (self is null) throw new ArgumentNullException(nameof(self));
        if (other is null) throw new ArgumentNullException(nameof(other));

        return self.IsSuccess ? other : self;
    }

    /// <summary>
    /// Combines two results using the logical OR operation.
    /// Returns the first operand if its <see cref="IsSuccess"/> property is <c>true</c>,
    /// otherwise the second operand.
    /// This can be used to determine the first result that failed in a sequence of results.
    /// </summary>
    /// <example>
    /// The following code demonstrates how to ensure at least one result has succeeded
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
    /// <exception cref="ArgumentNullException">If any of the operands are null.</exception>
    public static Result<T> operator |(Result<T> self, Result<T> other)
    {
        if (self is null) throw new ArgumentNullException(nameof(self));
        if (other is null) throw new ArgumentNullException(nameof(other));
        return self.IsSuccess ? self : other;
    }

    #endregion operators
}
