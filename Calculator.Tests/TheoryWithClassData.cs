using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;

namespace Calculator.Tests
{
    public class CalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 2, 3, "+", 5 };
            yield return new object[] { 2, 3, "-", -1 };
            yield return new object[] { 2, 3, "*", 6 };
            yield return new object[] { 6, 3, "/", 2 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    
    public class TheoryWithClassData
    {
        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public void Test1(double val1, double val2, string op, double result)
        {
            double output;
            output = CalculatorClass.DoOperation(val1, val2, op);

            Assert.Equal(result, output);
        }
    }
}
