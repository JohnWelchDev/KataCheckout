using KataCheckout.Logic.Utilities;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OperatorUtilityTests
{
    [TestFixture]
    public class EvaluateTests
    {
        [TestCase(1, "=", 1, true, TestName = "Equals - pass")]
        [TestCase(1, "=", 2, false, TestName = "Equals - fail")]
        [TestCase(1, "!=", 1, false, TestName = "Not Equals - pass")]
        [TestCase(1, "!=", 2, true, TestName = "Not Equals - fail")]
        [TestCase(5, ">", 2, true, TestName = "Greater Than - pass")]
        [TestCase(5, ">", 5, false, TestName = "Greater Than - fail")]
        [TestCase(2, ">=", 1, true, TestName = "Greater Than or Equal - pass greater than")]
        [TestCase(2, ">=", 2, true, TestName = "Greater Than or Equal - pass equal")]
        [TestCase(2, ">=", 3, false, TestName = "Greater Than or Equal - fail")]
        [TestCase(1, "<", 2, true, TestName = "Less Than - pass")]
        [TestCase(1, "<", 1, false, TestName = "Less Than - fail")]
        [TestCase(1, "<=", 2, true, TestName = "Less Than or Equal - pass less than")]
        [TestCase(1, "<=", 1, true, TestName = "Less Than or Equal - pass equal")]
        [TestCase(2, "<=", 1, false, TestName = "Less Than or Equal - fail")]
        public void EvaluatesCorrectly(int subject, string operatorCode, int comparison, bool expectedResult)
        {
            this.ExecuteTest(subject, operatorCode, comparison, expectedResult);
        }

        [TestCase(1, "XOR", 3, typeof(InvalidOperationException), "Invalid operator code", TestName = "Invalid Operator 1")]
        [TestCase(1, ">>", 3, typeof(InvalidOperationException), "Invalid operator code", TestName = "Invalid Operator 2")]
        [TestCase(1, "|=", 3, typeof(InvalidOperationException), "Invalid operator code", TestName = "Invalid Operator 3")]
        public void ErrorsOnInvalidOperator(int subject, string operatorCode, int comparison, Type exceptionType, string exceptionMessage)
        {
            try
            {
                bool result = OperatorUtility.Evaluate(subject, operatorCode, comparison);

                Assert.Fail($"Exception was expected to be thrown - returned result {result}");
            }
            catch (Exception ex)
            {
                // check exception type.
                Assert.That(ex.GetType(), Is.EqualTo(exceptionType), "Unexpected exception type");

                Assert.That(ex.Message, Is.EqualTo(exceptionMessage), "Unexpected error message");
            }
        }

        /// <summary>
        /// Executes test.
        /// </summary>
        /// <param name="subject">The subject value.</param>
        /// <param name="operatorCode">The operator code.</param>
        /// <param name="comparison">the comparison value.</param>
        /// <param name="expectedResult">The expected result.</param>
        private void ExecuteTest(int subject, string operatorCode, int comparison, bool expectedResult)
        {
            bool result = OperatorUtility.Evaluate(subject, operatorCode, comparison);
        }
    }
}
