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
                foreach(string file in inputFiles)
                {
                    DLPZipUtil.Trace("Adding " + file);
                    if (file.EndsWith(".doc", StringComparison.CurrentCultureIgnoreCase) || file.EndsWith(".docx", StringComparison.CurrentCultureIgnoreCase))
                        DLPZipUtil.getWordInfo(file);
                    zip.AddFile(file);
                }
                zip.Save(archiveFile);
            }
            DLPZipUtil.Trace("~addFiles()");
        }

    }
}
