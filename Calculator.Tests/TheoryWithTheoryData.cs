using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Tests
{
    public class TheoryWithMemberDataFromTheoryData
    {
        /* Using TheoryData allows you to strongly type the parameters (to see an example of how things go wrong, wrap one of the ints in quotes in the other file. You'll see no errors, but the test will throw an exception when you run it).
         * To follow this, I recommend starting at the abstract class at the bottom of the file.
         */
        public static TheoryData<double, double, string, double> MemberDataFromTheoryData
        {
            get
            {
                var memberData = new TheoryData<double, double, string, double>();
                memberData.Add(2, 3, "+", 5);
                memberData.Add(2, 3, "-", -1);
                memberData.Add(2, 3, "*", 6);
                memberData.Add(6, 2, "/", 3);

                return memberData;
            }
        }

        // I like this syntax because it is significantly cleaner, IMO. Using the lambda expression and not implementing the Add allows for a syntax more reminiscent of InlineData, but this way allows you to use methods and classes in a more seamless way. Notice that it is also in the same class, so the data is very closely grouped.
        public static TheoryData<double, double, string, double> CleanerMemberDataFromTheoryData =>
            new TheoryData<double, double, string, double>
            {
                { 2, 3, "+", 5 },
                { 2, 3, "-", -1 },
                { 2, 3, "*", 6 },
                { 6, 2, "/", 3 },
            };

        [Theory]
        // You can use ClassData here, if you wish to load data in other files.
        [MemberData(nameof(CleanerMemberDataFromTheoryData))]
        public void Test1(double val1, double val2, string op, double result)
        {
            double output;
            output = CalculatorClass.DoOperation(val1, val2, op);

            Assert.Equal(result, output);
        }
    }
    
    
    public class TheoryWithClassDataFromTheoryData
    {
        [Theory]
        [ClassData(typeof(ClassDataFromTheoryData))]
        public void Test1(double val1, double val2, string op, double result)
        {
            double output;
            output = CalculatorClass.DoOperation(val1, val2, op);

            Assert.Equal(result, output);
        }
    }

    // Notice that inheriting from TheoryData (set below) allows you to explicitly type your values within the Add.
    public class ClassDataFromTheoryData : TheoryData<double, double, string, double>
    {
        public ClassDataFromTheoryData()
        {
            Add(2, 3, "+", 5);
            Add(2, 3, "-", -1);
            Add(2, 3, "*", 6);
            Add(6, 2, "/", 3);
            //Add("string", 8.1, "operator here", false); // This should throw compiler errors, rather than throwing exceptions at run-time.
        }
    }

    // By inheriting from the non-typed TheoryData, you set requirements on the Add method (allowing for the compiler to throw errors, making test data input easier and safer).
    public class TheoryData<T1, T2, T3, T4> : TheoryData
    {
        public void Add(T1 param1, T2 param2, T3 param3, T4 param4)
        {
            AddRow(param1, param2, param3, param4);
        }
    }


    // Stand up an abstract class that implements IEnumerable, has a readonly List, and implements the AddRow method.
    public abstract class TheoryData : IEnumerable<object[]>
    {
        readonly List<object[]> data = new List<object[]>();
        protected void AddRow(params object[] values)
        {
            data.Add(values);
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
