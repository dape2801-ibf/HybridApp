using System.Windows;
using CommonLib.Controls;

namespace HybridApp.Views;

public partial class AboutWindow : HybridWindow
{
    public AboutWindow()
    {
        InitializeComponent();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}