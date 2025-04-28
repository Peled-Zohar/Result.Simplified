using NUnit.Framework;
using System;

namespace Result.Simplified.Tests;

class ResultOrUnitTests
{

    private Result _fail1, _fail2, _fail3,
        _success1, _success2, _success3;

    [SetUp]
    public void Setup()
    {
        _fail1 = Result.Fail("Fail 1");
        _fail2 = Result.Fail("Fail 2");
        _fail3 = Result.Fail("Fail 3");

        _success1 = Result.Success();
        _success2 = Result.Success();
        _success3 = Result.Success();
    }

    #region | operator

    [Test]
    public void OrOperator_NullAndSuccess_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => { var result = null | _success1; });
    }

    [Test]
    public void OrOperator_NullAndFail_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => { var result = null | _fail1; });
    }

    [Test]
    public void OrOperator_SuccessAndNull_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => { var result = _success1 | null; });
    }

    [Test]
    public void OrOperator_FailAndNull_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => { var result = _fail1 | null; });
    }

    [Test]
    public void OrOperator_failAndSuccess()
    {
        var result = _fail1 | _success1;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrOperator_successAndFail()
    {
        var result = _success1 | _fail1;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrOperator_successAndSuccess()
    {
        var result = _success1 | _success2;

        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrOperator_failAndFail()
    {
        var result = _fail1 | _fail2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(ReferenceEquals(result, _fail2));
        Assert.That(!result.IsSuccess);
    }

    [Test]
    public void OrOperator_successAndFailAndFail()
    {
        var result = _success1 | _fail1 | _fail2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _fail2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrOperator_failAndSuccessAndFail()
    {
        var result = _fail1 | _success1 | _fail2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _fail2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrOperator_failAndFailAndSuccess()
    {
        var result = _fail1 | _fail2 | _success1;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _fail2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrOperator_successAndSuccessAndFail()
    {
        var result = _success1 | _success2 | _fail1;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrOperator_successAndFailAndSuccess()
    {
        var result = _success1 | _fail1 | _success2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrOperator_failAndSuccessAndSuccess()
    {
        var result = _fail1 | _success1 | _success2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrOperator_successAndSuccessAndSuccess()
    {
        var result = _success1 | _success2 | _success3;

        Assert.That(!ReferenceEquals(result, _success3));
        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    #endregion | operator

    #region || operator

    [Test]
    public void OrElseOperator_failAndSuccess()
    {
        var result = _fail1 || _success1;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_successAndFail()
    {
        var result = _success1 || _fail1;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_successAndSuccess()
    {
        var result = _success1 || _success2;

        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_failAndFail()
    {
        var result = _fail1 || _fail2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(ReferenceEquals(result, _fail2));
        Assert.That(!result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_successAndFailAndFail()
    {
        var result = _success1 || _fail1 || _fail2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _fail2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_failAndSuccessAndFail()
    {
        var result = _fail1 || _success1 || _fail2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _fail2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_failAndFailAndSuccess()
    {
        var result = _fail1 || _fail2 || _success1;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _fail2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_successAndSuccessAndFail()
    {
        var result = _success1 || _success2 || _fail1;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_successAndFailAndSuccess()
    {
        var result = _success1 || _fail1 || _success2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_failAndSuccessAndSuccess()
    {
        var result = _fail1 || _success1 || _success2;

        Assert.That(!ReferenceEquals(result, _fail1));
        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    [Test]
    public void OrElseOperator_successAndSuccessAndSuccess()
    {
        var result = _success1 || _success2 || _success3;

        Assert.That(!ReferenceEquals(result, _success3));
        Assert.That(!ReferenceEquals(result, _success2));
        Assert.That(ReferenceEquals(result, _success1));
        Assert.That(result.IsSuccess);
    }

    #endregion || operator
}
