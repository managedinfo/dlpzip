using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32; // for registry
using System.Diagnostics; // for trace output

namespace dlpzip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string[] args = Environment.GetCommandLineArgs();
            ReadConfig();
            if(false) InitializeComponent();
        }

        private void ReadConfig()
        {
            DateTime ncts; // next check time stamp
           
            const string userRoot = "HKEY_LOCAL_MACHINE";
            const string subkey = "SOFTWARE\\DLPZip";
            const string keyName = userRoot + "\\" + subkey;

            RegistryKey rkey = Registry.LocalMachine.OpenSubKey("SOFTWARE");

            //var result = (long)rk.GetValue("NCTS", 0);
            String[] names = rkey.GetSubKeyNames();
            foreach (String s in names)
                Trace.WriteLine(s);

            //ncts = DateTime.FromBinary(result);

            if(1 > 0)
            {
                Trace.WriteLine("rereading config");
            }
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
