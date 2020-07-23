using System;
using System.Collections.Generic;
using System.Text;
using Loans.Domain.Applications;
using NUnit.Framework;


namespace Loans.Tests
{

    public class LoanRepaymentCalculatorShould
    {
        [Test]
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        [TestCase(500_000, 10, 30, 4387.86)]
        public void CalculateCorrectMonthlyRepayment(decimal principal, decimal interestrate, int termYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestrate, new LoanTerm(termYears));
            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }



        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        [TestCase(500_000, 10, 30, ExpectedResult = 4387.86)]
        public decimal CalculateCorrectMonthlyRepayment_simplifiedTestCase(decimal principal, decimal interestrate, int termYears)
        {
            var sut = new LoanRepaymentCalculator();
            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestrate, new LoanTerm(termYears));

        }





        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        public void CalculateCorrectMonthlyRepayment_Centralized(decimal principal, decimal interestrate, int termYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestrate, new LoanTerm(termYears));
            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }



        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestCases")]
        public decimal CalculateCorrectMonthlyRepayment_CentralizedwithReturn(decimal principal, decimal interestrate, int termYears)
        {
            var sut = new LoanRepaymentCalculator();
            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestrate, new LoanTerm(termYears));


        }



        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentCSVData), "GetTestCases", new object[]{"Data.csv"})]
        public void CalculateCorrectMonthlyRepayment_Csv(decimal principal, decimal interestrate, int termYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestrate, new LoanTerm(termYears));
            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }
        [Test]

        public void calculateCorrectMonthlyRepayment_Combinatorial(
           [Values(100_000, 200_000, 500_000)] decimal principal,
           [Values(6.5, 10, 20)] decimal interestRate,
           [Values(10, 20, 30)] int termYears)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termYears));


        }

        [Test]
        [Sequential]
        public void calculateCorrectMonthlyRepayment_Sequential(
         [Values(200_000, 200_000, 500_000)] decimal principal,
         [Values(6.5, 10, 10)] decimal interestRate,
         [Values(30, 30, 30)] int termYears,
         [Values(1264.14,1755.14,4387.86)]decimal ExpectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termYears));

            Assert.That(monthlyPayment, Is.EqualTo(ExpectedMonthlyPayment));


        }


        [Test]

        public void calculateCorrectMonthlyRepayment_Range(
          [Range(50_000, 1_000_000, 50_000)] decimal principal,
          [Range(0.5, 20.00, 0.5)] decimal interestRate,
          [Values(10, 20, 30)] int termYears)
        {
            var sut = new LoanRepaymentCalculator();
            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termYears));


        }
    }
}
