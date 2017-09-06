using System;

namespace WpfCommander
{
    public class DirectoryEntry
    {
        private string _name;
        private string _fullpath;
        private string _ext;
        private string _size;
        private DateTime _date;
        private EntryType _type;

        public DirectoryEntry(string name, string fullname, string ext, string size, DateTime date, EntryType type)
        {
            _name = name;
            _fullpath = fullname;
            _ext = ext;
            _size = size;
            _date = date;
            _type = type;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Ext
        {
            get { return _ext; }
            set { _ext = value; }
        }

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public EntryType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Fullpath
        {
            get { return _fullpath; }
            set { _fullpath = value; }
        }
    }
}
