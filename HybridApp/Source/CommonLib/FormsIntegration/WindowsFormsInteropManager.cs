using System;
using System.Windows;
using System.Windows.Forms;
using CommonLib.Contracts;
using CommonLib.Controls;

namespace CommonLib.FormsIntegration;
/// <summary>
/// Demonstrates how to integrate Windows Forms windows into a WPF application.
/// This class provides functionality to host a Windows Forms <see cref="Form"/> within a WPF <see cref="Window"/>.
/// It handles the initialization, display, and property adjustments required for seamless interoperation.
/// </summary>
internal class WindowsFormsInteropManager
{
    /// <summary>
    /// The Windows Forms <see cref="Form"/> to be hosted within the WPF application.
    /// </summary>
    private readonly Form formToHost;

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowsFormsInteropManager"/> class.
    /// </summary>
    /// <param name="formToHost">The Windows Forms <see cref="Form"/> to be hosted.</param>
    public WindowsFormsInteropManager(Form formToHost)
    {
        this.formToHost = formToHost;
        formToHost.MdiChildActivate += OnMdiChildActivate;
    }

    /// <summary>
    /// Handles the activation of MDI child forms within the hosted Windows Forms <see cref="Form"/>.
    /// Ensures that the active child form's text is cleared.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">Event arguments.</param>
    private void OnMdiChildActivate(object sender, EventArgs e)
    {
        if (formToHost.ActiveMdiChild != null)
        {
            formToHost.ActiveMdiChild.Text = @" ";
        }
    }

    /// <summary>
    /// Displays the hosted Windows Forms <see cref="Form"/> as a modal dialog within a WPF <see cref="Window"/>.
    /// </summary>
    /// <param name="container">The dependency injection container for resolving dependencies.</param>
    /// <returns>The dialog result of the hosted form.</returns>
    public DialogResult ShowWpfWindowWithFormsContentDialog(IDependencyInjectionContainer container)
    {
        var window = InitializeWindow(container);
        window.ShowDialog();
        return formToHost.DialogResult;
    }

    /// <summary>
    /// Displays the hosted Windows Forms <see cref="Form"/> within a WPF <see cref="Window"/> without modal behavior.
    /// </summary>
    /// <param name="container">The dependency injection container for resolving dependencies.</param>
    public void ShowWpfWindowWithFormsContent(IDependencyInjectionContainer container)
    {
        var window = InitializeWindow(container);
        window.Show();
    }

    /// <summary>
    /// Initializes a WPF <see cref="Window"/> to host the Windows Forms <see cref="Form"/>.
    /// Configures the window properties and integrates the form using a <see cref="WindowsFormsHost"/>.
    /// </summary>
    /// <param name="container">The dependency injection container for resolving dependencies.</param>
    /// <returns>The initialized WPF <see cref="Window"/>.</returns>
    private Window InitializeWindow(IDependencyInjectionContainer container)
    {
        var window = new WindowsFormsWrapperWindow(container);
        AdjustProperties(window);
        window.Content = window.HwndHost = new WindowsFormsHost(new Win32FormsAdapter(window), formToHost);
        return window;
    }

    /// <summary>
    /// Adjusts the properties of the WPF <see cref="Window"/> to match the hosted Windows Forms <see cref="Form"/>.
    /// Ensures proper positioning, size, and startup behavior.
    /// </summary>
    /// <param name="window">The WPF <see cref="Window"/> to adjust.</param>
    private void AdjustProperties(HybridWindow window)
    {
        var originalSize = formToHost.Size;
        formToHost.ControlBox = false;
        formToHost.FormBorderStyle = FormBorderStyle.None;

        window.Title = formToHost.Text;
        window.Width = originalSize.Width;
        window.Height = originalSize.Height;
        window.WindowName = formToHost.Name;

        if (formToHost.Location.X != 0 && formToHost.Location.Y != 0)
        {
            window.WindowStartupLocation = WindowStartupLocation.Manual;
            window.Left = formToHost.Location.X;
            window.Top = formToHost.Location.Y;
        }
    }
}
