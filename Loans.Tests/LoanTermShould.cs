using NUnit.Framework;
using System;
using Loans.Domain.Applications;
using System.Collections.Generic;

namespace Loans.Tests
{

 
    [Category("Years check")]
    [Category("Loan Check")]
    public class LoanTermShould
    {
        [Test]
       [Category("Loan Check")]
        public void ReturnTermInMonths()
        {
            //Arrange : Initialise test data
            var sut = new LoanTerm(1);

            //Act :  Call methods
            var numberofmonths = sut.ToMonths();

            //Assert
            Assert.That(numberofmonths, Is.EqualTo(12),"Months should be 12* number of Years");
        }

        [Test]
       
        public void StoreYears()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueEquality()
        {
            var a = new LoanTerm(1);
           var  b = new LoanTerm(1);

            Assert.That(a, Is.EqualTo(b));
        
        }

        [Test]
        public void RespectValueInEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);

            Assert.That(a, Is.Not.EqualTo(b));

        }

        [Test]
        public void ReferenceEqualiyExample()
        {
            var a = new LoanTerm(1);
            var b = a;//

            var c = new LoanTerm(1);


            Assert.That(a, Is.SameAs(b));//comparing if a and b point to the same object in memory
            Assert.That(a, Is.Not.SameAs(c));

            var x = new List<string> { "a","b","c"};
            var z= new List<string> { "a", "b", "c" };
            var y = x;


            Assert.That(x,Is.SameAs(y));
            Assert.That(z, Is.Not.SameAs(x));

        }
        [Test]

        public void Double()
        {
            double a = 1.0 / 3.0;

            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }
        [Test]
        public void NotAllowZeroYears()
        {
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());




            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>()
                                    .With
                                    .Matches<ArgumentOutOfRangeException>(
                                    ex => ex.ParamName == "years"));
        }

    }
}
