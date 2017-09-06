using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCommander
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileCopyViewModel copyModel;
        private DirectoriesViewModel directories;

        public MainWindow()
        {
            InitializeComponent();
            directories = new DirectoriesViewModel();
            this.DataContext = directories;
        }

        void LeftlistViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = e.Source as ListViewItem;
            DirectoryEntry entry = item.DataContext as DirectoryEntry;

            if (entry.Type == EntryType.Dir || entry.Type == EntryType.Drive)
            {
                directories.LeftEntries.Clear();
                directories.LeftPath = entry.Fullpath;
                directories.LeftEntries = DirectoryStructure.GetDirectoryContents(entry.Fullpath);
            }
        }

        void RightlistViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = e.Source as ListViewItem;
            DirectoryEntry entry = item.DataContext as DirectoryEntry;

            if (entry.Type == EntryType.Dir || entry.Type == EntryType.Drive)
            {
                directories.RightEntries.Clear();
                directories.RightPath = entry.Fullpath;
                directories.RightEntries = DirectoryStructure.GetDirectoryContents(entry.Fullpath);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<DirectoryEntry> selectedItems = new List<DirectoryEntry>();
            string path = string.Empty;
            Dictionary<string, string> dict = new Dictionary<string, string>(); 

            if (this.directories.ActivePanel == ActivePanel.Right)
            {
                var selection = this.RightView.SelectedItems;
                foreach (var d in selection)
                {
                    selectedItems.Add(d as DirectoryEntry);
                }
                path = this.LeftPath.Text;
            }
            else
            {
                var selection = this.LeftView.SelectedItems;
                foreach (var d in selection)
                {
                    selectedItems.Add(d as DirectoryEntry);
                }
                path = this.RightPath.Text;
            }
            if (selectedItems.Count > 0 && path != "..")
            {
                if (path.Last() != '\\')
                    path += '\\';
                selectedItems.ForEach(_ => dict.Add(_.Fullpath, path + _.Name));
                copyModel = new FileCopyViewModel(dict);
                var copyDiag = new FilecopyDialog();
                copyDiag.DataContext = copyModel;
                copyDiag.ContinuePanel.Visibility = Visibility.Hidden;
                copyDiag.CopyModel = copyModel;
                copyDiag.Show();
            }
            else
            {
                MessageBox.Show("Не выбраны файлы для копирования или каталог в который будем копировать!");
            }
        }

        private void RightView_Selected(object sender, RoutedEventArgs e)
        {
            directories.ActivePanel = ActivePanel.Right;
        }

        private void LeftView_Selected(object sender, RoutedEventArgs e)
        {
            directories.ActivePanel = ActivePanel.Left;
        }
    }
}

