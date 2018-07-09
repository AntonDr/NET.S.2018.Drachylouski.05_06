using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialLogic
{
    public sealed class Polynomial
    {
        private readonly double[] coefficients;

        public Polynomial(double[] coefficients)
        {
            this.coefficients = new double[coefficients.Length];
            Array.Copy(coefficients, this.coefficients, coefficients.Length);
        }

        public int PolynomialPower => coefficients.Length;

        public double this[int n]
        {
            get
            {
                if (n > PolynomialPower || n < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return coefficients[n];
            }
        }

        public double[] ToArray()
        {

            int n = PolynomialPower;

            var temp = new double[n];

            Array.Copy(coefficients, temp, n);

            return temp;

        }

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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this,obj))
            {
                
            }

            

            if (obj == null || GetType() != obj.GetType())
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
                if (Math.Abs(this[i] - p[i]) > Double.Epsilon)
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

                result[i] = first - second;
            }

            return new Polynomial(result);
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


    }
}
