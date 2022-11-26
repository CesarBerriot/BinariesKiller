using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinariesKiller
{
    internal class Program
    {
        static void DeleteFolder(string _name)
        {
            if (Directory.Exists(_name))
            {
                Console.WriteLine($"deleted {_name}/");
                Directory.Delete(_name, true);
            }
        }
        static void DeleteFilesOfExtension(string _ext)
        {
            FileInfo[] _files = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles($"*.{_ext}");
            for (int i = 0; i < _files.Length; i++)
            {
                Console.WriteLine($"deleted {_files[i].Name}");
                if (File.Exists(_files[i].Name))
                    File.Delete(_files[i].Name);
            }
        }
        static void Main(string[] args)
        {
            string[] _folders = { "Binaries", ".vs", "Intermediate", "Sript", "DerivedDataCache" };
            foreach (string _folder in _folders)
                DeleteFolder(_folder);
            DeleteFilesOfExtension("sln");
        }
    }
}
