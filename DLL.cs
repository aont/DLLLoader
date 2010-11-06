using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;


namespace DLL
{
    class Win32API : IDisposable
    {
        IntPtr handle;

        /// <summary>
        /// DLL名を指定してWin32APIをロードします
        /// </summary>
        /// <param name="LibraryName"></param>
        public Win32API(string LibraryName)
        {
            this.handle = LoadLibrary(LibraryName);
        }

        /// <summary>
        /// ライブラリから関数を読み出します
        /// </summary>
        /// <typeparam name="DelegateType">読み出す関数のプロトタイプが定義されたデリゲート</typeparam>
        /// <param name="FuncName">ライブラリ名</param>
        /// <returns>関数</returns>
        public DelegateType Function<DelegateType>(string FuncName)
        {
            IntPtr funcPtr = GetProcAddress(this.handle, FuncName);
            return (DelegateType)(object)Marshal.GetDelegateForFunctionPointer(funcPtr, typeof(DelegateType));
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("kernel32", SetLastError = true)]
        static extern bool FreeLibrary(IntPtr hModule);
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        /// <summary>
        /// DLLをメモリから開放する
        /// </summary>
        public void Dispose()
        {
            FreeLibrary(handle);
        }

        ~Win32API()
        {
            FreeLibrary(handle);
        }
    }
}
