using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Peach.Application.Interfaces;
using Peach.Application.Services;
using PeachPlayer.Views;
using Splat;
using System;

namespace PeachPlayer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
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
        Locator.CurrentMutable.RegisterConstant(new RuleTestService(), typeof(IRuleTestService));
        Locator.CurrentMutable.RegisterConstant(new SourceService(), typeof(ISourceService));
        Locator.CurrentMutable.RegisterConstant(new SpiderService(), typeof(ISpiderService));
        //Locator.CurrentMutable.RegisterLazySingleton(() => new RuleTestService(), typeof(IRuleTestService));
        //Locator.Current.GetService<IRuleTestService>();

    }

}
