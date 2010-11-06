using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;

namespace DLL
{
    class Program 
    {

        delegate bool BeepDelegate(uint dwFreq, uint dwDuration);

        static void Main(string[] args)
        {
            Win32API kernel32 = new Win32API("kernel32.dll");
            BeepDelegate Beep = kernel32.Function<BeepDelegate>("Beep");
            
            Beep(440,500);
            kernel32.Dispose();

        }
    }

}
