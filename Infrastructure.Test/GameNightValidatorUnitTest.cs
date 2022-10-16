using System;
namespace Infrastructure.Test
{
    public class GameNightValidatorUnitTest
    {
        [Fact]
        public void Date_In_The_Past_Returns_False()
        {
            // Arrange
            var valid = new GameNightValidator();
            DateTime date = new DateTime(1998, 07, 08);

            // Act

            bool truth = valid.DateInPresent(date);

            // Assert

            Assert.False(truth);
        }

        [Fact]
        public void Date_In_The_Future_Returns_True()
        {
            // Arrange
            var valid = new GameNightValidator();
            DateTime date = new DateTime(2023, 07, 08);

            // Act

            bool truth = valid.DateInPresent(date);

            // Assert

            Assert.True(truth);
        }
    }
}

