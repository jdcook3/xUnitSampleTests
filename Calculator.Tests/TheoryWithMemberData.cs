using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Tests
{

    /* This is probably my preferred method if you really don't want to do InlineData.
     * Here, you use MemberData to call on a property or method within the same class as the test.
     * This lets you pair data to tests more easily than ClassData, but 
     * still allows the pairing of properties/methods from other classes using:
     * MemberType = typeOf()
     */
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
                new object[] { 6, 3, "d", 2 }
            };
            // If you set parameters, you should use the Linq.Take Method.
            return allTestData.Take(testCount);
        }

        // First is an example of calling memberdata from a method within the class. 
        // Here is an example of MemberData calling data from another class. While it is a bit more verbose than just using class data directly, it allows you to not implement IEnumerable at the class level.
        // You can stack these, interchangeably with other xUnit test data attributes.
        [Theory]
        [MemberData(nameof(myDataMethod), parameters: 4)]
        [MemberData(nameof(TheoryWithMemberDataProperty.myMemberData), MemberType = typeof(TheoryWithMemberDataProperty))]
        [ClassData(typeof(CalculatorTestData))] // This is calling input data from the TheoryWithClassData file. It will most likely skip due to matching test signatures.
        public void Test1(double val1, double val2, string op, double result)
        {
            double output;
            output = CalculatorClass.DoOperation(val1, val2, op);

            Assert.Equal(result, output);
        }
    }
}
