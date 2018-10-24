using System;
using FluentAssertions;
using Syncromatics.Configuration.Validation.Extensions;
using Xunit;

namespace Syncromatics.Configuration.Validation.UnitTests
{

    public class RequiredNonZeroAttribeTests
    {
        private class WithIntNonZero
        {
            [RequiredNonZero]
            public int ShouldNotBeZero { get; set; }
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void Should_validate_reqiured_nonzero_int(int value, bool expectThrow)
        {
            var toTest = new WithIntNonZero
            {
                ShouldNotBeZero = value
            };
            TestThrow(toTest, expectThrow);
        }

        private class WithTimeSpanNonZero
        {
            [RequiredNonZero]
            public TimeSpan ShouldNotBeZero { get; set; }
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void Should_validate_required_nonzero_timespan(long hours, bool expectThrow)
        {
            var value = TimeSpan.FromHours(hours);
            var toTest = new WithTimeSpanNonZero
            {
                ShouldNotBeZero = value
            };
            TestThrow(toTest, expectThrow);
        }

        private static void TestThrow(object toValidate, bool expectThrow)
        {
            var isThrown = false;
            try
            {
                toValidate.EnsureIsValid();
            }
            catch (Exception e)
            {
                isThrown = true;
            }

            isThrown.Should().Be(expectThrow);
        }
    }
}