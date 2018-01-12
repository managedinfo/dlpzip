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
            //if(false) InitializeComponent();
        }

        private void ReadConfig()
        {
            DateTime ncts; // next check time stamp
           
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "SOFTWARE\\DLPZip";
            const string keyName = userRoot + "\\" + subkey;

            try
            {
                var regval = (long)Registry.GetValue(keyName, "NCTS", 0);
                ncts = DateTime.FromBinary(regval);
            }
            catch(NullReferenceException)
            {
                ncts = DateTime.MinValue;
            }

            if(DateTime.Now >= ncts)
            {
                Trace.WriteLine("rereading config");

                //TODO write time + delay to registry
                Registry.SetValue(keyName, "NCTS", DateTime.Now.ToBinary(), RegistryValueKind.QWord);
            }

            //string json = @"{""key1"":""value1"",""key2"":""value2""}";
            //JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
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
