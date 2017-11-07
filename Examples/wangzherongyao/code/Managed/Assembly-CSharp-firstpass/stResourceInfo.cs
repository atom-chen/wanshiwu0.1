﻿using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct stResourceInfo
{
    public string m_fullPathInResourcesWithoutExtension;
    public string m_extension;
    public int m_flags;
}

