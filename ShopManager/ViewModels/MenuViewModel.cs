using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ShopManager.ViewModels.Common;

namespace ShopManager.ViewModels
{
    public class MenuViewModel
    {
        private ICommand _aboutCommand;
        private ICommand _exitCommand;

        public ICommand AboutCommand
        {
            get
            {
                if (_aboutCommand == null)
                {
                    _aboutCommand = new RelayCommand(x =>
                    {
                        MessageBox.Show("This is simple shop manager application", "About", MessageBoxButton.OK,
                            MessageBoxImage.Asterisk);
                    });
                }

                return _aboutCommand;
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                {
                    _exitCommand = new RelayCommand(x=>
                    {
                        Application.Current.MainWindow.Close();
                    });
                }
                return _exitCommand;
            }
        }

        public MenuViewModel()
        {
           
        }
    }
}
