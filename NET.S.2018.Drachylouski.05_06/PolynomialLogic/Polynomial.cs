using System;
using System.Text;

namespace PolynomialLogic
{
    /// <summary>
    /// Class for work with polynomials
    /// </summary>
    public sealed class Polynomial : ICloneable, IEquatable<Polynomial>
    {
        #region Private fields

        private static readonly double accurancy = 0.0000001;

        /// <summary>
        /// The coefficients
        /// </summary>
        private readonly double[] coefficients;

        #endregion

        #region Constructor
        public Polynomial(double[] coefficients)
        {
            Validate(coefficients);
            this.coefficients = new double[coefficients.Length];
            Array.Copy(coefficients, this.coefficients, coefficients.Length);
        }
        #endregion

        #region Properties
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

            private set
            {
                if (index > PolynomialPower || index < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                coefficients[index] = value;
            }
        }
        #endregion

        #region Public methods
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
        #endregion

        #region Interface members

        object ICloneable.Clone() => new Polynomial(ToArray());

        public Polynomial Clone() => new Polynomial(ToArray());

        public bool Equals(Polynomial polynomial)
        {
            if (ReferenceEquals(this, polynomial))
            {
                return true;
            }

            if (ReferenceEquals(polynomial, null))
            {
                return false;
            }

            int n = PolynomialPower;

            if (n != polynomial.PolynomialPower)
            {
                return false;
            }

            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(this[i] - polynomial[i]) > accurancy)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Overrided member
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
                if (Math.Abs(this[i]) < accurancy)
                {
                    continue;
                }
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
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            Polynomial polynomial = (Polynomial)obj;

            return Equals(polynomial);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (accurancy.GetHashCode() * 397) ^ coefficients.GetHashCode();
            }
        }
        #endregion

        #region Overload operation methods
        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            return lhs != null && lhs.Equals(rhs);
        }

        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            return !(lhs == rhs);
        }

        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            Validate(lhs, rhs);

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
            Validate(lhs, rhs);
            return lhs + (-rhs);
        }

        public static Polynomial operator *(Polynomial lhs, Polynomial rhs)
        {
            Validate(lhs, rhs);

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
            Validate(p);

            int n = p.PolynomialPower;

            var result = new double[p.PolynomialPower];

            for (int i = 0; i < n; i++)
            {
                result[i] = -1 * p[i];
            }

            return new Polynomial(result);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Validates the specified polynomials.
        /// </summary>
        /// <param name="polynomial1">The polynomial1.</param>
        /// <param name="polynomial2">The polynomial2.</param>
        /// <exception cref="ArgumentNullException">Polynomial can't be null</exception>
        private static void Validate(Polynomial polynomial1, Polynomial polynomial2)
        {
            if (polynomial1 == null || polynomial2 == null)
            {
                throw new ArgumentNullException("Polynomial can't be null");
            }
        }

        /// <summary>
        /// Validates the specified polynomial.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <exception cref="ArgumentNullException">polynomial</exception>
        private static void Validate(Polynomial polynomial)
        {
            if (polynomial == null)
            {
                throw new ArgumentNullException($"{nameof(polynomial)} can't be null");
            }
        }

        /// <summary>
        /// Validates the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <exception cref="ArgumentNullException">array</exception>
        /// <exception cref="ArgumentException">array</exception>
        private static void Validate(double[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} can't be null");
            }

            if (array.Length == 0)
            {
                throw new ArgumentException($"{nameof(array)} can't be empty");
            }
        } 
        #endregion
    }
}
