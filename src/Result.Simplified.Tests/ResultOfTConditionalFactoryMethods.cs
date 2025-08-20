using NUnit.Framework;
using System;

namespace Result.Simplified.Tests;

public class ResultOfTConditionalFactoryMethods
{
    private const int value = 5;
    private const string errorDescription = "failed";

    [TestCase(true)]
    [TestCase(false)]
    public void SuccessIf_PredicateIsTrue_ReturnSuccess(bool includeValue)
    {
        
        var result = Result<int>.SuccessIf(x => x == value, value, errorDescription, includeValue);
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.EqualTo(value));
        Assert.That(result.ErrorDescription, Is.Null);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void SuccessIf_ExpressionIsTrue_ReturnSuccess(bool includeValue)
    {

        var result = Result<int>.SuccessIf(1 == 1, value, errorDescription, includeValue);
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.EqualTo(value));
        Assert.That(result.ErrorDescription, Is.Null);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void SuccessIf_PredicateIsFalse_ReturnFail(bool includeValue)
    {
        var result = Result<int>.SuccessIf(x => x != value, value, errorDescription, includeValue);
        Assert.That(result.IsSuccess, Is.False);
        if (includeValue)
        {
            Assert.That(result.Value, Is.EqualTo(value));
        }
        else
        {
            Assert.That(result.Value, Is.EqualTo(default(int)));
        }
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void SuccessIf_ExpressionIsFalse_ReturnFail(bool includeValue)
    {
        var result = Result<int>.SuccessIf(1 == 2, value, errorDescription, includeValue);
        Assert.That(result.IsSuccess, Is.False);
        if (includeValue)
        {
            Assert.That(result.Value, Is.EqualTo(value));
        }
        else
        {
            Assert.That(result.Value, Is.EqualTo(default(int)));
        }
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void SuccessIf_PredicateIsNull_Throw(bool includeValue)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            Result<int>.SuccessIf(null, value, errorDescription, includeValue);
        });
    }

    [TestCase(true)]
    [TestCase(false)]
    public void FailIf_PredicateIsFalse_ReturnSuccess(bool includeValue)
    {

        var result = Result<int>.FailIf(x => x != value, value, errorDescription, includeValue);
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.EqualTo(value));
        Assert.That(result.ErrorDescription, Is.Null);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void FailIf_ExpressionIsFalse_ReturnSuccess(bool includeValue)
    {

        var result = Result<int>.FailIf(1 == 2, value, errorDescription, includeValue);
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.EqualTo(value));
        Assert.That(result.ErrorDescription, Is.Null);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void FailIf_PredicateIsTrue_ReturnFail(bool includeValue)
    {
        var result = Result<int>.FailIf(x => x == value, value, errorDescription, includeValue);
        Assert.That(result.IsSuccess, Is.False);
        if (includeValue)
        {
            Assert.That(result.Value, Is.EqualTo(value));
        }
        else
        {
            Assert.That(result.Value, Is.EqualTo(default(int)));
        }
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void FailIf_ExpressioneIsTrue_ReturnFail(bool includeValue)
    {
        var result = Result<int>.FailIf(1 == 1, value, errorDescription, includeValue);
        Assert.That(result.IsSuccess, Is.False);
        if (includeValue)
        {
            Assert.That(result.Value, Is.EqualTo(value));
        }
        else
        {
            Assert.That(result.Value, Is.EqualTo(default(int)));
        }
        Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void FailIf_PredicateIsNull_Throw(bool includeValue)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            Result<int>.FailIf(null, value, errorDescription, includeValue);
        });
    }
}
