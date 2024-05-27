using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using PeachPlayer.Foundation;
using ReactiveUI;
using Splat;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace PeachPlayer.Views;

public partial class MainWindow : Window
{
    Path maximizeIcon;
    ToolTip maximizeToolTip;

    public MainWindow()
    {
        InitializeComponent();

        var minimizeButton = this.FindControl<Button>("MinimizeButton");
        var maximizeButton = this.FindControl<Button>("MaximizeButton");
        maximizeIcon = this.FindControl<Path>("MaximizeIcon");
        maximizeToolTip = this.FindControl<ToolTip>("MaximizeToolTip");
        var closeButton = this.FindControl<Button>("CloseButton");

        minimizeButton.Click += MinimizeWindow;
        maximizeButton.Click += MaximizeWindow;
        closeButton.Click += CloseWindow;

        var bg = this.FindControl<Panel>("bg");
        bg.PointerPressed += MainWindow_PointerPressed;

        Application.Current.RequestedThemeVariant = ThemeVariant.Light;

        //不要在Linux上使用自定义标题栏，因为可能的选项太多了。
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) == true)
        {
            UseNativeTitleBar();
        }
        SubscribeToWindowState();

        Interactions.ShowNote.RegisterHandler(cxt => NotifyMessage(cxt));
        Interactions.ShowError.RegisterHandler(cxt => NotifyMessage(cxt, NotificationType.Error));

    }

    private void DarkTheme_IsCheckedChanged(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (darkThemeToggleButton.IsChecked == true)
            SetDarkTheme();
        else
            SetLightTheme();
    }
    private void SetLightTheme()
    {
        Cursor = new Cursor(StandardCursorType.Wait);
        Application.Current.RequestedThemeVariant = ThemeVariant.Light;
        Application.Current.Resources["MacOsTitleBarBackground"] = new SolidColorBrush { Color = new Color(255, 222, 225, 230) };
        Application.Current.Resources["MacOsWindowTitleColor"] = new SolidColorBrush { Color = new Color(255, 77, 77, 77) };
        Cursor = new Cursor(StandardCursorType.Arrow);
    }
    private void SetDarkTheme()
    {
        Cursor = new Cursor(StandardCursorType.Wait);
        Application.Current.RequestedThemeVariant = ThemeVariant.Dark;
        Cursor = new Cursor(StandardCursorType.Arrow);
    }
    private void UseNativeTitleBar()
    {
        ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.SystemChrome;
        ExtendClientAreaTitleBarHeightHint = -1;
        ExtendClientAreaToDecorationsHint = false;
    }
    private void CloseWindow(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Window hostWindow = (Window)this.VisualRoot;
        hostWindow.Close();
    }

    private void MaximizeWindow(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Window hostWindow = (Window)this.VisualRoot;

        if (hostWindow.WindowState == WindowState.Normal)
        {
            hostWindow.WindowState = WindowState.Maximized;
        }
        else
        {
            hostWindow.WindowState = WindowState.Normal;
        }
    }

    private void MinimizeWindow(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Window hostWindow = (Window)this.VisualRoot;
        hostWindow.WindowState = WindowState.Minimized;
    }

    private async void SubscribeToWindowState()
    {
        Window hostWindow = (Window)this.VisualRoot;

        while (hostWindow == null)
        {
            hostWindow = (Window)this.VisualRoot;
            await Task.Delay(50);
        }

        hostWindow.GetObservable(Window.WindowStateProperty).Subscribe(s =>
        {
            if (s != WindowState.Maximized)
            {
                maximizeIcon.Data = Geometry.Parse("M2048 2048v-2048h-2048v2048h2048zM1843 1843h-1638v-1638h1638v1638z");
                hostWindow.Padding = new Thickness(0, 0, 0, 0);
                maximizeToolTip.Content = "Maximize";
            }
            if (s == WindowState.Maximized)
            {
                maximizeIcon.Data = Geometry.Parse("M2048 1638h-410v410h-1638v-1638h410v-410h1638v1638zm-614-1024h-1229v1229h1229v-1229zm409-409h-1229v205h1024v1024h205v-1229z");
                hostWindow.Padding = new Thickness(7, 7, 7, 7);
                maximizeToolTip.Content = "Restore Down";

                // This should be a more universal approach in both cases, but I found it to be less reliable, when for example double-clicking the title bar.
                /*hostWindow.Padding = new Thickness(
                        hostWindow.OffScreenMargin.Left,
                        hostWindow.OffScreenMargin.Top,
                        hostWindow.OffScreenMargin.Right,
                        hostWindow.OffScreenMargin.Bottom);*/
            }
        });
    }


    private void MainWindow_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Pointer.Type == PointerType.Mouse)
        {
            this.BeginMoveDrag(e);
        }
    }


    WindowNotificationManager windowNotiManager;

    public void NotifyMessage(IInteractionContext<string, bool?> context, NotificationType notificationType = NotificationType.Information)
    {
        var title = "信息";
        switch (notificationType)
        {
            case NotificationType.Information:
                title = "信息";
                break;
            case NotificationType.Success:
                title = "成功";
                break;
            case NotificationType.Warning:
                title = "警告";
                break;
            case NotificationType.Error:
                title = "异常";
                break;
            default:
                break;
        }
        if (windowNotiManager == null)
            windowNotiManager = new WindowNotificationManager(TopLevel.GetTopLevel(this));
        windowNotiManager.Show(new Notification(title, context.Input, notificationType));
        context.SetOutput(true);
    }

}
