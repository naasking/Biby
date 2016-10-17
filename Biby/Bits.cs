using System;
using System.Text;
using System.Diagnostics.Contracts;

namespace Biby
{
    /// <summary>
    /// Efficient endian encoding/decoding.
    /// </summary>
    [System.Diagnostics.Contracts.Pure]
    public static class Bits
    {
        /// <summary>
        /// Fold the high order bits into the low order bits.
        /// </summary>
        /// <param name="value">The value to operate on.</param>
        /// <returns>The folded value.</returns>
        [CLSCompliant(false)]
        public static uint Fold(uint value)
        {
            value |= (value >> 1);
            value |= (value >> 2);
            value |= (value >> 4);
            value |= (value >> 8);
            value |= (value >> 16);
            return value;
        }

        /// <summary>
        /// Fold the high order bits into the low order bits.
        /// </summary>
        /// <param name="value">The value to operate on.</param>
        /// <returns>The folded value.</returns>
        public static int Fold(int value)
        {
            return unchecked((int)Fold((uint)value));
        }

        /// <summary>
        /// Extract the highest bit.
        /// </summary>
        /// <param name="value">The bit pattern to use.</param>
        /// <returns>The bit pattern with only the highest bit set.</returns>
        /// <remarks>
        /// Implementation taken from:
        /// http://aggregate.org/MAGIC/#Most%20Significant%201%20Bit
        /// </remarks>
        [CLSCompliant(false)]
        public static uint HighestBit(this uint value)
        {
            value = Fold(value);
            return value ^ (value >> 1);
        }

        /// <summary>
        /// Extract the highest bit.
        /// </summary>
        /// <param name="value">The bit pattern to use.</param>
        /// <returns>The bit pattern with only the highest bit set.</returns>
        public static int HighestBit(this int value)
        {
            return unchecked((int)HighestBit((uint)value));
        }

        static int[] deBruijinPos =
        {
            0,  9,  1, 10, 13, 21,  2, 29, 11, 14, 16, 18, 22, 25, 3, 30,
            8, 12, 20, 28, 15, 17, 24,  7, 19, 27, 23,  6, 26,  5, 4, 31
        };

        /// <summary>
        /// Computes the base2 logarithm.
        /// </summary>
        /// <param name="value">The value to compute.</param>
        /// <returns>The base2 logarithm.</returns>
        /// <remarks>
        /// Implementation taken from:
        /// http://graphics.stanford.edu/~seander/bithacks.html#IntegerLogDeBruijn
        /// </remarks>
        [CLSCompliant(false)]
        public static int Log2(this uint value)
        {
            var i = (Fold(value) * 0x07C4ACDDU) >> 27;
            Contract.Assume(i < deBruijinPos.Length);
            return deBruijinPos[i];
        }

        /// <summary>
        /// Calculate the number of set bits in <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The integer whose bits we wish to count.</param>
        /// <returns>The number of set bits in <paramref name="value"/>.</returns>
        /// <remarks>
        /// Implementation taken from:
        /// http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel
        /// </remarks>
        [CLSCompliant(false)]
        public static int BitCount(this uint value)
        {
            value = value - ((value >> 1) & 0x55555555);                    // reuse input as temporary
            value = (value & 0x33333333) + ((value >> 2) & 0x33333333);     // temp
            value = ((value + (value >> 4) & 0xF0F0F0F) * 0x1010101) >> 24; // count
            return unchecked((int)value);
        }

        /// <summary>
        /// Calculate the number of set bits in <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The integer whose bits we wish to count.</param>
        /// <returns>The number of set bits in <paramref name="value"/>.</returns>
        public static int BitCount(this int value)
        {
            return unchecked(BitCount((uint)value));
        }

        #region Bit formatting
        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <param name="buffer">The output buffer.</param>
        /// <returns>A binary string.</returns>
        [CLSCompliant(false)]
        public static StringBuilder ToBinaryString(this ulong value, StringBuilder buffer)
        {
            Contract.Requires(buffer != null);
            for (int i = 63; i >= 0; --i) buffer.Append((value >> i) & 1);
            return buffer;
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <returns>A binary string.</returns>
        [CLSCompliant(false)]
        public static string ToBinaryString(this ulong value)
        {
            return value.ToBinaryString(new StringBuilder()).ToString();
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <returns>A binary string.</returns>
        public static string ToBinaryString(this long value)
        {
            return unchecked(ToBinaryString((ulong)value));
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <param name="buffer">The output buffer.</param>
        /// <returns>A binary string.</returns>
        public static StringBuilder ToBinaryString(this long value, StringBuilder buffer)
        {
            Contract.Requires(buffer != null);
            return unchecked(ToBinaryString((ulong)value, buffer));
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <param name="buffer">The output buffer.</param>
        /// <returns>A binary string.</returns>
        [CLSCompliant(false)]
        public static StringBuilder ToBinaryString(this uint value, StringBuilder buffer)
        {
            Contract.Requires(buffer != null);
            for (int i = 31; i >= 0; --i) buffer.Append((value >> i) & 1);
            return buffer;
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <returns>A binary string.</returns>
        [CLSCompliant(false)]
        public static string ToBinaryString(this uint value)
        {
            return value.ToBinaryString(new StringBuilder()).ToString();
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <returns>A binary string.</returns>
        public static string ToBinaryString(this int value)
        {
            return unchecked(ToBinaryString((uint)value));
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <param name="buffer">The output buffer.</param>
        /// <returns>A binary string.</returns>
        public static StringBuilder ToBinaryString(this int value, StringBuilder buffer)
        {
            Contract.Requires(buffer != null);
            return unchecked(ToBinaryString((uint)value, buffer));
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <param name="buffer">The output buffer.</param>
        /// <returns>A binary string.</returns>
        [CLSCompliant(false)]
        public static StringBuilder ToBinaryString(this ushort value, StringBuilder buffer)
        {
            Contract.Requires(buffer != null);
            for (int i = 16; i >= 0; --i) buffer.Append((value >> i) & 1);
            return buffer;
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <returns>A binary string.</returns>
        [CLSCompliant(false)]
        public static string ToBinaryString(this ushort value)
        {
            return value.ToBinaryString(new StringBuilder()).ToString();
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <returns>A binary string.</returns>
        public static string ToBinaryString(this short value)
        {
            return unchecked(ToBinaryString((ushort)value));
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <param name="buffer">The output buffer.</param>
        /// <returns>A binary string.</returns>
        public static StringBuilder ToBinaryString(this short value, StringBuilder buffer)
        {
            Contract.Requires(buffer != null);
            return unchecked(ToBinaryString((ushort)value, buffer));
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <param name="buffer">The output buffer.</param>
        /// <returns>A binary string.</returns>
        public static StringBuilder ToBinaryString(this byte value, StringBuilder buffer)
        {
            Contract.Requires(buffer != null);
            for (int i = 16; i >= 0; --i) buffer.Append((value >> i) & 1);
            return buffer;
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <returns>A binary string.</returns>
        public static string ToBinaryString(this byte value)
        {
            return value.ToBinaryString(new StringBuilder()).ToString();
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <returns>A binary string.</returns>
        [CLSCompliant(false)]
        public static string ToBinaryString(this sbyte value)
        {
            return unchecked(ToBinaryString((byte)value));
        }

        /// <summary>
        /// A binary string representationg.
        /// </summary>
        /// <param name="value">The unsigned long.</param>
        /// <param name="buffer">The output buffer.</param>
        /// <returns>A binary string.</returns>
        [CLSCompliant(false)]
        public static StringBuilder ToBinaryString(this sbyte value, StringBuilder buffer)
        {
            return unchecked(ToBinaryString((byte)value, buffer));
        }
        #endregion

        #region Universal hash codes
        /// <summary>
        /// Computes a universal hash code.
        /// </summary>
        /// <param name="value">The value to hash.</param>
        /// <param name="scalingFactor">The multiplicative constant.</param>
        /// <param name="additiveConstant">The additive constant.</param>
        /// <param name="bitCount">The number of bits in the resulting hash.</param>
        /// <returns>An integral universal hash code.</returns>
        [CLSCompliant(false)]
        public static int GetHashCode(this uint value, uint scalingFactor, uint additiveConstant, int bitCount)
        {
            if (bitCount > 32) throw new ArgumentOutOfRangeException("bitCount", "Must be 32 bits or less.");
            return unchecked((int)((scalingFactor * value + additiveConstant) >> (32 - bitCount)));
        }

        /// <summary>
        /// Computes a universal hash code.
        /// </summary>
        /// <param name="value">The value to hash.</param>
        /// <param name="scalingFactor">The multiplicative constant.</param>
        /// <param name="additiveConstant">The additive constant.</param>
        /// <param name="bitCount">The number of bits in the resulting hash.</param>
        /// <returns>An integral universal hash code.</returns>
        public static int GetHashCode(this int value, int scalingFactor, int additiveConstant, int bitCount)
        {
            return unchecked(GetHashCode((uint)value, (uint)scalingFactor, (uint)additiveConstant, bitCount));
        }

        /// <summary>
        /// Computes a universal hash code.
        /// </summary>
        /// <param name="value">The value to hash.</param>
        /// <param name="scalingFactor">The multiplicative constant.</param>
        /// <param name="additiveConstant">The additive constant.</param>
        /// <param name="bitCount">The number of bits in the resulting hash.</param>
        /// <returns>An integral universal hash code.</returns>
        [CLSCompliant(false)]
        public static int GetHashCode(this ulong value, ulong scalingFactor, ulong additiveConstant, int bitCount)
        {
            if (bitCount > 32) throw new ArgumentOutOfRangeException("bitCount", "Must be 32 bits or less.");
            var x = (scalingFactor * value + additiveConstant) >> (32 - bitCount);
            unchecked { return (int)x | (int)(x >> 32); }
        }

        /// <summary>
        /// Computes a universal hash code.
        /// </summary>
        /// <param name="value">The value to hash.</param>
        /// <param name="scalingFactor">The multiplicative constant.</param>
        /// <param name="additiveConstant">The additive constant.</param>
        /// <param name="bitCount">The number of bits in the resulting hash.</param>
        /// <returns>An integral universal hash code.</returns>
        public static int GetHashCode(this long value, long scalingFactor, long additiveConstant, int bitCount)
        {
            return unchecked(GetHashCode((ulong)value, (ulong)scalingFactor, (ulong)additiveConstant, bitCount));
        }
        #endregion
    }
}
