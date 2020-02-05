using System;
using Xunit;

namespace Calculator.Tests
{
    public class TheoryWithInlineData
    {
        [Theory]
        [InlineData(2, 3, "+", 5)]
        [InlineData(2, 3, "-", -1)]
        [InlineData(2, 3, "*", 6)]
        [InlineData(6, 3, "/", 2)]
        public void Test1(double val1, double val2, string op, double result)
        {
            double output;
            output = CalculatorClass.DoOperation(val1, val2, op);

            Assert.Equal(result, output);
        }
    }
}
