using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCommander
{
    public enum ActivePanel
    {
        Left,
        Right
    }

    public class DirectoriesViewModel : BaseViewModel
    {
        public ObservableCollection<DirectoryEntry> LeftEntries { get; set; }

        public ObservableCollection<DirectoryEntry> RightEntries { get; set; }

        public string LeftPath { get; set; }

        public string RightPath { get; set; }

        public ActivePanel ActivePanel { get; set; }

        public DirectoriesViewModel()
        {
            this.ActivePanel = ActivePanel.Left;
            this.LeftEntries = DirectoryStructure.GetAllLogicalDrives();
            this.RightEntries = DirectoryStructure.GetAllLogicalDrives();
            this.RightPath = "..";
            this.LeftPath = "..";
        }
    }
}
