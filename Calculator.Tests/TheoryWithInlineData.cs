using System;
using Xunit;

namespace Calculator.Tests
{
    public class TheoryWithInlineData
    {
        /* Use theory when you want to pass parameters. The simplest way is listed here:
         * InlineData is the most direct way to pass parameters into the Test Method signature.
         * 1 big issue is that you can not call methods to get parameter (such as xdocument loading of filepaths here).
         * For this, you need to pass them through an IEnumerable<> property, class, or method. 
         * See the other files for examples of multiple workflows.
         */
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
