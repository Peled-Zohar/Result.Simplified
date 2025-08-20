using NUnit.Framework;
using System;


namespace Result.Simplified.Tests;

[TestFixture]
class ResultConditionalFactoryMethods
{
    [Test]
    public void Result_SuccessIf_PredicateIsTrueReturnSuccess()
    {
        var result = VoidResult.SuccessIf(() => true, "failed");
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Result_SuccessIf_ExpressionIsTrueReturnSuccess()
    {
        var result = VoidResult.SuccessIf(1 == 1, "failed");
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Result_SuccessIf_PredicateIsTrueErrorDescriptionIsNull()
    {
        var result = VoidResult.SuccessIf(() => true, "failed");
        Assert.That(result.ErrorDescription, Is.Null);
    }

    [Test]
    public void Result_SuccessIf_ExpressionIsTrueErrorDescriptionIsNull()
    {
        var result = VoidResult.SuccessIf(1 == 1, "failed");
        Assert.That(result.ErrorDescription, Is.Null);
    }

    [Test]
    public void Result_SuccessIf_PredicateIsFalseReturnFail()
    {
        const string errorDescription = "failed";
        var result = VoidResult.SuccessIf(() => false, errorDescription);
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Result_SuccessIf_ExpressionIsFalseReturnFail()
    {
        const string errorDescription = "failed";
        var result = VoidResult.SuccessIf(1 == 2, errorDescription);
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Result_SuccessIf_PredicateIsFalseReturnCorrectErrorMessage()
    {
        const string errorDescription = "failed";
        var result = VoidResult.SuccessIf(() => false, errorDescription);
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [Test]
    public void Result_SuccessIf_ExpressionIsFalseReturnCorrectErrorMessage()
    {
        const string errorDescription = "failed";
        var result = VoidResult.SuccessIf(1 == 2, errorDescription);
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [Test]
    public void Result_SuccessIf_PredicateIsNull_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => VoidResult.SuccessIf(null, "failed"));
    }


    [Test]
    public void Result_FailIf_PredicateIsFalseReturnSuccess()
    {
        var result = VoidResult.FailIf(() => false, "failed");
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Result_FailIf_ExpressionIsFalseReturnSuccess()
    {
        var result = VoidResult.FailIf(1 == 2, "failed");
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void Result_FailIf_PredicateIsFalseErrorDescriptionIsNull()
    {
        var result = VoidResult.FailIf(() => false, "failed");
        Assert.That(result.ErrorDescription, Is.Null);
    }
    
    [Test]
    public void Result_FailIf_ExpressionIsFalseErrorDescriptionIsNull()
    {
        var result = VoidResult.FailIf(1 == 2, "failed");
        Assert.That(result.ErrorDescription, Is.Null);
    }

    [Test]
    public void Result_FailIf_PredicateIsTrueReturnFail()
    {
        const string errorDescription = "failed";
        var result = VoidResult.FailIf(() => true, errorDescription);
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Result_FailIf_ExpressionIsTrueReturnFail()
    {
        const string errorDescription = "failed";
        var result = VoidResult.FailIf(1 == 1, errorDescription);
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void Result_FailIf_PredicateIsTrueReturnCorrectErrorMessage()
    {
        const string errorDescription = "failed";
        var result = VoidResult.FailIf(() => true, errorDescription);
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [Test]
    public void Result_FailIf_ExpressionIsTrueReturnCorrectErrorMessage()
    {
        const string errorDescription = "failed";
        var result = VoidResult.FailIf(1 == 1, errorDescription);
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [Test]
    public void Result_FailIf_PredicateIsNull_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => VoidResult.FailIf(null, "failed"));
    }

}
