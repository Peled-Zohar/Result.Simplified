using NUnit.Framework;
using System;

namespace Result.Simplified.Tests;

class ResultFactoryMethods
{
    private const string errorDescription = "Fail";
    [Test]
    public void ResultFail_ValidErrorDescription_GeneratesAFailedResult()
    {
        var result = VoidResult.Fail(errorDescription);
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public void ResultFail_ValidErrorDescription_GeneratesCorrectErrorMessage()
    {
        var result = VoidResult.Fail(errorDescription);
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [Test]
    public void ResultFail_NullErrorDescription_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => VoidResult.Fail(null));        
    }

    [Test]
    public void ResultFail_EmptyErrorDescription_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => VoidResult.Fail(""));
    }

    [Test]
    public void ResultFail_WhitespaceErrorDescription_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => VoidResult.Fail(" "));
    }

    [Test]
    public void ResultSuccess_GeneratesASuccessfulResult()
    {
        var result = VoidResult.Success();
        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void ResultSuccess_GeneratesAResultWithNullErrorDescription()
    {
        var result = VoidResult.Success();
        Assert.That(result.ErrorDescription, Is.Null);
    }
}
