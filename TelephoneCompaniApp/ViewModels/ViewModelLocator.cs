﻿using Microsoft.Extensions.DependencyInjection;

namespace TelephoneCompaniApp.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();

        public StreetsWindowViewModel StreetsWindowViewModel => App.Services.GetRequiredService<StreetsWindowViewModel>();
    }
}
