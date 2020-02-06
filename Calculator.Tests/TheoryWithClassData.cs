using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;

namespace Calculator.Tests
{
    /* The ClassData attribute is useful when you wish to load the data via another class.
     * The good thing is that you can introduce logic here that helps build your parameters
     * (unlike InlineData). The important thing to remember is that your class MUST implement the IEnumerable interface,
     * and the GetEnumerator method should return all of the sets of parameters you wish to test.
     */

    public class CalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // Most examples I've found use yield here. If needed, you can call helper methods inside of the arrays.
            // NOTE: This allows for non-typed arg input. If you want to set up strongly typed parameters, see TheoryData.
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
        // ClassData uses typeof, and calls another class. 
        // This is good if you wish to share data between tests, but I can't seem to find a situation for that to make sense.
        // Another pro is that you can group inputs for a project inside of one class (again, not sure that's best).
        // The biggest con here that I can see is that you can VERY easily lose track of the data that pairs with a test.
        [ClassData(typeof(CalculatorTestData))]
        public void Test1(double val1, double val2, string op, double result)
        {
            double output;
            output = CalculatorClass.DoOperation(val1, val2, op);

            Assert.Equal(result, output);
        }
    }
}
