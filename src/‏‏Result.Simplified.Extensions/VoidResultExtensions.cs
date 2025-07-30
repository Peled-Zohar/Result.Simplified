using System;


namespace Result.Simplified.Extensions;

public static class VoidResultExtensions
{
    /// <summary>
    /// Continues the current <see cref="VoidResult"/> with another <see cref="VoidResult"/>, if the current <see cref="VoidResult"/> is successful.
    /// </summary>
    /// <param name="self">The current <see cref="VoidResult"/>.</param>
    /// <param name="predicate">A predicate to build the next <see cref="VoidResult"/>. Success if evaluates to <c>true</c>, fail otherwise.</param>
    /// <param name="errorDescription">The error description in case the <paramref name="predicate"/> evaluates to <c>false</c>.</param>
    /// <returns>
    /// <paramref name="self"/> if the current <see cref="VoidResult"/> has succeeded; 
    /// otherwise, a new <see cref="VoidResult"/> instance based on <paramref name="predicate"/> and <paramref name="errorDescription"/>.
    /// </returns>        
    public static VoidResult ThenIf(
        this VoidResult self,
        Func<bool> predicate,
        string errorDescription
    ) => self.IsSuccess
        ? VoidResult.SuccessIf(predicate, errorDescription)
        : self;

    /// <summary>
    /// Continues the current <see cref="VoidResult"/> with another <see cref="VoidResult"/>, if the current <see cref="VoidResult"/> is successful.
    /// </summary>
    /// <param name="self">The current <see cref="VoidResult"/>.</param>
    /// <param name="negativePredicate">A predicate to build the next <see cref="VoidResult"/>. Fail if evaluates to <c>true</c>, success otherwise.</param>
    /// <param name="errorDescription">The error description in case the <paramref name="negativePredicate"/> evaluates to <c>true</c>.</param>
    /// <returns>
    /// <paramref name="self"/> if the current <see cref="VoidResult"/> has succeeded; 
    /// otherwise, a new <see cref="VoidResult"/> instance based on <paramref name="negativePredicate"/> and <paramref name="errorDescription"/>.
    /// </returns>
    public static VoidResult ThenFailIf(
        this VoidResult self,
        Func<bool> negativePredicate,
        string errorDescription
    ) => self.IsSuccess
        ? VoidResult.FailIf(negativePredicate, errorDescription) 
        : self;

    /// <summary>
    /// Continues the current <see cref="VoidResult"/> with another <see cref="VoidResult"/>, if the current <see cref="VoidResult"/> has failed.
    /// </summary>
    /// <param name="self">The current <see cref="VoidResult"/>.</param>
    /// <param name="predicate">A predicate to build the next <see cref="VoidResult"/>. Success if evaluates to <c>true</c>, fail otherwise.</param>
    /// <param name="errorDescription">The error description in case the <paramref name="predicate"/> evaluates to <c>false</c>.</param>
    /// <returns>
    /// <paramref name="self"/> if the current <see cref="VoidResult"/> has failed; 
    /// otherwise, a new <see cref="VoidResult"/> instance based on <paramref name="predicate"/> and <paramref name="errorDescription"/>.
    /// </returns>
    public static VoidResult OtherwiseIf(
        this VoidResult self,
        Func<bool> predicate,
        string errorDescription
    ) => !self.IsSuccess
        ? VoidResult.SuccessIf(predicate, errorDescription)
        : self;

    /// <summary>
    /// Continues the current <see cref="VoidResult"/> with another <see cref="VoidResult"/>, if the current <see cref="VoidResult"/> has failed.
    /// </summary>
    /// <param name="self">The current <see cref="VoidResult"/>.</param>
    /// <param name="negativePredicate">A predicate to build the next <see cref="VoidResult"/>. Fail if evaluates to <c>true</c>, success otherwise.</param>
    /// <param name="errorDescription">The error description in case the <paramref name="negativePredicate"/> evaluates to <c>true</c>.</param>
    /// <returns>
    /// <paramref name="self"/> if the current <see cref="VoidResult"/> has failed; 
    /// otherwise, a new <see cref="VoidResult"/> instance based on <paramref name="negativePredicate"/> and <paramref name="errorDescription"/>.
    /// </returns>
    public static VoidResult OtherwiseFailIf(
        this VoidResult self,
        Func<bool> negativePredicate,
        string errorDescription
    ) => !self.IsSuccess
        ? VoidResult.FailIf(negativePredicate, errorDescription)
        : self;
}
