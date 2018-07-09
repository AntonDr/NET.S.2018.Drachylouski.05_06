namespace NUnitPolynomialTest
{
    using System;
    using System.Linq;
    using PolynomialLogic;
    using NUnit.Framework;

    [TestFixture]
    public class PolynomialTests
    {

        [TestCase(new[] { 1d, 2, 3 }, new[] { 1d, 2, 3 }, new[] { 2d, 4, 6 })]
        [TestCase(new[] { 8d, 2, 3 }, new[] { 1d, 2 }, new[] { 9d,4d, 3 })]
        [TestCase(new[] { 1d, 0, 2}, new[] { -1d,1, 0 }, new[] { 0d,1d,2d})]
        public void AddTestCase(double[] d1, double[] d2, double[] sum)
        {
            var p1 = new Polynomial(d1);
            var p2 = new Polynomial(d2);

            var expected = new Polynomial(sum);

            Polynomial actual = Polynomial.Add(p1,p2);

            Assert.True(actual == expected);
        }

        [TestCase(new[] { 1d, 2d, 33d }, new[] { 1d, 2d, 3d }, new[] { 0d,0d,30d })]
        [TestCase(new[] { 2d, 2, 3 }, new[] { 1d, 2 ,3}, new[] { 1d, 0,0 })]
        [TestCase(new[] { 0d, 2d, 0 }, new[] { -1d, 0 }, new[] { 1d, 2d ,0})]
        public void SubtractionTestCase(double[] d1, double[] d2, double[] sum)
        {
            var p1 = new Polynomial(d1);
            var p2 = new Polynomial(d2);

            var expected = new Polynomial(sum);

            Polynomial actual = p1 - p2;

            Assert.True(actual == expected);
        }

     

        [TestCase(new[] { 5d, 8, 2 }, new[] { 2d, -1 }, new[] { 10d, 11, -4, -2 })]
        [TestCase(new[] { 5d, 0, 0, 2 }, new[] { 1d, -5, 4, 0 }, new[] { 5d, -25, 20, 2, -10, 8, 0 })]
        [TestCase(new[] { 3d, 0, -2 }, new[] { 1d, -2, -8 }, new[] { 3d, -6, -26, 4, 16 })]
        [TestCase(new[] { 3d, 0, -2 }, new[] { 0d }, new[] { 0d,0d,0d })]
        [TestCase(new[] { 3d, 0, -2 }, new[] { 1d }, new[] { 3d, 0, -2 })]
        public void MultiplicationTestCase(double[] d1, double[] d2, double[] mul)
        {
            var p1 = new Polynomial(d1);
            var p2 = new Polynomial(d2);

            var expected = new Polynomial(mul);

            Polynomial actual = p1 * p2;

            Assert.True(actual == expected);
        }

        [TestCase(new[] { 3d, 0, -2 })]
        [TestCase(new[] { -3d, 0, 2 })]
        [TestCase(new[] { 0d })]
        public void NegativeTestCase(double[] coeffs)
        {
            double[] negatedCoeffs = coeffs.Select(v => -v).ToArray();
            Polynomial expected = new Polynomial(negatedCoeffs);

            Polynomial actual = -new Polynomial(coeffs);

            Assert.True(expected == actual);
        }

        [Test]
        public void ConstructorThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new Polynomial(null));
    }
}
