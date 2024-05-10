using Avalonia;
using Peach.Application.Interfaces;
using ReactiveUI;
using Splat;
using System.Reactive;
using System.Windows.Input;

namespace PeachPlayer.ViewModels;

public class MainWindowModel : ViewModelBase
{

    private ViewModelBase _contentViewModel;
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set { SetSelectView(value); this.RaiseAndSetIfChanged(ref _contentViewModel, value); }
    }

    private int selectView;
    public int SelectView
    {
        get => selectView;
        private set => this.RaiseAndSetIfChanged(ref selectView, value);
    }


    public ReactiveCommand<string, Unit> SwitchMenuCommand { get; }
    public ReactiveCommand<string, Unit> SearchCommand { get; }

    public ICommand SetUpCommand { get; }
   
    public MainWindowModel()
    {
        ContentViewModel = new FilmTelevisionViewModel();

        SwitchMenuCommand = ReactiveCommand.Create<string>(SwitchMenu);
        SetUpCommand = ReactiveCommand.Create(() => ContentViewModel = new SetUpViewModel());
        SearchCommand = ReactiveCommand.Create<string>(s => ContentViewModel = new SearchViewModel(s));

    }

    private void SetSelectView(ViewModelBase viewModel)
    {
        switch (viewModel.ToString().Replace("PeachPlayer.ViewModels.", ""))
        {
            case "FilmTelevisionViewModel":
                SelectView = 1;
                break;
            case "LiveBroadcastViewModel":
                SelectView = 2;
                break;
            case "NetdiskViewModel":
                SelectView = 3;
                break;
            case "PlayHistoryViewModel":
                SelectView = 4;
                break;
            default:
                SelectView = 0;
                break;
        }
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
