using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinariesKiller
{
    internal static class Program
    {
        enum Verbosity
        {
            Info = ConsoleColor.Green,
            Warning = ConsoleColor.Yellow,
            Error = ConsoleColor.Red,
        }

        static void SetVerbosity(Verbosity _v) => Console.ForegroundColor = (ConsoleColor)_v;

        static void Log(Verbosity _verbosity, string _verbosityMsg, string _msg, bool _inline = false)
        {
            SetVerbosity(_verbosity);
            Console.Write(_verbosityMsg + _msg);
            if (!_inline)
                Console.WriteLine();
        }

        static void LogError(string _msg, bool _inline = false) => Log(Verbosity.Error, "[Error] ", _msg, _inline);
        static void LogWarning(string _msg, bool _inline = false) => Log(Verbosity.Warning, "[Warning] ", _msg, _inline);
        static void LogInfo(string _msg, bool _inline = false) => Log(Verbosity.Info, "[Info] ", _msg, _inline);

        static void TryDelete(Action<string> _deleteFunc, string _type, string _name)
        {
            try
            {
                _deleteFunc(_name);
                LogInfo($"deleted {_type} : [{_name}]");
            }
            catch (Exception)
            {
                LogError($"Failed to delete {_type} [{_name}]\nPress enter to continue.", true);
                Console.ReadLine();
            }
        }

        static void DeleteFolder(string _name)
        {
            if (!Directory.Exists(_name))
            {
                LogWarning($"Couldn't find folder [{_name}]");
                return;
            }

            TryDelete((string _folderName) => Directory.Delete(_folderName, true), "folder", _name);
        }

        static void DeleteFilesOfExtension(string _ext)
        {
            FileInfo[] _files = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles($"*.{_ext}");
            foreach (FileInfo _file in _files)
                TryDelete(File.Delete, "file", _file.Name);
        }

        static void Main()
        {
            string[] _folders = { "Binaries", "Saved", ".vs", ".idea", "Intermediate", "Script", "DerivedDataCache" };
            foreach (string _folder in _folders)
                DeleteFolder(_folder);
            DeleteFilesOfExtension("sln");
            LogInfo("Done. Press any key to continue");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }
    }
}