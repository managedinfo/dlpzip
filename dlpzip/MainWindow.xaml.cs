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

namespace dlpzip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //TODO Glob input before doing this?
        Console.WriteLine("Adding {0} files to {1}", inputfiles.Count, archive);

            Regex rgx = new Regex(filenamePattern, RegexOptions.IgnoreCase);
        String filename;
        String clientCode = null;
        String parentDir = null;
        Match m;

            foreach (String path in inputfiles)
            {
                Console.Write(" + {0}", path);
                filename = Path.GetFileNameWithoutExtension(path);
                m = rgx.Match(filename);
                if (m.Success)
                { 
                    Console.Write(" - Correct Name Format");
                    if (clientCode == null)
                    {
                        clientCode = m.Value;
                        Console.Write(" - Setting client code to '{0}'", clientCode.Substring(0,10));
                    }
                    else if(filename.StartsWith(clientCode))
                    {
                        Console.Write(" - Matching client code");
                    }
                    else
                    {
                        Console.WriteLine(" - Mismatched client code");
                        return;
                    }

                    String fileParentDir = Path.GetDirectoryName(Path.GetFullPath(path));
fileParentDir = fileParentDir.Substring(fileParentDir.LastIndexOf(@"\") + 1);

                    if (parentDir == null)
                    {
                        parentDir = fileParentDir;
                        Console.WriteLine(" - Parent directory set to '{0}'", parentDir);
                    }
                    else
                    {
                        if (String.Compare(parentDir, fileParentDir, true) == 0)
                        {
                            Console.WriteLine(" - Matching parent directory");
                        }
                        else
                        {
                            Console.WriteLine(" - Mismatched parent directory '{0}'", fileParentDir);
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" - Ìncorrect Name Format '{0}'", filename);
                    return;
                }
            }

            using (var zip = new ZipFile())
            {
                foreach (String path in inputfiles)
                {
                    filename = Path.GetFileName(path);
                    filename = filename.Replace(clientCode, "");
                    ZipEntry ze = zip.AddFile(path);
                    ze.FileName = filename;
                    ze.Password = clientCode.Substring(1, 9);
                }

                // Add tracking content
                zip.AddEntry("NPDDLP.txt", parentDir);

                zip.Save(archive);
            }

    }
}
