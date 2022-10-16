namespace Infrastructure.Test;
using Infrastructure;

public class PersonValidatorUnitTest
{
    [Fact]
    public void Age_Older_Than_18_Returns_True()
    {
        // Arrange
        var valid = new PersonValidator();
        DateTime age = new DateTime(1998,07,08);

        // Act

        bool truth = valid.CheckAge(age);

        // Assert

        Assert.True(truth);
    }

    [Fact]
    public void Age_Younger_Than_18_Returns_False()
    {
        // Arrange
        var valid = new PersonValidator();
        DateTime age = new DateTime(2009,05,09);

        // Act

        bool truth = valid.CheckAge(age);

        // Assert

        Assert.False(truth);
    }


}
