using NmeaParser.ViewModels;
using System;
using System.Windows.Input;

namespace NmeaParser.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Result")
            {
                viewModel.SelectedViewModel = new ResultViewModel();
            }
            else if (parameter.ToString() == "Message")
            {
                viewModel.SelectedViewModel = new MessageViewModel();
            }
        }
    }
}