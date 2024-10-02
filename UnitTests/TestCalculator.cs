using NUnit.Framework;
using Logic.Domain;
using System.Collections;

namespace UnitTests
{
	public class TestCalculator
	{
		[Theory]
		[TestCase(1, 2, 3)]
		[TestCase(-1, -2, -3)]
		[TestCase(-0.5, 0.15, -0.35)]
		public void Addition_Equals(double a, double b, double expectedResult)
		{
			var calculator = new Calculator();
			var result = calculator.Addition(a, b);
			Assert.AreEqual(result, expectedResult);
		}

		private static object[] TestDataSubtraction =
		{
			new object[] { 1, 2, -1 },
			new object[] { -1, -2, 1 },
			new object[] { -0.5, 0.15, -0.65 }
		};

		[Theory]
		[TestCaseSource(nameof(TestDataSubtraction))]
		public void Subtraction_Equals(double a, double b, double expectedResult)
		{
			var calculator = new Calculator();
			var result = calculator.Subtraction(a, b);
			Assert.AreEqual(result, expectedResult);
		}
	}
}