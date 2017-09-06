using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCommander
{
    public static class Copier
    {
        public static async void CopyMetod(FilecopyDialog dialog)
        {
            try
            {
                await Copier.CopyFiles(dialog, prog => dialog.CopyModel.ProgressValue = prog);
            }
            catch (OperationCanceledException)
            {
                foreach (var item in dialog.CopyModel.SourceDestination)
                {
                    if (File.Exists(item.Value))
                        File.Delete(item.Value);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                dialog.Close();
            }
        }

        public static async Task CopyFiles(FilecopyDialog dialog, Action<double> progressCallback)
        {
            long total_size = dialog.CopyModel.SourceDestination.Keys.Select(x => new FileInfo(x).Length).Sum();
            long total_read = 0;
            double progress_size = 10000.0;

            foreach (var item in dialog.CopyModel.SourceDestination)
            {
                long total_read_for_file = 0;
                var from = item.Key;
                var to = item.Value;
                dialog.CopyModel.CopyMessage = string.Format("Копируем из {0} в {1}", from, to);
                using (var outStream = new FileStream(to, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (var inStream = new FileStream(from, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await CopyStream(dialog, inStream, outStream, x =>
                        {
                            total_read_for_file = x;
                            progressCallback(((total_read + total_read_for_file) / (double)total_size) * progress_size);
                        });
                    }
                }

                total_read += total_read_for_file;
            }
        }

        public static async Task CopyStream(FilecopyDialog dialog, Stream from, Stream to, Action<long> progress)
        {
            int buffer_size = 10240;
            byte[] buffer = new byte[buffer_size];
            long total_read = 0;

            while (total_read < from.Length)
            {
                await dialog.m_pauseTokeSource.Token.WaitWhilePausedAsync();
                dialog.m_cancelationTokenSource.Token.ThrowIfCancellationRequested();
                int read = await from.ReadAsync(buffer, 0, buffer_size);
                await to.WriteAsync(buffer, 0, read);
                total_read += read;
                progress(total_read);
            }
        }
    }
}
