using System;
using FileHelpers;

namespace ViviPlusTT.Format
{
    public class FieldConverter
    {
        internal class TwoDecimalConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                var res = Convert.ToDecimal(from);
                return res / 100;
            }

            public override string FieldToString(object from)
            {
                var d = Convert.ToDouble(from);
                return Math.Round(d * 100, MidpointRounding.AwayFromZero).ToString();
            }
        }

        internal class FixedLengthConverter : ConverterBase
        {
            private Type _returnType;
            private int _maxLength;
            public FixedLengthConverter(Type returnType, int maxLength)
            {
                _returnType = returnType;
                _maxLength = maxLength;
            }
            public override object StringToField(string from)
            {
                return from;
            }

            public override string FieldToString(object from)
            {
                var chars = from.ToString();
                if (chars.Length > _maxLength)
                {
                    return chars.Substring(0, _maxLength);
                }
                return chars;
            }
        }

    }
}
