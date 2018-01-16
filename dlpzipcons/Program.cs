using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; // for trace output
using System.Text.RegularExpressions; //to parse arguments
using Newtonsoft.Json; // to write config
using System.IO; // to write config

using dlpziplib;


namespace dlpzipcons
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                System.Console.WriteLine("Usage: to be completed");
                return;
            }
            else if (args[0] == @"writeConfig")
            {
                DLPZipConfig newConfig = new DLPZipConfig();
                Trace.WriteLine("DLPZip.exe: Writing Configuration file");

                String pattern = @"^(.*):""?(.*)""?";

                for (int i = 1; i < args.Length; i++)
                {
                    foreach (Match m in Regex.Matches(args[i], pattern))
                    {
                        String key = m.Groups[1].Value;
                        String value = m.Groups[2].Value;
                        switch (key)
                        {
                            case "configRefreshPeriod":
                                Console.WriteLine("{0} = {1}", key, value);
                                newConfig.configRefreshPeriod = uint.Parse(value);
                                break;
                            case "defaultKey":
                                Console.WriteLine("{0} = {1}", key, value);
                                newConfig.defaultKey = value;
                                break;
                            default:
                                Console.WriteLine("Unknown config item {0}", key);
                                return;
                        }
                    }
                }
                using (StreamWriter sw = File.CreateText(Config.localConfigFile))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(newConfig));
                }
                return;
            }
            Config.ReadConfig();
        }
    }
}
