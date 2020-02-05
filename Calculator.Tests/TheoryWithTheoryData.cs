using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Tests
{
    public class TheoryWithMemberDataFromTheoryData
    {
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

        public static TheoryData<double, double, string, double> CleanerMemberDataFromTheoryData =>
            new TheoryData<double, double, string, double>
            {
                { 2, 3, "+", 5 },
                { 2, 3, "-", -1 },
                { 2, 3, "*", 6 },
                { 6, 2, "/", 3 },
            };

        [Theory]
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

    public class ClassDataFromTheoryData : TheoryData<double, double, string, double>
    {
        public ClassDataFromTheoryData()
        {
            Add(2, 3, "+", 5);
            Add(2, 3, "-", -1);
            Add(2, 3, "*", 6);
            Add(6, 2, "/", 3);
            //Add("string", 8.1, "operator here", false); // This should throw errors.
        }
    }



    public class TheoryData<T1, T2, T3, T4> : TheoryData
    {
        public void Add(T1 param1, T2 param2, T3 param3, T4 param4)
        {
            AddRow(param1, param2, param3, param4);
        }
    }

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
