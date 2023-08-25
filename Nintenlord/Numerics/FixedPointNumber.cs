using System;

namespace Nintenlord.Numerics
{
    public readonly struct FixedPointNumber : IEquatable<FixedPointNumber>, IComparable<FixedPointNumber>//, IConvertible
    {
        #region Static
        private static readonly int[] masks;

        static FixedPointNumber()
        {
            masks = new int[sizeof(int) * 8];
            int mask = 0;
            for (int i = 0; i < masks.Length; i++)
            {
                masks[i] = mask;
                mask <<= 1;
                mask |= 1;
            }
            decimals = 16;
            decimalMask = masks[decimals];
        }

        private static readonly int decimals;
        private static readonly int decimalMask;
        #endregion

        private readonly int rawValue;

        private FixedPointNumber(int rawVal)
        {
            rawValue = rawVal;
        }

        public static explicit operator int(FixedPointNumber val)
        {
            return val.rawValue >> decimals;
        }

        public static implicit operator FixedPointNumber(int val)
        {
            return new FixedPointNumber(val << decimals);
        }

        public static explicit operator float(FixedPointNumber val)
        {
            float value = (int)val;
            value += (val.rawValue & decimalMask) / (float)(decimalMask + 1);
            return value;
        }

        public static implicit operator FixedPointNumber(float val)
        {
            int rawval = (int)val;
            rawval <<= decimals;
            double decimalval = val - Math.Floor(val);
            rawval |= (int)((decimalMask + 1) * decimalval);
            return new FixedPointNumber(rawval);
        }

        public static FixedPointNumber operator +(FixedPointNumber val, FixedPointNumber val2)
        {
            return new FixedPointNumber(val.rawValue + val2.rawValue);
        }

        public static FixedPointNumber operator -(FixedPointNumber val, FixedPointNumber val2)
        {
            return new FixedPointNumber(val.rawValue - val2.rawValue);
        }

        public static FixedPointNumber operator *(FixedPointNumber val, FixedPointNumber val2)
        {
            long temp = Math.BigMul(val.rawValue, val2.rawValue);
            return new FixedPointNumber((int)(temp >> decimals));
        }

        public static FixedPointNumber operator /(FixedPointNumber val, FixedPointNumber val2)
        {
            int remainder = Math.DivRem(val.rawValue, val2.rawValue, out int quatient);
            int result = remainder << decimals | quatient / (val2.rawValue >> decimals);
            return new FixedPointNumber(result);
        }

        public static FixedPointNumber operator <<(FixedPointNumber val, int val2)
        {
            return new FixedPointNumber(val.rawValue << val2);
        }

        public static FixedPointNumber operator >>(FixedPointNumber val, int val2)
        {
            return new FixedPointNumber(val.rawValue >> val2);
        }

        public static FixedPointNumber operator &(FixedPointNumber val, FixedPointNumber val2)
        {
            return new FixedPointNumber(val.rawValue & val2.rawValue);
        }

        public static FixedPointNumber operator |(FixedPointNumber val, FixedPointNumber val2)
        {
            return new FixedPointNumber(val.rawValue | val2.rawValue);
        }

        public static FixedPointNumber operator ^(FixedPointNumber val, FixedPointNumber val2)
        {
            return new FixedPointNumber(val.rawValue ^ val2.rawValue);
        }


        #region IEquatable<FixedPointInteger> Members

        public bool Equals(FixedPointNumber other)
        {
            return CompareTo(other) == 0;
        }

        #endregion

        #region IComparable<FixedPointInteger> Members

        public int CompareTo(FixedPointNumber other)
        {
            return rawValue - other.rawValue;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is FixedPointNumber)
            {
                return Equals((FixedPointNumber)obj);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return rawValue;
        }

        public override string ToString()
        {
            //return Convert.ToString(rawValue, 16);
            //return rawValue.ToString();
            return ((float)this).ToString();
        }

        #region IConvertible Members

        //public TypeCode GetTypeCode()
        //{
        //    return TypeCode.Object;
        //}

        //public bool ToBoolean(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public byte ToByte(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public char ToChar(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public DateTime ToDateTime(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public decimal ToDecimal(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public double ToDouble(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public short ToInt16(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public int ToInt32(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public long ToInt64(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public sbyte ToSByte(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public float ToSingle(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public string ToString(IFormatProvider provider)
        //{
        //    ret 
        //}

        //public object ToType(Type conversionType, IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public ushort ToUInt16(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public uint ToUInt32(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public ulong ToUInt64(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
