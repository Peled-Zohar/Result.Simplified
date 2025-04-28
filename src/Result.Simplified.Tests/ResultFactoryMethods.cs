using NUnit.Framework;
using System;

namespace Result.Simplified.Tests;

class ResultFactoryMethods
{
    [Test]
    public void ResultFail_ValidErrorDescription_GeneratesAFailedResult()
    {
        const string errorDescription = "Fail";
        var result = Result.Fail(errorDescription);
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void ResultFail_ValidErrorDescription_GeneratesCorrectErrorMessage()
    {
        const string errorDescription = "Fail";
        var result = Result.Fail(errorDescription);
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [Test]
    public void ResultFail_NullErrorDescription_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Result.Fail(null));        
    }

    [Test]
    public void ResultFail_EmptyErrorDescription_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Result.Fail(""));
    }

    [Test]
    public void ResultFail_WhitespaceErrorDescription_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Result.Fail(" "));
    }

    [Test]
    public void ResultSuccess_GeneratesASuccessfulResult()
    {
        var result = Result.Success();
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void ResultSuccess_GeneratesAResultWithNullErrorDescription()
    {
        var result = Result.Success();
        Assert.That(result.ErrorDescription, Is.Null);
    }
}
