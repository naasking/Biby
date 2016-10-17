using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Biby;
using System.Diagnostics;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestEndian();
            TestHighestBit();
            TestBinaryString();
            TestBitCount();
            TestLog2();
        }

        static void TestEndian()
        {
            var buf = new byte[128];
            long.MaxValue.CopyTo(buf, 36);
            Debug.Assert(long.MaxValue == buf.GetInt64(36));
            var x = 0x07C4ACDDU;
            x.CopyTo(buf, 98);
            Debug.Assert(x == buf.GetUInt32(98));

            var s16 = (short)23565;
            s16.CopyTo(buf, 54);
            Debug.Assert(s16 == buf.GetInt16(54));

            var g = Guid.NewGuid();
            g.CopyTo(buf, 23);
            Debug.Assert(g == buf.GetGuid(23));
        }

        static void TestHighestBit()
        {
            Debug.Assert((uint)1 << 31 == Bits.HighestBit(uint.MaxValue));
            Debug.Assert((uint)1 << 13 == Bits.HighestBit((uint)1 << 13));
            Debug.Assert((uint)1 << 13 == Bits.HighestBit(1 + ((uint)1 << 13)));
            Debug.Assert((uint)1 <<  7 == Bits.HighestBit((uint)255));
            Debug.Assert((uint)0       == Bits.HighestBit(0));
        }

        static void TestLog2()
        {
            Debug.Assert(31 == Bits.Log2(uint.MaxValue));
            Debug.Assert(13 == Bits.Log2((uint)1 << 13));
            Debug.Assert(13 == Bits.Log2(1 + ((uint)1 << 13)));
            Debug.Assert( 7 == Bits.Log2((uint)255));
            Debug.Assert( 0 == Bits.Log2(0));
        }

        static void TestBitCount()
        {
            Debug.Assert(Bits.BitCount(uint.MaxValue) == 32);
            Debug.Assert(Bits.BitCount(0) == 0);
            Debug.Assert(Bits.BitCount(1) == 1);
            Debug.Assert(Bits.BitCount(ushort.MaxValue) == 16);
            Debug.Assert(Bits.BitCount(ushort.MaxValue - 1) == 15);
        }

        static void TestBinaryString()
        {
            Debug.Assert(0L.ToBinaryString() == new string('0', 64));
            Debug.Assert(1L.ToBinaryString() == new string('0', 63) + '1');
            Debug.Assert(ulong.MaxValue.ToBinaryString() == new string('1', 64));
        }
    }
}
