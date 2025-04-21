using NUnit.Framework;

namespace Result.Simplified.Tests
{
    class ResultBooleanOperatorUnitTests
    {
        private Result _fail, _success;
        private Result<int> _failOfT, _successOfT;

        [SetUp]
        public void Setup()
        {
            _fail = Result.Fail("fail");
            _success = Result.Success();
            _failOfT = Result<int>.Fail("fail");
            _successOfT = Result<int>.Success(1);
        }

        [Test]
        public void Result_FalseOperator_ReturnsFalseOnFailure()
        {
            if(_fail)
            {
                Assert.Fail("Fail result should have returned false.");
            }
            else
            {
                Assert.Pass();
            }
        }

        [Test]
        public void Result_TrueOperator_ReturnsTrueOnSuccess()
        {
            if (_success)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Success result should have returned true."); 
            }
        }

        [Test]
        public void ResultOfT_FalseOperator_ReturnsFalseOnFailure()
        {
            if (_failOfT)
            {
                Assert.Fail("Fail result should have returned false.");
            }
            else
            {
                Assert.Pass();
            }
        }

        [Test]
        public void ResultOfT_TrueOperator_ReturnsTrueOnSuccess()
        {
            if (_successOfT)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Success result should have returned true.");
            }
        }
    }
}
