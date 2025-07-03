using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Brushes = System.Windows.Media.Brushes;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace CommonLib.Controls;
public class WindowZoom
{
    public static readonly DependencyProperty IgnoreMouseWheelWindowZoomProperty =
        DependencyProperty.RegisterAttached("IgnoreMouseWheelWindowZoom", typeof(bool), typeof(WindowZoom),
            new PropertyMetadata(false));

    public static readonly DependencyProperty IsZoomWithMouseWheelEnabledProperty =
        DependencyProperty.RegisterAttached("IsZoomWithMouseWheelEnabled", typeof(bool), typeof(WindowZoom),
            new PropertyMetadata(false, OnIsZoomWithMouseWheelEnabledChanged));


    public static bool GetIgnoreMouseWheelWindowZoom(DependencyObject obj)
    {
        return (bool)obj.GetValue(IgnoreMouseWheelWindowZoomProperty);
    }

    public static void SetIgnoreMouseWheelWindowZoom(DependencyObject obj,
        bool value)
    {
        obj.SetValue(IgnoreMouseWheelWindowZoomProperty, value);
    }


    public static bool GetIsZoomWithMouseWheelEnabled(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsZoomWithMouseWheelEnabledProperty);
    }

    public static void SetIsZoomWithMouseWheelEnabled(DependencyObject obj,
        bool value)
    {
        obj.SetValue(IsZoomWithMouseWheelEnabledProperty, value);
    }

    private static void OnIsZoomWithMouseWheelEnabledChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        if (d is ContentControl wnd)
        {
            wnd.PreviewMouseWheel += OnMouseWheel;
        }
    }

    private static void OnMouseWheel(object sender,
        MouseWheelEventArgs e)
    {
        var wnd = sender as ContentControl;
        var content = wnd?.Content as FrameworkElement;
        if (content == null)
        {
            return;
        }

        if (e.Source is DependencyObject dependencyObject && GetIgnoreMouseWheelWindowZoom(dependencyObject))
        {
            return;
        }

        var scale = new ScaleTransform(1.0, 1.0, 0.5, 0.5);
        if (!(content.LayoutTransform is ScaleTransform))
        {
            content.LayoutTransform = scale;
        }
        else
        {
            scale = (ScaleTransform)content.LayoutTransform;
        }

        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
        {
            if (e.Delta > 0)
            {
                var scaleValue = scale.ScaleX + 0.1;
                scale.ScaleX = scale.ScaleY = Math.Min(scaleValue, 1.5);
            }
            else
            {
                var scaleValue = scale.ScaleX - 0.1;
                scale.ScaleX = scale.ScaleY = Math.Max(scaleValue, 0.5);
            }

            e.Handled = true;

            var textBlock = new TextBlock
            {
                Text = $"{(scale.ScaleX * 100):0} %",
                Foreground = Brushes.Black,
                FontSize = 64,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                IsHitTestVisible = false
            };
            var animationTimeline = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(1.5)));
            textBlock.BeginAnimation(UIElement.OpacityProperty, animationTimeline);
        }
    }
}
