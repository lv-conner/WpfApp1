using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1
{
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    class DelegateCommand : ICommand
    {
        //A method prototype without return value.
        public Action<object> ExecuteCommand = null;
        //A method prototype return a bool type.
        public Func<object, bool> CanExecuteCommand = null;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (CanExecuteCommand != null)
            {
                return this.CanExecuteCommand(parameter);
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            if (this.ExecuteCommand != null) this.ExecuteCommand(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }



    public class Model : NotificationObject
    {
        private string _wpf = "WPF";

        public string WPF
        {
            get { return _wpf; }
            set
            {
                _wpf = value;
                this.RaisePropertyChanged("WPF");
            }
        }

        public void Copy(object obj)
        {
            this.WPF += " WPF";
        }

    }
    class ViewModel
    {
        public DelegateCommand CopyCmd { get; set; }
        public Model model { get; set; }

        public ViewModel()
        {
            this.model = new Model();
            this.CopyCmd = new DelegateCommand();
            this.CopyCmd.ExecuteCommand = new Action<object>(this.model.Copy);
        }
    }
}
