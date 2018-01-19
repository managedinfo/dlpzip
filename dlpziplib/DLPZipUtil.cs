using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; // for trace output
using System.IO; // for file expansion

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
        public static List<String> ExpandFiles(String path)
        {
            Trace("ExpandFiles()");
            String[] files;
            String root = Path.GetDirectoryName(path);
            Trace("root is:", root);
            Trace("FileName is:", Path.GetFileName(path));
            if (root.Length == 0)
            {
                files = Directory.GetFiles(Directory.GetCurrentDirectory(), Path.GetFileName(path));
            }
            else
                files = Directory.GetFiles(root, Path.GetFileName(path));

            foreach (String f in files)
            {
                Trace("Expanded file name: ", f);
            }

            Trace("~ExpandFiles()");
            return files.ToList();
        }
    }
}
