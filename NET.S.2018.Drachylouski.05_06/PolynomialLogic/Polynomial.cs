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

        public Polynomial(double [] coefficients)
        {
            this.coefficients= new double[coefficients.Length];
            Array.Copy(coefficients,this.coefficients,coefficients.Length);
        }

        public int PolynomialPower => coefficients.Length;

        public double[] Coefficients
        {
            get
            {
                int n = PolynomialPower;

                var temp = new double[n];

                Array.Copy(coefficients,temp,n);

                return temp;
            }
        }

        public double this[int n] => coefficients[n];

        public override string ToString()
        {
            var sb = new StringBuilder();

            int n = PolynomialPower;

            for (int i = 0; i <=n; i++)
            {
                sb.Append(this[i] + "a^" + (n - i));
            }

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj==null || GetType() != obj.GetType())
            {
                return false;
            }

            Polynomial p = (Polynomial) obj;

            int n = PolynomialPower;

            if (n != p.PolynomialPower)
            {
                return false;
            }


            for (int i = 0; i < n;i++)
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

        public static bool operator ==(Polynomial a, Polynomial b)
        {
            return a != null && a.Equals(b);
        }

        public static bool operator !=(Polynomial a, Polynomial b)
        {
            return a != null && !a.Equals(b);
        }

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            int itemsCount = Math.Max(a.PolynomialPower,b.PolynomialPower);
            var result = new double[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                double first = 0;
                double second = 0;

                if (i < a.PolynomialPower)
                {
                    first = a[i];
                }
                if (i < b.PolynomialPower)
                {
                    second =b[i];
                }

                result[i] = first+second;
            }

            return new Polynomial(result);
        }

        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            int itemsCount = Math.Max(a.PolynomialPower, b.PolynomialPower);
            var result = new double[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                double first = 0;
                double second = 0;

                if (i < a.PolynomialPower)
                {
                    first = a[i];
                }
                if (i < b.PolynomialPower)
                {
                    second = b[i];
                }

                result[i] = first - second;
            }

            return new Polynomial(result);
        }

        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            int itemsCount = a.PolynomialPower + b.PolynomialPower - 1;
            var result = new double[itemsCount];

            for (int i = 0; i < a.PolynomialPower; i++)
            {
                for (int j = 0; j < b.PolynomialPower; j++)
                {
                    result[i + j] += a[i] * b[j];
                }
            }

            return new Polynomial(result);
        }


    }
}
