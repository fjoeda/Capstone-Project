using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DataCombiner
{
    class Program
    {
        List<string> lines = new List<string>();
        static void Main(string[] args)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] filePaths = Directory.GetFiles(dialog.SelectedPath, "preprocessed*.txt");
                foreach(string file in filePaths)
                {
                    Console.WriteLine(file);
                }
            }



            Console.ReadLine();
        }
    }
}
