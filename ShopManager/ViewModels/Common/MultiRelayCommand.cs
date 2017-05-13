using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopManager.ViewModels.Common
{
    public class MultiRelayCommand : ICommand
    {
        private List<RelayCommand> singleCommands;

        public MultiRelayCommand(RelayCommand[] arr)
        {
            singleCommands = new List<RelayCommand>(arr);
        }

        public bool CanExecute(object parameter)
        {
            foreach (var func in singleCommands)
            {
                if (func.CanExecute(parameter) == false)
                    return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            foreach (var action in singleCommands)
            {
               action.Execute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
