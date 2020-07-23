using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests
{
    [TestFixture]
    public class productsComparerShould { 


    [OneTimeSetUp]
    public void OneTimeSetUp()
    { 
    products = new List<LoanProduct>
         {
             new LoanProduct(1,"a",1),
             new LoanProduct(2,"b",2),
             new LoanProduct(3,"c",3)

         };
    }

        [OneTimeTearDown]

        public void OneTimeTearDown()
        { 
        
        
        }

        private List<LoanProduct> products;
        private ProductComparer sut;



        [SetUp]
        public void SetUp()
        {
            

            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }


        [TearDown]
        public void TearDown()
        { 
          //sut.Dispose();
        }
        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));


            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicateComparisons()
        {
              
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));
            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonsForFirstProduct()
        {     
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);
            Assert.That(comparisons, Does.Contain(expectedProduct));
        }


        [Test]
        public void ReturnComparisonsForFirstProductWithPartialKNownExpectedValues()
        {
         
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

           
            Assert.That(comparisons, Has.Exactly(1)
                                        .Property("ProductName").EqualTo("a")
                                        .And
                                         .Property("InterestRate").EqualTo(1)
                                         .And
                                         .Property("MonthlyRepayment").GreaterThan(0));

            Assert.That(comparisons, Has.Exactly(1)
                                        .Matches<MonthlyRepaymentComparison>(
                                          item => item.ProductName=="a"&&
                                          item.InterestRate==1&&
                                          item.MonthlyRepayment>0 ));
        }
    }
}
