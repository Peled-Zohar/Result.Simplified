using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Result.Simplified.Tests
{
    [TestFixture]
    class ResultConditionalFactoryMethods
    {
        [Test]
        public void Result_SuccessIf_PredicateIsTrueReturnSuccess()
        {
            var result = Result.SuccessIf(() => true, "failed");
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorDescription is null, "error description should be null", null);
        }

        [Test]
        public void Result_SuccessIf_PredicateIsFalseReturnFaile()
        {
            const string errorDescription = "failed";
            var result = Result.SuccessIf(() => false, errorDescription);
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorDescription == errorDescription, $"error description should be {errorDescription}", errorDescription);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ResultOfT_SuccessIf_PredicateIsTrueReturnSuccess(bool includeValue)
        {
            const int value = 5;
            var result = Result<int>.SuccessIf(x => x == value, value, "failed", includeValue);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value == value, $"result's value should be {value}");
            Assert.That(result.ErrorDescription is null, "error description should be null", null);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ResultOfT_SuccessIf_PredicateIsFalseReturnFaile(bool includeValue)
        {
            const int value = 5;
            const string errorDescription = "failed";
            var result = Result<int>.SuccessIf(x => x != value, value, errorDescription, includeValue);
            Assert.That(result.IsSuccess, Is.False);
            if(includeValue)
            {
                Assert.That(result.Value != value, $"result's value should'nt be {value}");
            }
            else
            {
                Assert.That(result.Value == default, "result's value should be default");
            }
            Assert.That(result.ErrorDescription == errorDescription, $"error description should be {errorDescription}", errorDescription);
        }
    }
}
