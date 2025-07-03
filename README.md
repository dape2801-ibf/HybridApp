# HybridApp – Integrating WinForms into WPF

This sample project demonstrates how to integrate existing WinForms applications or individual WinForms components into a modern WPF application. The goal is to enable a gradual migration from WinForms to WPF without requiring a complete rewrite of the application at once.

## Motivation

Many organizations have large WinForms applications that need to be migrated to WPF over time. This solution allows WinForms dialogs and windows to be seamlessly embedded into a WPF interface, enabling a step-by-step migration process.

## Technical Overview

- **.NET 8**: Modern framework for future-proof development.
- **WPF as Host**: The main application is built with WPF.
- **WinForms Integration**: The [`WindowsFormsHost`](Source/CommonLib/FormsIntegration/WindowsFormsHost.cs) class enables hosting WinForms forms within WPF windows and managing their lifecycle.
- **HybridWindow**: An extensible WPF window base class, prepared for custom styles and templates.
- **Sample Views**: Demonstrate how WinForms and WPF components can be used together.

## Key Components

- [`WindowsFormsHost`](Source/CommonLib/FormsIntegration/WindowsFormsHost.cs):  
  Encapsulates the integration of a WinForms form into a WPF window, handling focus, lifecycle, and event bridging between the UI technologies.
- [`HybridWindow`](Source/HybridApp/Controls/HybridWindow.cs):  
  Base class for WPF windows, ready for custom styling and future enhancements.

## Benefits

- **Incremental Migration**: Continue using existing WinForms components and gradually replace them with WPF.
- **Seamless User Experience**: WinForms dialogs appear as native parts of the WPF application.
- **Reusability**: The provided classes can be easily adapted for your own migration projects.

## Getting Started

1. Open the solution in Visual Studio 2022 (.NET 8).
2. Explore the project **LegacyWinFormsApp** to take a look at the legacy WinForms code.
3. Explore the project **HybridApp** which is the new WPF shell to call WPF windows and also WinForms forms.

## Further Notes

- This solution is designed as a blueprint for your own migration projects.
- For production scenarios, consider additional aspects such as data binding, MVVM, and resource management.

---

**Questions or feedback?**  
Feel free to open an issue in the repository or get in touch directly.
