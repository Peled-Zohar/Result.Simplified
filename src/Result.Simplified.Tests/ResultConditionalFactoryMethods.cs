using NUnit.Framework;
using System;


namespace Result.Simplified.Tests;

[TestFixture]
class ResultConditionalFactoryMethods
{
    [Test]
    public void Result_SuccessIf_PredicateIsTrueReturnSuccess()
    {
        var result = Result.SuccessIf(() => true, "failed");
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Result_SuccessIf_PredicateIsTrueErrorDescriptionIsNull()
    {
        var result = Result.SuccessIf(() => true, "failed");
        Assert.That(result.ErrorDescription, Is.Null);
    }

    [Test]
    public void Result_SuccessIf_PredicateIsFalseReturnFail()
    {
        const string errorDescription = "failed";
        var result = Result.SuccessIf(() => false, errorDescription);
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Result_SuccessIf_PredicateIsFalseReturnCorrectErrorMessage()
    {
        const string errorDescription = "failed";
        var result = Result.SuccessIf(() => false, errorDescription);
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [Test]
    public void Result_SuccessIf_PredicateIsNull_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => Result.SuccessIf(null, "failed"));
    }

    [Test]
    public void Result_FailIf_PredicateIsFalseReturnSuccess()
    {
        var result = Result.FailIf(() => false, "failed");
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Result_FailIf_PredicateIsFalseErrorDescriptionIsNull()
    {
        var result = Result.FailIf(() => false, "failed");
        Assert.That(result.ErrorDescription, Is.Null);
    }

    [Test]
    public void Result_FailIf_PredicateIsTrueReturnFail()
    {
        const string errorDescription = "failed";
        var result = Result.FailIf(() => true, errorDescription);
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Result_FailIf_PredicateIsTrueReturnCorrectErrorMessage()
    {
        const string errorDescription = "failed";
        var result = Result.FailIf(() => true, errorDescription);
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [Test]
    public void Result_FailIf_PredicateIsNull_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => Result.FailIf(null, "failed"));
    }

}
