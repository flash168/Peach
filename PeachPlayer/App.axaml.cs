using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Avalonia.Markup.Xaml;
using Peach.Application.Interfaces;
using Peach.Application.Services;
using PeachPlayer.Views;
using ReactiveUI;
using Splat;
using System;
using System.Threading.Tasks;

namespace PeachPlayer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
    }

    private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ConfigureServiceProvider();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                // DataContext = new MainViewModel()
            };
            Locator.CurrentMutable.RegisterConstant(new NotificationManager(desktop.MainWindow));
        }
        //else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        //{
        //    singleViewPlatform.MainView = new MainView
        //    {
        //        DataContext = new MainViewModel()
        //    };
        //}
        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServiceProvider()
    {
        Locator.CurrentMutable.RegisterConstant(new VodInfoService(), typeof(IVodInfoService));
        Locator.CurrentMutable.RegisterConstant(new RuleTestService(), typeof(IRuleTestService));
        Locator.CurrentMutable.RegisterConstant(new SourceService(), typeof(ISourceService));
        Locator.CurrentMutable.RegisterConstant(new SpiderService(), typeof(ISpiderService));

        //Locator.CurrentMutable.RegisterLazySingleton(() => new RuleTestService(), typeof(IRuleTestService));
        //Locator.Current.GetService<IRuleTestService>();

    }



}
