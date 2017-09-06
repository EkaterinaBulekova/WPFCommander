using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfCommander
{
    public partial class FilecopyDialog : Window
    {
        public PauseTokenSource m_pauseTokeSource = null;
        public CancellationTokenSource m_cancelationTokenSource = null;
        public FileCopyViewModel CopyModel { get; set; }

        public FilecopyDialog()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            m_cancelationTokenSource.Cancel();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            m_pauseTokeSource.IsPaused = !m_pauseTokeSource.IsPaused;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            m_pauseTokeSource = new PauseTokenSource();
            m_cancelationTokenSource = new CancellationTokenSource();
            this.StartPanel.Visibility = Visibility.Hidden;
            this.ContinuePanel.Visibility = Visibility.Visible;
            Copier.CopyMetod(this);
        }
    }
}
