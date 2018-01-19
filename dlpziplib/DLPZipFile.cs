using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ionic.Zip;

namespace dlpziplib
{
    public class DLPZipFile
    {
        public static void addFiles(string archiveFile, List<string> inputFiles)
        {
            DLPZipUtil.Trace("addFiles()");
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFiles(inputFiles);
                zip.Save(archiveFile);
            }
            DLPZipUtil.Trace("~addFiles()");
        }

    }
}
