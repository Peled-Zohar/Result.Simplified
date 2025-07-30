using System;

namespace Result.Simplified.Extensions;

public static class ResultOfTExtensions
{
    /// <summary>
    /// Continues the operation only if the <see cref="Result{T}"/> is successful and the provided condition evaluates to <c>true</c>.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Result{T}"/>.</typeparam>
    /// <param name="self">The current <see cref="Result{T}"/>.</param>
    /// <param name="condition">A predicate that should return <c>true</c> to continue the successful <see cref="Result{T}"/>.</param>
    /// <param name="errorDescription">The error message in case the predicate evaluates to <c>false</c>.</param>
    /// <returns>The original <see cref="Result{T}"/> if successful and condition is <c>true</c>; otherwise, a failed <see cref="Result{T}"/> with the provided error message.</returns>
    public static Result<T> ThenIf<T>(
        this Result<T> self,
        Predicate<T> condition,
        string errorDescription
    ) => self.IsSuccess
        ? condition(self.Value) ? self : Result<T>.Fail(errorDescription)
        : self;

    /// <summary>
    /// Fails the <see cref="Result{T}"/> if it is successful and the provided fail condition evaluates to <c>true</c>.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Result{T}"/>.</typeparam>
    /// <param name="self">The current <see cref="Result{T}"/>.</param>
    /// <param name="failCondition">A predicate that should return <c>true</c> to fail the <see cref="Result{T}"/>.</param>
    /// <param name="errorDescription">The error message in case the predicate evaluates to <c>true</c>.</param>
    /// <returns>The original <see cref="Result{T}"/> if successful and failCondition is <c>false</c>; otherwise, a failed <see cref="Result{T}"/> with the provided error message.</returns>
    public static Result<T> ThenFailIf<T>(
        this Result<T> self,
        Predicate<T> failCondition,
        string errorDescription
    ) => self.IsSuccess
        ? failCondition(self.Value) ? Result<T>.Fail(errorDescription) : self
        : self;

    /// <summary>
    /// Continues the operation only if the <see cref="Result{T}"/> is a failure and the provided condition evaluates to <c>true</c>.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Result{T}"/>.</typeparam>
    /// <param name="self">The current <see cref="Result{T}"/>.</param>
    /// <param name="condition">A predicate that should return <c>true</c> to continue the failed <see cref="Result{T}"/>.</param>
    /// <param name="errorDescription">The error message in case the predicate evaluates to <c>false</c>.</param>
    /// <returns>The original <see cref="Result{T}"/> if failed and condition is <c>true</c>; otherwise, a failed <see cref="Result{T}"/> with the provided error message.</returns>
    public static Result<T> OtherwiseIf<T>(
        this Result<T> self,
        Predicate<T> condition,
        string errorDescription
    ) => !self.IsSuccess
        ? condition(self.Value) ? self : Result<T>.Fail(errorDescription)
        : self;

    /// <summary>
    /// Fails the <see cref="Result{T}"/> again if it is already a failure and the provided fail condition evaluates to <c>true</c>.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Result{T}"/>.</typeparam>
    /// <param name="self">The current <see cref="Result{T}"/>.</param>
    /// <param name="failCondition">A predicate that should return <c>true</c> to apply an additional failure.</param>
    /// <param name="errorDescription">The error message in case the predicate evaluates to <c>true</c>.</param>
    /// <returns>The original <see cref="Result{T}"/> if failed and failCondition is <c>false</c>; otherwise, a new failed <see cref="Result{T}"/> with the provided error message.</returns>
    public static Result<T> OtherwiseFailIf<T>(
        this Result<T> self,
        Predicate<T> failCondition,
        string errorDescription
    ) => !self.IsSuccess
        ? failCondition(self.Value) ? Result<T>.Fail(errorDescription) : self
        : self;
}
