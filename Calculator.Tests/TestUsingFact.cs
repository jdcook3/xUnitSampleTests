using System;
using Xunit;

namespace Calculator.Tests
{
    public class TestUsingFact
    {
        [Fact]
        public void Add()
        {
            double val1 = 2;
            double val2 = 3;
            string op = "a";
            double result = 5;
            double output;

            output = CalculatorClass.DoOperation(val1, val2, op);
            Assert.Equal(result, output);
        }
    }
}
