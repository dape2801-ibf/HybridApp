using System;
using System.Windows;
using CommonLib.Contracts;

namespace CommonLib.DependencyInjection;

public class DIServiceLocator
{
    public static readonly DependencyProperty ContainerProperty =
        DependencyProperty.RegisterAttached("Container", typeof(IDependencyInjectionContainer),
            typeof(DIServiceLocator),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    ///     Application wide default DI Container defined by <see cref="SetInstance"/>.
    ///     Set when bootstrapping the application.
    /// </summary>
    public static IDependencyInjectionContainer Instance { get; private set; }

    /// <summary>
    ///     Get the DI Container for a specific <see cref="DependencyObject"/>
    ///     by accessing the Container on it.
    ///     This usually means accessing the DI Container of the parent window,
    ///     since the DependencyProperty is inherited.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static IDependencyInjectionContainer GetContainer(DependencyObject obj)
    {
        if (!obj.Dispatcher.CheckAccess())
        {
            return obj.Dispatcher.Invoke(() => GetContainer(obj));
        }
        return (IDependencyInjectionContainer)obj.GetValue(ContainerProperty);
    }

    /// <summary>
    ///     Set the DI Container on a DependencyObject.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void SetContainer(DependencyObject obj,
        IDependencyInjectionContainer value)
    {
        obj.SetValue(ContainerProperty, value);
    }

    /// <summary>
    ///     Set the default DI Container for the whole application.
    /// </summary>
    /// <param name="instance"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetInstance(IDependencyInjectionContainer instance)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        if (Instance == null)
        {
            Instance = instance;
        }
    }

    /// <summary>
    ///     Same as <see cref="GetContainer(DependencyObject)"/> but defaults to
    ///     <see cref="Instance"/> if no DI Container is found on the DependencyObject
    ///     or its parents.
    /// </summary>
    /// <param name="dependencyObject"></param>
    /// <returns></returns>
    public static IDependencyInjectionContainer LookupContainer(DependencyObject dependencyObject)
    {
        if (dependencyObject.Dispatcher != null &&
            !dependencyObject.Dispatcher.CheckAccess())
        {
            return dependencyObject.Dispatcher.Invoke(() => LookupContainer(dependencyObject));
        }
        var container = GetContainer(dependencyObject);
        return container ?? Instance;
    }
}
