using System;
using System.Windows.Input;

namespace TelephoneCompaniApp.Infrastructure.Commands
{
    class DialogCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool? DialogResult { get; set; }

        public bool CanExecute(object? parameter) => App.CurrentWindow != null;

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            var window = App.CurrentWindow;

            var dialog_result = DialogResult;
            if (parameter is string stringParameter)
            {
                if (!bool.TryParse(stringParameter, out var boolValue))
                {
                    return;
                }

                dialog_result = boolValue;
            }
            else
            {
                return;
            }

            window.DialogResult = dialog_result;
            window.Close();
        }
    }
}
