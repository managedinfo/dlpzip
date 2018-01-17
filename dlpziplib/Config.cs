using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32; // for registry
using System.Diagnostics; // for trace output
using System.IO; // for file copies
using System.Net; //for url copies
using Newtonsoft.Json;

namespace dlpziplib
{
    public class Config
    {
        public const string localConfigFile = "DLPZip.config";

        public static DLPZipConfig ReadConfig()
        {
            DLPZipUtil.Trace("ReadConfig()");
            DateTime ncts; // next check time stamp
            DLPZipConfig config;

            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "SOFTWARE\\DLPZip";
            const string keyName = userRoot + "\\" + subkey;

 
            try
            {
                var regval = (long)Registry.GetValue(keyName, "NCTS", 0);
                DLPZipUtil.Trace("Read " + regval.ToString() + " from " + keyName);
                ncts = DateTime.FromBinary(regval);
                DLPZipUtil.Trace("Parsed ncts as " + ncts.ToString());
            }
            catch (NullReferenceException)
            { 
                ncts = DateTime.MinValue;
                DLPZipUtil.Trace(keyName + " not read, using " + ncts.ToString());
            }

            if (DateTime.Now >= ncts)
            {
                DLPZipUtil.Trace("Re-reading config");
                String configSource;

                try
                {
                    var regval = (String)Registry.GetValue(keyName, "ConfigLocation", "");
                    DLPZipUtil.Trace("Read '" + regval.ToString() + "' from " + keyName);
                    if (regval.Length == 0)
                        throw new ArgumentNullException("ConfigLocation not defined");
                    configSource = regval.ToString();
                }
                catch (NullReferenceException)
                {
                    DLPZipUtil.Trace(keyName + " not read, using ConfigLocation");
                    throw new ArgumentNullException("ConfigLocation");
                }

                if (configSource.StartsWith("http"))
                {
                    DLPZipUtil.Trace("ConfigLocation is a URL");

                    using (var client = new WebClient())
                    {
                        client.DownloadFile(configSource, localConfigFile);
                    }
                }
                else
                {
                    DLPZipUtil.Trace("Assuming ConfigLocation is a path");
                    File.Copy(configSource, localConfigFile, true);
                }
                config = ParseConfig();
                //TODO write time + delay to registry
                Registry.SetValue(keyName, "NCTS", DateTime.Now.AddSeconds(config.configRefreshPeriod).ToBinary(), RegistryValueKind.QWord);
            }
            else
            {
                DLPZipUtil.Trace("Not re-reading config");
                config = ParseConfig();
            }

            DLPZipUtil.Trace("~ReadConfig()");

            return config;
        }

        public static void WriteConfig(DLPZipConfig c)
        {
            DLPZipUtil.Trace("WriteConfig()");
            using (StreamWriter sw = File.CreateText(Config.localConfigFile))
            {
                sw.WriteLine(JsonConvert.SerializeObject(c));
            }
            DLPZipUtil.Trace("~WriteConfig()");
        }

        private static DLPZipConfig ParseConfig()
        {
            DLPZipUtil.Trace("ParseConfig()");
            DLPZipConfig config;
            //decrypt
            DLPZipUtil.Trace("Config encryption not implemented");

            using (StreamReader file = File.OpenText(localConfigFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                config = (DLPZipConfig)serializer.Deserialize(file, typeof(DLPZipConfig));
            }

            DLPZipUtil.Trace("~ParseConfig()");
            return config;
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
