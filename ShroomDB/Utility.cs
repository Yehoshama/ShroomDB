using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShroomDB
{
    public class Utility
    {
        public static class BitFlags
        {
            // Get the value of a bit (0 or 1)
            public static bool GetBit(byte value, int bit)
            {
                return (value & (1 << bit)) != 0;
            }

            // Set a bit to 1 (mutates original)
            public static void SetBit(ref byte value, int bit)
            {
                value = (byte)(value | (1 << bit));
            }

            // Clear a bit to 0 (mutates original)
            public static void ClearBit(ref byte value, int bit)
            {
                value = (byte)(value & ~(1 << bit));
            }

            // Write a bit as true/false (mutates original)
            public static void WriteBit(ref byte value, int bit, bool state)
            {
                if (state)
                    SetBit(ref value, bit);
                else
                    ClearBit(ref value, bit);
            }

            // Toggle a bit (mutates original)
            public static void ToggleBit(ref byte value, int bit)
            {
                value = (byte)(value ^ (1 << bit));
            }
        }

    }
}
