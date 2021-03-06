﻿using System;
using System.Runtime.InteropServices;

namespace AtgDev.Utils.Native
{
#if (NET5_0_OR_GREATER || NETCOREAPP3_0_OR_GREATER)
    static class DllLoader
    {
        public static IntPtr Load(string dllToLoad)
        {
            NativeLibrary.TryLoad(dllToLoad, out IntPtr resultPtr);
            return resultPtr;
        }

        public static IntPtr GetProcedureAddress(IntPtr dllHandle, string procedureName)
        {
            NativeLibrary.TryGetExport(dllHandle, procedureName, out IntPtr resultPtr);
            return resultPtr;
        }

        public static void Free(IntPtr dllHandle)
        {
            NativeLibrary.Free(dllHandle);
        }
    }
#else
    static class DllLoader
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        public static IntPtr Load(string dllToLoad)
        {
            return LoadLibrary(dllToLoad);
        }

        public static IntPtr GetProcedureAddress(IntPtr dllHandle, string procedureName)
        {
            return GetProcAddress(dllHandle, procedureName);
        }

        public static void Free(IntPtr dllHandle)
        {
            FreeLibrary(dllHandle);
        }
    }
#endif
}
