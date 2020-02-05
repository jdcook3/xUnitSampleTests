using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Tests
{
    public class TheoryWithMemberDataProperty
    {
        public static IEnumerable<object[]> myMemberData =>
        new List<object[]>
        {
            new object[] { 2, 3, "+", 5 },
            new object[] { 2, 3, "-", -1 },
            new object[] { 2, 3, "*", 6 },
            new object[] { 6, 3, "/", 2 },
        };

        [Theory]
        [MemberData(nameof(myMemberData))]
        public void Test1(double val1, double val2, string op, double result)
        {
            double output;
            output = CalculatorClass.DoOperation(val1, val2, op);

            Assert.Equal(result, output);
        }
    }

    public class TheoryWithMemberDataMethod
    {
        public static IEnumerable<object[]> myDataMethod(int testCount)
        {
            var allTestData = new List<object[]>
            {
                new object[] { 2, 3, "a", 5 },
                new object[] { 2, 3, "s", -1 },
                new object[] { 2, 3, "m", 6 },
                new object[] { 6, 3, "d", 2 },
            };

            return allTestData.Take(testCount);
        }

        [Theory]
        [MemberData(nameof(myDataMethod), parameters: 4)]
        public void Test1(double val1, double val2, string op, double result)
        {
            double output;
            output = CalculatorClass.DoOperation(val1, val2, op);

            Assert.Equal(result, output);
        }
    }
}
