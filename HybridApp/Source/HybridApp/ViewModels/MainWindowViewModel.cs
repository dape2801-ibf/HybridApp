using System.Windows;
using System.Windows.Input;
using CommonLib.Contracts;
using HybridApp.Implementations;
using LegacyWinForms.Contracts;
using MessageBox = System.Windows.MessageBox;

namespace HybridApp.ViewModels;

public class MainWindowViewModel
{
    private readonly IHybridAppDialogService _dialogService;
    private readonly ILegacyWinFormsCallbacks _winFormsCallbacks;
    public MainWindowViewModel(IHybridAppDialogService dialogService, ILegacyWinFormsCallbacks winFormsCallbacks)
    {
        _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        _winFormsCallbacks = winFormsCallbacks ?? throw new ArgumentNullException(nameof(winFormsCallbacks));
        OpenAboutWindow = new RelayCommand(OpenAboutWindowExecute);
        OpenCustomerManagement = new RelayCommand(OpenCustomerManagementExecute);
        OpenOrderManagement = new RelayCommand(OpenOrderManagementExecute);
        OpenActionDialog = new RelayCommand(OpenActionDialogExecute);
        OpenWinFormsApplication = new RelayCommand(OpenWinFormsApplicationExecute);
    }

    private void OpenWinFormsApplicationExecute()
    {
        _winFormsCallbacks.OpenMainApplication();
    }

    private void OpenOrderManagementExecute()
    {
        _winFormsCallbacks.OpenOrderManagement();
    }

    private void OpenCustomerManagementExecute()
    {
        _winFormsCallbacks.OpenCustomerManagement();
    }

    private async void OpenActionDialogExecute()
    {
        if (await _winFormsCallbacks.OpenActionDialog())
        {
            ShowMessageBox("DialogResult: OK");
        }
        else
        {
            ShowMessageBox("DialogResult: Cancel");
        }
    }

    public ICommand OpenAboutWindow { get; set; }

    public ICommand OpenCustomerManagement { get; set; }

    public ICommand OpenOrderManagement { get; set; }
    
    public ICommand OpenActionDialog { get; set; }
    
    public ICommand OpenWinFormsApplication { get; set; }

    private async void OpenAboutWindowExecute()
    {
        await _dialogService.ShowWindow(typeof(Views.AboutWindow));
    }

    private void ShowMessageBox(string message)
    {
        MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}