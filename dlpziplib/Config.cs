using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32; // for registry
using System.Diagnostics; // for trace output
using System.IO; // for file copies
using System.Net; //for url copies

namespace dlpziplib
{
    public class Config
    {
        private const string localConfigFile = "DLPZip.config";

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
                Trace.WriteLine("DLPZip.exe: Read " + regval.ToString() + " from " + keyName);
                ncts = DateTime.FromBinary(regval);
                Trace.WriteLine("DLPZip.exe: Parsed ncts as " + ncts.ToString());
            }
            catch (NullReferenceException)
            { 
                ncts = DateTime.MinValue;
                Trace.WriteLine("DLPZip.exe: " + keyName + " not read, using " + ncts.ToString());
            }

            if (DateTime.Now >= ncts)
            {
                Trace.WriteLine("DLPZip.exe: Re-reading config");
                String configSource;

                try
                {
                    var regval = (String)Registry.GetValue(keyName, "ConfigLocation", "");
                    Trace.WriteLine("DLPZip.exe: Read '" + regval.ToString() + "' from " + keyName);
                    if (regval.Length == 0)
                        throw new ArgumentNullException("ConfigLocation not defined");
                    configSource = regval.ToString();
                }
                catch (NullReferenceException)
                {
                    Trace.WriteLine("DLPZip.exe: " + keyName + " not read, using ConfigLocation");
                    throw new ArgumentNullException("ConfigLocation");
                }

                if (configSource.StartsWith("http"))
                {
                    Trace.WriteLine("DLPZip.exe: ConfigLocation is a URL");

                    using (var client = new WebClient())
                    {
                        client.DownloadFile(configSource, localConfigFile);
                    }
                }
                else
                {
                    Trace.WriteLine("DLPZip.exe: Assuming ConfigLocation is a path");
                    File.Copy(configSource, localConfigFile, true);
                }
                //TODO write time + delay to registry
                Registry.SetValue(keyName, "NCTS", DateTime.Now.ToBinary(), RegistryValueKind.QWord);
            }
            else
            {
                Trace.WriteLine("Not re-reading config");
            }

            ParseConfig();

            //string json = @"{""key1"":""value1"",""key2"":""value2""}";
            //JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            Trace.WriteLine("DLPZip.exe: ~ReadConfig()");
        }

        private static void ParseConfig()
        {
            //decrypt
            Trace.WriteLine("DLPZip.exe: Config encryption not implemented");
            return;
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
