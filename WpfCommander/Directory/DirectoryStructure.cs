using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace WpfCommander
{
    class DirectoryStructure
    {
        public static ObservableCollection<DirectoryEntry> GetAllLogicalDrives()
        {
            return new ObservableCollection<DirectoryEntry> (Directory.GetLogicalDrives()
                .Select(drive => new DirectoryEntry(drive, drive, "<Driver>", "<DIR>", 
                Directory.GetLastWriteTime(drive), EntryType.Drive)));
        }

        public static ObservableCollection<DirectoryEntry> GetDirectoryContents(string fullPath)
        {
            var items = new ObservableCollection<DirectoryEntry>();

            if (fullPath == "..")
                return GetAllLogicalDrives();

            if (fullPath.Length > 3)
            {
                var path = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
                if (path.Length < 3) path += '\\';
                DirectoryEntry h = new DirectoryEntry(
                "..", path, "<Folder>", "<DIR>",
                Directory.GetLastWriteTime(fullPath), EntryType.Dir);
                items.Add(h);
            }
            else
            {
                DirectoryEntry h = new DirectoryEntry(
                "..", "..", "<Folder>", "<DIR>",
                DateTime.Today, EntryType.Drive);
                items.Add(h);
            }

            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    foreach (var directory in dirs)
                    {
                      DirectoryInfo dir = new DirectoryInfo(directory);
                      DirectoryEntry d = new DirectoryEntry(
                                dir.Name, dir.FullName, "<Folder>", "<DIR>",
                                Directory.GetLastWriteTime(directory), EntryType.Dir);
                        items.Add(d);
                    }
            }
            catch
            {
            }

            try
            {
                var files = Directory.GetFiles(fullPath);
                if (files.Length > 0)
                    foreach (string f in files)
                    {
                        FileInfo file = new FileInfo(f);
                        DirectoryEntry fl = new DirectoryEntry(
                            file.Name, file.FullName, file.Extension, file.Length.ToString(),
                            file.LastWriteTime, EntryType.File);
                        items.Add(fl);
                    }
            }
            catch
            {
            }

            return items;
        }
    }
}
