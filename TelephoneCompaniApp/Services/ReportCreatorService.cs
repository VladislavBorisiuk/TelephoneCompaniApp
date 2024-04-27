using Microsoft.Identity.Client.NativeInterop;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using TelephoneCompaniApp.Services.Interfaces;

namespace TelephoneCompaniApp.Services
{
    internal class ReportCreatorService : IReportCreatorService
    {
        public void CreateReport(ObservableCollection<MainDataGridItem> items)
        {
            string fileName = DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss");
            fileName = $"report_{fileName}.csv";

            var dialog = new SaveFileDialog();
            dialog.Filter = "CSV файлы (.csv)|.csv|Все файлы (.)|.";
            dialog.FileName = fileName;

            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            using (var writer = new StreamWriter(dialog.FileName))
            {
                writer.WriteLine("FullName,Street,HouseNumber,HomePhoneNumber,WorkPhoneNumber,MobilePhoneNumber");

                foreach (var item in items)
                {
                    writer.WriteLine($"{item.FullName},{item.Street},{item.HouseNumber},{item.PhoneNumbers[0]},{item.PhoneNumbers[1]},{item.PhoneNumbers[2]}");
                }
            }

            MessageBox.Show("Файл успешно сохранён.");
        }
    }
}

