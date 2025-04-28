using NUnit.Framework;
using System;

namespace Result.Simplified.Tests
{
    class ResultOfTFactoryMethods
    {
        [Test]
        public void ResultOfTFail_ValidErrorDescription_GeneratesAFailedResult()
        {
            const string errorDescription = "Fail";
            var result = Result<int>.Fail(errorDescription);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void ResultOfTFail_ValidErrorDescriptionWithValue_GeneratesAFailedResult()
        {
            const string errorDescription = "Fail";
            const int value = 1;
            var result = Result<int>.Fail(errorDescription, value);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void ResultOfTFail_ValidErrorDescription_GeneratesCorrectErrorMessage()
        {
            const string errorDescription = "Fail";
            var result = Result<int>.Fail(errorDescription);
            Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
        }

        [Test]
        public void ResultOfTFail_ValidErrorDescriptionWithValue_GeneratesCorrectErrorMessage()
        {
            const string errorDescription = "Fail";
            const int value = 1;
            var result = Result<int>.Fail(errorDescription, value);
            Assert.That(result.ErrorDescription, Is.EqualTo(errorDescription));
        }

        [Test]
        public void ResultOfTFail_ValidErrorDescriptionWithValue_GeneratesCorrectValue()
        {
            const string errorDescription = "Fail";
            const int value = 1;
            var result = Result<int>.Fail(errorDescription, value);
            Assert.That(result.Value, Is.EqualTo(value));
        }

        [Test]
        public void ResultOfTFail_NullErrorDescription_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Result<int>.Fail(null));
        }

        [Test]
        public void ResultOfTFail_EmptyErrorDescription_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Result<int>.Fail(""));
        }

        [Test]
        public void ResultOfTFail_WhitespaceErrorDescription_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Result<int>.Fail(" "));
        }

        [Test]
        public void ResultOfTSuccess_GeneratesASuccessfulResult()
        {
            var result = Result<int>.Success(1);
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public void ResultOfTSuccess_GeneratesAResultWithNullErrorDescription()
        {
            var result = Result<int>.Success(1);
            Assert.That(result.ErrorDescription, Is.Null);
        }

        [Test]
        public void ResultOfTSuccess_GeneratesAResultWithCorrectValue()
        {
            const int value = 1;
            var result = Result<int>.Success(value);
            Assert.That(result.Value, Is.EqualTo(value));
        }

        [Test]
        public void ResultOfTSuccess_NullValueGeneratesAResultWithNullValue()
        {
            var result = Result<string>.Success(null);
            Assert.That(result.Value, Is.Null);
        }
    }
}
