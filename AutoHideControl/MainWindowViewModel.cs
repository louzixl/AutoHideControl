using AutoHideControl.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoHideControl
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public bool ShowView
        {
            get => _showView;
            set 
            { 
                _showView = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand ButtonClickedCommand =>
            _buttonClickedCommand ?? (_buttonClickedCommand = new DelegateCommand { ExecuteAction = (_)=> ShowView = !_showView });

        public void OnPropertyChanged([CallerMemberName] string name = "")=>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _showView;
        private DelegateCommand _buttonClickedCommand;
    }
}
