using System;
using System.Windows;
using TelephoneCompaniApp.Infrastructure.Commands.Base;

namespace TelephoneCompaniApp.Infrastructure.Commands
{
    internal class CloseWindow : Command
    {

        protected override bool CanExecute(object p) => App.CurrentWindow != null;

        protected override void Execute(object p) => App.CurrentWindow.Close();
    }
}
