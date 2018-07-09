using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialLogic
{
    /// <summary>
    /// Class for work with polynomials
    /// </summary>
    public sealed class Polynomial:ICloneable
    {
        private readonly double accurancy = 0.0000001;

        /// <summary>
        /// The coefficients
        /// </summary>
        private readonly double[] coefficients;

        public Polynomial(double[] coefficients)
        {
            this.coefficients = new double[coefficients.Length];
            Array.Copy(coefficients, this.coefficients, coefficients.Length);
        }

        /// <summary>
        /// Gets the polynomial power.
        /// </summary>
        /// <value>
        /// The polynomial power.
        /// </value>
        public int PolynomialPower => coefficients.Length;

        /// <summary>
        /// Gets the <see cref="System.Double"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="System.Double"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns><see cref="System.Double"/></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public double this[int index]
        {
            get
            {
                if (index > PolynomialPower || index < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return coefficients[index];
            }

            private set => coefficients[index] = value;
        }

        /// <summary>
        /// To the array.
        /// </summary>
        /// <returns>Array of coefficents</returns>
        public double[] ToArray()
        {

            int n = PolynomialPower;

            var temp = new double[n];

            Array.Copy(coefficients, temp, n);

            return temp;

        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            int n = PolynomialPower;

            for (int i = 0; i <= n; i++)
            {
                sb.Append(this[i] + "a^" + (n - i));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this,obj))
            {
                return true;
            }

            if (obj ==null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            Polynomial p = (Polynomial)obj;

            int n = PolynomialPower;

            if (n != p.PolynomialPower)
            {
                return false;
            }


            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(this[i] - p[i]) > accurancy)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        object ICloneable.Clone()
        {
            return new Polynomial(ToArray());
        }

        public Polynomial Clone()
        {
            return new Polynomial(ToArray());
        }

        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            return lhs != null && lhs.Equals(rhs);
        }

        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            return !(lhs==rhs);
        }

        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            int itemsCount = Math.Max(lhs.PolynomialPower, rhs.PolynomialPower);
            var result = new double[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                double first = 0;
                double second = 0;

                if (i < lhs.PolynomialPower)
                {
                    first = lhs[i];
                }
                if (i < rhs.PolynomialPower)
                {
                    second = rhs[i];
                }

                result[i] = first + second;
            }

            return new Polynomial(result);
        }

        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            return lhs+(-rhs);
        }

        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            int itemsCount = lhs.PolynomialPower + rhs.PolynomialPower - 1;
            var result = new double[itemsCount];

            for (int i = 0; i < lhs.PolynomialPower; i++)
            {
                for (int j = 0; j < rhs.PolynomialPower; j++)
                {
                    result[i + j] += lhs[i] * rhs[j];
                }
            }

            return new Polynomial(result);
        }

        public static Polynomial operator -(Polynomial p)
        {
            int n = p.PolynomialPower;

            var result = new double[p.PolynomialPower];

            for (int i = 0; i < n; i++)
            {
                result[i] = -1 * p[i];
            }

            return new Polynomial(result);
        }

    }
}
