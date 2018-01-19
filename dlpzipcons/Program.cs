using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions; //to parse arguments

using dlpziplib;


namespace dlpzipcons
{
    class Program
    {
        private static String archiveFile;
        private static DLPZipConfig config;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                DLPZipUsage();
                return;
            }
            else if (args[0] == @"writeConfig")
            {
                DLPZipConfig newConfig = new DLPZipConfig();
                DLPZipUtil.Trace("Writing Configuration file");

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
                Config.WriteConfig(newConfig);
                return;
            }

            config = Config.ReadConfig();

            archiveFile = args[1];
            DLPZipUtil.Trace("Archive file is", archiveFile);

            List<string> inputFiles = new List<string>();
            for (int i = 2; i < args.Length; i++)
            {
                DLPZipUtil.Trace("Input files:", args[i]);
                List<string> expandedfiles = DLPZipUtil.ExpandFiles(args[i]);
                inputFiles.AddRange(expandedfiles);
            }
          
            switch (args[0])
            {
                case "a":
                    DLPZipFile.addFiles(archiveFile, inputFiles);
                    break;
                case "b":
                case "d":
                case "e":
                case "h":
                case "i":
                case "l":
                case "rn":
                case "t":
                case "u":
                case "x":
                    Console.WriteLine("Argument ", args[0], "not implemented");
                    break;
                default:
                    DLPZipUsage();
                    break;
            }
        }

      private static void DLPZipUsage()
        {
            System.Console.WriteLine("Usage: to be completed");
        }
    }
}
