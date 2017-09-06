using PropertyChanged;
using System.ComponentModel;

namespace WpfCommander
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
       public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
