using System;

namespace Result.Simplified.Extensions;

public static class ResultExtensions
{
    /// <summary>
    /// Continues the current result with another result, if the current result is successful.
    /// </summary>
    /// <param name="self">The current result.</param>
    /// <param name="predicate">A predicate to build the next result. Success if evaluates to <c>true</c>, fail otherwise.</param>
    /// <param name="errorDescription">The error description in case the <paramref name="predicate"/> evaluates to <c>false</c>.</param>
    /// <returns>
    /// <paramref name="self"/> if the current result has succeeded; 
    /// otherwise, a new result instance based on <paramref name="predicate"/> and <paramref name="errorDescription"/>.
    /// </returns>        
    public static Result ThenIf(
        this Result self,
        Func<bool> predicate,
        string errorDescription
    ) => self.IsSuccess
        ? Result.SuccessIf(predicate, errorDescription)
        : self;

    /// <summary>
    /// Continues the current result with another result, if the current result is successful.
    /// </summary>
    /// <param name="self">The current result.</param>
    /// <param name="negativePredicate">A predicate to build the next result. Fail if evaluates to <c>true</c>, success otherwise.</param>
    /// <param name="errorDescription">The error description in case the <paramref name="negativePredicate"/> evaluates to <c>true</c>.</param>
    /// <returns>
    /// <paramref name="self"/> if the current result has succeeded; 
    /// otherwise, a new result instance based on <paramref name="negativePredicate"/> and <paramref name="errorDescription"/>.
    /// </returns>
    public static Result ThenFailIf(
        this Result self,
        Func<bool> negativePredicate,
        string errorDescription
    ) => self.IsSuccess
        ? Result.FailIf(negativePredicate, errorDescription) 
        : self;

    /// <summary>
    /// Continues the current result with another result, if the current result has failed.
    /// </summary>
    /// <param name="self">The current result.</param>
    /// <param name="predicate">A predicate to build the next result. Success if evaluates to <c>true</c>, fail otherwise.</param>
    /// <param name="errorDescription">The error description in case the <paramref name="predicate"/> evaluates to <c>false</c>.</param>
    /// <returns>
    /// <paramref name="self"/> if the current result has failed; 
    /// otherwise, a new result instance based on <paramref name="predicate"/> and <paramref name="errorDescription"/>.
    /// </returns>
    public static Result OtherwiseIf(
        this Result self,
        Func<bool> predicate,
        string errorDescription
    ) => !self.IsSuccess
        ? Result.SuccessIf(predicate, errorDescription)
        : self;

    /// <summary>
    /// Continues the current result with another result, if the current result has failed.
    /// </summary>
    /// <param name="self">The current result.</param>
    /// <param name="negativePredicate">A predicate to build the next result. Fail if evaluates to <c>true</c>, success otherwise.</param>
    /// <param name="errorDescription">The error description in case the <paramref name="negativePredicate"/> evaluates to <c>true</c>.</param>
    /// <returns>
    /// <paramref name="self"/> if the current result has failed; 
    /// otherwise, a new result instance based on <paramref name="negativePredicate"/> and <paramref name="errorDescription"/>.
    /// </returns>
    public static Result OtherwiseFailIf(
        this Result self,
        Func<bool> negativePredicate,
        string errorDescription
    ) => !self.IsSuccess
        ? Result.FailIf(negativePredicate, errorDescription)
        : self;
}
