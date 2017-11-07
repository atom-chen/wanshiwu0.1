﻿namespace TMPro
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct KerningPairKey
    {
        public int ascii_Left;
        public int ascii_Right;
        public int key;
        public KerningPairKey(int ascii_left, int ascii_right)
        {
            this.ascii_Left = ascii_left;
            this.ascii_Right = ascii_right;
            this.key = (ascii_right << 0x10) + ascii_left;
        }
    }
}

