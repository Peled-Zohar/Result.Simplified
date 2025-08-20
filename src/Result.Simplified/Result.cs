using System;
using System.Diagnostics.CodeAnalysis;

namespace Result.Simplified;

/// <summary>
/// Deprecated. Use <see cref="VoidResult"/> instead.
/// </summary>
[ExcludeFromCodeCoverage] // There's no need to test it anymore, it's obsolte
[Obsolete("Use VoidResult instead. Result will be removed in in v3.0.0, no sooner than 2026-01-01.")]
public class Result
{
    #region ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class to indicate a success.
    /// </summary>
    /// <returns>An instance of the <see cref="Result"/> class indicating success.</returns>
    public static Result Success()
        => new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class to indicate a failure.
    /// </summary>
    /// <param name="errorDescription">Description of the error.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="errorDescription"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="errorDescription"/> is empty or contains only white spaces.</exception>
    /// <returns>An instance of the <see cref="Result"/> class indicating failure.</returns>
    public static Result Fail(string errorDescription)
        => new(errorDescription);

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class based on the <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A condition to evaluate.</param>
    /// <param name="errorDescription">Description of the error in case <paramref name="predicate"/> evaluates to <c>false</c>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> evaluates to <c>false</c> and <paramref name="errorDescription"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="predicate"/> evaluates to <c>false</c> and <paramref name="errorDescription"/> is empty or contains only white spaces.</exception>
    /// <returns>An instance of the <see cref="Result"/> class indicating success if <paramref name="predicate"/> evaluates to <c>true</c>, otherwise fail.</returns>
    public static Result SuccessIf(Func<bool> predicate, string errorDescription)
        => predicate?.Invoke() ?? throw new ArgumentNullException(nameof(predicate))
        ? Success()
        : Fail(errorDescription);

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class based on the <paramref name="negativePredicate"/>.
    /// </summary>
    /// <param name="negativePredicate">A condition to evaluate.</param>
    /// <param name="errorDescription">Description of the error in case <paramref name="negativePredicate"/> evaluates to <c>true</c>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="negativePredicate"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="negativePredicate"/> evaluates to <c>true</c> and <paramref name="errorDescription"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="negativePredicate"/> evaluates to <c>true</c> and <paramref name="errorDescription"/> is empty or contains only white spaces.</exception>
    /// <returns>An instance of the <see cref="Result"/> class indicating fail if <paramref name="negativePredicate"/> evaluates to <c>true</c>, otherwise success.</returns>
    public static Result FailIf(Func<bool> negativePredicate, string errorDescription)
        => negativePredicate?.Invoke() ?? throw new ArgumentNullException(nameof(negativePredicate))
        ? Fail(errorDescription)
        : Success();

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class to indicate a success.
    /// </summary>
    protected Result()
        => IsSuccess = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class to indicate a failure.
    /// </summary>
    /// <param name="errorDescription">Description of the error.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="errorDescription"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="errorDescription"/> is empty or contains only white spaces.</exception>
    protected Result(string errorDescription)
    {
        if (errorDescription == null)
            throw new ArgumentNullException(nameof(errorDescription));

        if (string.IsNullOrWhiteSpace(errorDescription))
            throw new ArgumentException("Error description cannot be empty.", nameof(errorDescription));

        IsSuccess = false;
        ErrorDescription = errorDescription;
    }

    #endregion ctor

    #region properties

    /// <summary>
    /// Gets a boolean value indicating success or failure of the method.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the description of the error.
    /// </summary>
    public string ErrorDescription { get; }

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
    /// <param name="self">An instance of the <see cref="Result"/> class.</param>
    /// <param name="other">An instance of the <see cref="Result"/> class.</param>
    /// <returns><paramref name="self"/> if not succeeded, <paramref name="other"/> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If any of the operands are null.</exception>
    public static Result operator &(Result self, Result other)
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
    /// <param name="self">An instance of the <see cref="Result"/> class.</param>
    /// <param name="other">An instance of the <see cref="Result"/> class.</param>
    /// <returns><paramref name="self"/> if succeeded, <paramref name="other"/> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If any of the operands are null.</exception>
    public static Result operator |(Result self, Result other)
    {
        if (self is null) throw new ArgumentNullException(nameof(self));
        if (other is null) throw new ArgumentNullException(nameof(other));
        return self.IsSuccess ? self : other;
    }

    /// <summary>
    /// Returns <c>true</c> when succeeded.
    /// <para>
    /// This operator is needed to allow the usage of the <c>||</c> operator.
    /// </para>
    /// </summary>
    /// <param name="self">The instance of the <see cref="Result"/> class to test.</param>
    /// <returns><c>true</c> when succeeded, <c>false</c> otherwise.</returns>
    public static bool operator true(Result self)
        => self.IsSuccess;

    /// <summary>
    /// Returns <c>false</c> when succeeded. (the opposite of the true operator.)
    /// <para>
    /// This operator is needed to allow the usage of the <c>&amp;&amp;</c> operator.
    /// </para>
    /// </summary>
    /// <param name="self">The instance of the <see cref="Result"/> class to test.</param>
    /// <returns><c>false</c> when succeeded, <c>true</c> otherwise.</returns>
    public static bool operator false(Result self)
        => !self.IsSuccess;

    #endregion operators
}
