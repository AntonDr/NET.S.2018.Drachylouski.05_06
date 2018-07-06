using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterLogic
{    
    public static class Converter
    {
        /// <summary>
        /// To the decimal converter.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="ground">The ground.</param>
        /// <returns>The number.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static long ToDecimalConverter(this string source, int ground)
        {
            var notation = new Notation(ground);

            Validate(source,notation);

            long number = 0, product = 1;

            int @base = notation.Base;

            string alphabet = notation.Alphabet;

            string upperString = source.ToUpper();

            for (int i = source.Length - 1; i >= 0; i--)
            {
                checked
                {
                    if (ConvertToValue(upperString[i], alphabet) == -1)
                    {
                        throw new ArgumentException($"Invalid symbol {source[i]} in string!");
                    }
                    else
                    {
                        number += product * ConvertToValue(upperString[i], alphabet);

                        product *= @base;
                    }
                }
            }

            return number;
        }

        /// <summary>
        /// Validates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="notation">The notation.</param>
        /// <exception cref="ArgumentException">source</exception>
        /// <exception cref="ArgumentNullException">notation</exception>
        private static void Validate(string source, Notation notation)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentException($"The string {nameof(source)} can not be null or empty!");
            }

            if (notation == null)
            {
                throw new ArgumentNullException($"The object {nameof(notation)} can not be null!");
            }
        }

        /// <summary>
        /// Converts to value.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="alphbet">The alphbet.</param>
        /// <returns>Value of current symbol</returns>
        private static int ConvertToValue(char symbol, string alphbet) => alphbet.IndexOf(symbol);
    }

    /// <summary>
    /// Сlass that connects the ground with its alphabet
    /// </summary>
    public sealed class Notation
    {
        #region Constants

        private const int UpperValue = 16;
        private const int LowerValue = 2;

        #endregion

        #region Fields

        private int @base;

        #endregion

        #region Constructors

        public Notation()
            : this(10)
        {
        }

        public Notation(int @base)
        {
            this.Base = @base;
            this.Alphabet = this.StringCreation();
        }

        #endregion

        #region Properties

        public int Base
        {
            get => this.@base;

            set
            {
                if (value < LowerValue || value > UpperValue)
                {
                    throw new ArgumentOutOfRangeException(
                        $"Base of Notation must be more or equal then {LowerValue} and less or equal then {UpperValue}!");
                }

                this.@base = value;
                this.Alphabet = this.StringCreation();
            }
        }

        public string Alphabet { get; private set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Сreates an alphabet for the base
        /// </summary>
        /// <returns></returns>
        private string StringCreation()
        {
            int @base = this.Base;

            StringBuilder sb = new StringBuilder(@base);

            int symbol = 'A';

            for (int i = 0; i < @base; i++)
            {
                if (i <= 9)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append((char) symbol++);
                }
            }

            return sb.ToString();
        }

        #endregion

    }
}
