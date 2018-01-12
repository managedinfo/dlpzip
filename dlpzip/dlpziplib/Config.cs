using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32; // for registry
using System.Diagnostics; // for trace output

namespace dlpziplib
{
    public class Config
    {
        public static void ReadConfig()
        {
            Trace.WriteLine("DLPZip.exe: ReadConfig()");
            DateTime ncts; // next check time stamp

            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "SOFTWARE\\DLPZip";
            const string keyName = userRoot + "\\" + subkey;

            try
            {
                var regval = (long)Registry.GetValue(keyName, "NCTS", 0);
                ncts = DateTime.FromBinary(regval);
            }
            catch (NullReferenceException)
            { 
                ncts = DateTime.MinValue;
            }

            if (DateTime.Now >= ncts)
            {
                Trace.WriteLine("DLPZip.exe: Re-reading config");

                //TODO write time + delay to registry
                Registry.SetValue(keyName, "NCTS", DateTime.Now.ToBinary(), RegistryValueKind.QWord);
            }

            //string json = @"{""key1"":""value1"",""key2"":""value2""}";
            //JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            Trace.WriteLine("DLPZip.exe: ~ReadConfig()");
        }
        //foreach (String path in inputfiles)
        //{

        //using (var zip = new ZipFile())
        //{
        //    foreach (String path in inputfiles)
        //    {
        //        filename = Path.GetFileName(path);
        //        filename = filename.Replace(clientCode, "");
        //        ZipEntry ze = zip.AddFile(path);
        //        ze.FileName = filename;
        //        ze.Password = clientCode.Substring(1, 9);
        //    }

        //    // Add tracking content
        //    zip.AddEntry("NPDDLP.txt", parentDir);

        //    zip.Save(archive);

    }
}
