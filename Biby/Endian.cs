using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Biby
{
    /// <summary>
    /// Endian/byte-order operations.
    /// </summary>
    public static class Endian
    {
        /// <summary>
        /// Copy the value to the buffer in big endian format.
        /// </summary>
        /// <param name="value">The value to copy.</param>
        /// <param name="buffer">The buffer to copy into.</param>
        /// <param name="start">The optional index to start the copy.</param>
        public static void CopyTo(this long value, byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 7 < buffer.Length);
            unchecked((ulong)value).CopyTo(buffer, start);
        }

        /// <summary>
        /// Copy the value to the buffer in big endian format.
        /// </summary>
        /// <param name="value">The value to copy.</param>
        /// <param name="buffer">The buffer to copy into.</param>
        /// <param name="start">The optional index to start the copy.</param>
        public static void CopyTo(this int value, byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 3 < buffer.Length);
            unchecked((uint)value).CopyTo(buffer, start);
        }

        /// <summary>
        /// Copy the value to the buffer in big endian format.
        /// </summary>
        /// <param name="value">The value to copy.</param>
        /// <param name="buffer">The buffer to copy into.</param>
        /// <param name="start">The optional index to start the copy.</param>
        public static void CopyTo(this short value, byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 1 < buffer.Length);
            unchecked((ushort)value).CopyTo(buffer, start);
        }

        /// <summary>
        /// Copy the value to the buffer in big endian format.
        /// </summary>
        /// <param name="value">The value to copy.</param>
        /// <param name="buffer">The buffer to copy into.</param>
        /// <param name="start">The optional index to start the copy.</param>
        public static void CopyTo(this double value, byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 7 < buffer.Length);
            BitConverter.DoubleToInt64Bits(value).CopyTo(buffer, start);
        }
        
        /// <summary>
        /// Copy the value to the buffer in big endian format.
        /// </summary>
        /// <param name="value">The value to copy.</param>
        /// <param name="buffer">The buffer to copy into.</param>
        /// <param name="start">The optional index to start the copy.</param>
        public static void CopyTo(this float value, byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 3 < buffer.Length);
            new Union32(value).Unsigned.CopyTo(buffer, start);
        }

        /// <summary>
        /// Copy the value to the buffer in big endian format.
        /// </summary>
        /// <param name="value">The value to copy.</param>
        /// <param name="buffer">The buffer to copy into.</param>
        /// <param name="start">The optional index to start the copy.</param>
        [CLSCompliant(false)]
        public static void CopyTo(this ulong value, byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 7 < buffer.Length);
            if (BitConverter.IsLittleEndian)
            {
                buffer[0 + start] = (byte)((value >> 56) & 0xFFFF);
                buffer[1 + start] = (byte)((value >> 48) & 0xFFFF);
                buffer[2 + start] = (byte)((value >> 40) & 0xFFFF);
                buffer[3 + start] = (byte)((value >> 32) & 0xFFFF);
                buffer[4 + start] = (byte)((value >> 24) & 0xFFFF);
                buffer[5 + start] = (byte)((value >> 16) & 0xFFFF);
                buffer[6 + start] = (byte)((value >>  8) & 0xFFFF);
                buffer[7 + start] = (byte)(value         & 0xFFFF);
            }
            else
            {
                buffer[0 + start] = (byte)(value         & 0xFFFF);
                buffer[1 + start] = (byte)((value >>  8) & 0xFFFF);
                buffer[2 + start] = (byte)((value >> 16) & 0xFFFF);
                buffer[3 + start] = (byte)((value >> 24) & 0xFFFF);
                buffer[4 + start] = (byte)((value >> 32) & 0xFFFF);
                buffer[5 + start] = (byte)((value >> 40) & 0xFFFF);
                buffer[6 + start] = (byte)((value >> 48) & 0xFFFF);
                buffer[7 + start] = (byte)((value >> 56) & 0xFFFF);
            }
        }

        /// <summary>
        /// Copy the value to the buffer in big endian format.
        /// </summary>
        /// <param name="value">The value to copy.</param>
        /// <param name="buffer">The buffer to copy into.</param>
        /// <param name="start">The optional index to start the copy.</param>
        [CLSCompliant(false)]
        public static void CopyTo(this uint value, byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 3 < buffer.Length);
            if (BitConverter.IsLittleEndian)
            {
                buffer[0 + start] = (byte)((value >> 24) & 0xFFFF);
                buffer[1 + start] = (byte)((value >> 16) & 0xFFFF);
                buffer[2 + start] = (byte)((value >>  8) & 0xFFFF);
                buffer[3 + start] = (byte)(value         & 0xFFFF);
            }
            else
            {
                buffer[0 + start] = (byte)(value         & 0xFFFF);
                buffer[1 + start] = (byte)((value >>  8) & 0xFFFF);
                buffer[2 + start] = (byte)((value >> 16) & 0xFFFF);
                buffer[3 + start] = (byte)((value >> 24) & 0xFFFF);
            }
        }

        /// <summary>
        /// Copy the value to the buffer in big endian format.
        /// </summary>
        /// <param name="value">The value to copy.</param>
        /// <param name="buffer">The buffer to copy into.</param>
        /// <param name="start">The optional index to start the copy.</param>
        [CLSCompliant(false)]
        public static void CopyTo(this ushort value, byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 1 < buffer.Length);
            if (BitConverter.IsLittleEndian)
            {
                buffer[0 + start] = (byte)((value >> 8) & 0xFFFF);
                buffer[1 + start] = (byte)(value        & 0xFFFF);
            }
            else
            {
                buffer[0 + start] = (byte)(value        & 0xFFFF);
                buffer[1 + start] = (byte)((value >> 8) & 0xFFFF);
            }
        }

        /// <summary>
        /// Read a value out of a buffer in big endian format.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="start">The optional index to start reading</param>
        /// <returns>A value of the given type.</returns>
        [CLSCompliant(false)]
        [System.Diagnostics.Contracts.Pure]
        public static ulong GetUInt64(this byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 7 < buffer.Length);
            return unchecked((ulong)buffer.GetInt64(start));
        }

        /// <summary>
        /// Read a value out of a buffer in big endian format.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="start">The optional index to start reading</param>
        /// <returns>A value of the given type.</returns>
        [System.Diagnostics.Contracts.Pure]
        public static long GetInt64(this byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 7 < buffer.Length);
            unchecked
            {
                return BitConverter.IsLittleEndian
                     ? buffer[0 + start] << 56 | buffer[1 + start] << 48 | buffer[2 + start] << 40 | buffer[3 + start] << 32 |
                       buffer[4 + start] << 24 | buffer[5 + start] << 16 | buffer[6 + start] <<  8 | buffer[7 + start]
                     : buffer[0 + start]       | buffer[1 + start] <<  8 | buffer[2 + start] << 16 | buffer[3 + start] << 24 |
                       buffer[4 + start] << 32 | buffer[5 + start] << 40 | buffer[6 + start] << 48 | buffer[7 + start] << 56;
            }
        }

        /// <summary>
        /// Read a value out of a buffer in big endian format.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="start">The optional index to start reading</param>
        /// <returns>A value of the given type.</returns>
        [CLSCompliant(false)]
        [System.Diagnostics.Contracts.Pure]
        public static uint GetUInt32(this byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 3 < buffer.Length);
            return unchecked((uint)buffer.GetInt32(start));
        }

        /// <summary>
        /// Read a value out of a buffer in big endian format.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="start">The optional index to start reading</param>
        /// <returns>A value of the given type.</returns>
        [System.Diagnostics.Contracts.Pure]
        public static int GetInt32(this byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 3 < buffer.Length);
            unchecked
            {
                return BitConverter.IsLittleEndian
                     ? buffer[4 + start] << 24 | buffer[5 + start] << 16 | buffer[6 + start] <<  8 | buffer[7 + start]
                     : buffer[0 + start]       | buffer[1 + start] <<  8 | buffer[2 + start] << 16 | buffer[3 + start] << 24;
            }
        }

        /// <summary>
        /// Read a value out of a buffer in big endian format.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="start">The optional index to start reading</param>
        /// <returns>A value of the given type.</returns>
        [CLSCompliant(false)]
        [System.Diagnostics.Contracts.Pure]
        public static ushort GetUInt16(this byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 1 < buffer.Length);
            unchecked
            {
                return (ushort)(BitConverter.IsLittleEndian
                     ? buffer[0 + start] << 8 | buffer[1 + start]
                     : buffer[0 + start]      | buffer[1 + start] << 8);
            }
        }

        /// <summary>
        /// Read a value out of a buffer in big endian format.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="start">The optional index to start reading</param>
        /// <returns>A value of the given type.</returns>
        [System.Diagnostics.Contracts.Pure]
        public static short GetInt16(this byte[] buffer, int start = 0)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(0 <= start && start + 1 < buffer.Length);
            return unchecked((short)buffer.GetUInt16(start));
        }

        /// <summary>
        /// Swap upper and lower bytes.
        /// </summary>
        /// <param name="value">The value being swapped.</param>
        /// <returns>The swapped value.</returns>
        [System.Diagnostics.Contracts.Pure]
        public static short Swap(short value)
        {
            return unchecked((short)Swap((ushort)value));
        }

        /// <summary>
        /// Swap upper and lower bytes.
        /// </summary>
        /// <param name="value">The value being swapped.</param>
        /// <returns>The swapped value.</returns>
        [CLSCompliant(false)]
        [System.Diagnostics.Contracts.Pure]
        public static ushort Swap(ushort value)
        {
            return (ushort)((value << 8 | value >> 8) & 0xFFFF);
        }
        /// <summary>
        /// Swap upper and lower bytes.
        /// </summary>
        /// <param name="value">The value being swapped.</param>
        /// <returns>The swapped value.</returns>
        [CLSCompliant(false)]
        [System.Diagnostics.Contracts.Pure]
        public static uint Swap(uint value)
        {
            var top = value & 0x000000FF;
            return top << 24
                   | value <<  8 & 0x00FF0000
                   | value >>  8 & 0x0000FF00
                   | value >> 24 & 0x000000FF;
        }
        /// <summary>
        /// Swap upper and lower bytes.
        /// </summary>
        /// <param name="value">The value being swapped.</param>
        /// <returns>The swapped value.</returns>
        [System.Diagnostics.Contracts.Pure]
        public static int Swap(int value)
        {
            return unchecked((int)Swap((uint)value));
        }

        /// <summary>
        /// Swap upper and lower bytes.
        /// </summary>
        /// <param name="value">The value being swapped.</param>
        /// <returns>The swapped value.</returns>
        [CLSCompliant(false)]
        [System.Diagnostics.Contracts.Pure]
        public static ulong Swap(ulong value)
        {
            var top = value & 0x00000000000000FF;
            return top << 56
                   | value << 40 & 0x00FF000000000000
                   | value << 24 & 0x0000FF0000000000
                   | value <<  8 & 0x000000FF00000000
                   | value >>  8 & 0x00000000FF000000
                   | value >> 24 & 0x0000000000FF0000
                   | value >> 40 & 0x000000000000FF00
                   | value >> 56 & 0x00000000000000FF;
        }

        /// <summary>
        /// Swap upper and lower bytes.
        /// </summary>
        /// <param name="value">The value being swapped.</param>
        /// <returns>The swapped value.</returns>
        [System.Diagnostics.Contracts.Pure]
        public static long Swap(long value)
        {
            return unchecked((long)Swap((ulong)value));
        }
    }
}
