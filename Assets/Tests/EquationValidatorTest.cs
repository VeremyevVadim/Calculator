using Calculator.Infrastructure;
using NUnit.Framework;

public class EquationValidatorTest
{
    private EquationValidator _validator;

    [SetUp]
    public void SetUp() => _validator = new EquationValidator();

    [Test]
    [TestCase("1+1", true)]
    [TestCase("10000000000000000000000000000+11111111111111111111111111111", true)]
    [TestCase("1+2+3+4", true)]
    [TestCase("1", false)]        
    [TestCase("1+", false)]
    [TestCase("+1", false)]
    [TestCase("1-1", false)]
    [TestCase("a+1", false)]
    [TestCase("1++1", false)]
    [TestCase("1.5+1", false)]
    public void Validate_CheckInputs(string input, bool expected)
    {
        Assert.AreEqual(expected, _validator.Validate(input));
    }
}
