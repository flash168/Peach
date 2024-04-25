using Avalonia;
using ReactiveUI;
using System.Reactive;
using System.Windows.Input;

namespace PeachPlayer.ViewModels;

public class MainWindowModel : ViewModelBase
{

    private ViewModelBase _contentViewModel;
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }



    public ReactiveCommand<string, Unit> SwitchMenuCommand { get; }
    public ReactiveCommand<string, Unit> SearchCommand { get; }

    public ICommand SetUpCommand { get; }

    public MainWindowModel()
    {
        var main = new FilmTelevisionViewModel();
        _contentViewModel = main;


        SwitchMenuCommand = ReactiveCommand.Create<string>(SwitchMenu);
        SetUpCommand = ReactiveCommand.Create(() => ContentViewModel = new SetUpViewModel());
        SearchCommand = ReactiveCommand.Create<string>(s => ContentViewModel = new SearchViewModel(s));

    }


    public void SwitchMenu(string parameter)
    {
        switch (parameter)
        {
            case "FilmTelevisionViewModel":
                ContentViewModel = new FilmTelevisionViewModel();
                break;
            case "LiveBroadcastViewModel":
                ContentViewModel = new LiveBroadcastViewModel();
                break;
            case "NetdiskViewModel":
                ContentViewModel = new NetdiskViewModel();
                break;
            case "PlayHistoryViewModel":
                ContentViewModel = new PlayHistoryViewModel();
                break;
            default:
                break;
        }

    }

}
