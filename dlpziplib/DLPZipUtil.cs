using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; // for trace output

namespace dlpziplib
{
    public class DLPZipUtil
    {
        public static void Trace(params String[] msg)
        {
            System.Diagnostics.Trace.WriteLine("DLPZip.exe: " + String.Join(" ", msg));
        }
        public static void Debug(String msg)
        {
            System.Diagnostics.Debug.WriteLine("DLPZip.exe: " + String.Join(" ", msg));
        }
    }
}
