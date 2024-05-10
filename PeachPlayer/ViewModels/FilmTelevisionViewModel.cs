using Peach.Application.Interfaces;
using Peach.Model.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System.Collections.ObjectModel;
using System.Reactive;

namespace PeachPlayer.ViewModels;

public class FilmTelevisionViewModel : ViewModelBase
{

    //private ObservableCollection<SiteModel> listItems;
    //public ObservableCollection<SiteModel> ListItems
    //{
    //    get => listItems;
    //    private set => this.RaiseAndSetIfChanged(ref listItems, value);
    //}

    public ReactiveCommand<SiteModel, Unit> SwitchSiteCommand { get; }

    [Reactive]
    public ObservableCollection<SiteModel> ListItems { get; set; }

    private ISourceService source;
    public FilmTelevisionViewModel(ISourceService _source = null)
    {
        source = _source ?? Locator.Current.GetService<ISourceService>();
        SwitchSiteCommand = ReactiveCommand.Create<SiteModel>(SwitchSite);
        LoadSource();
    }

    private async void LoadSource()
    {
        string url = "http://drpy.nokia.press/config/2?ver=2";
        string url1 = "https://jihulab.com/yw88075/tvbox/-/raw/main/dr/js.json";
        //测试加载数据
        var req = await source.LoadConfig(url1);
        if (req)
            ListItems = new ObservableCollection<SiteModel>(source.Source.Sites);
    }

    public void SwitchSite(SiteModel site)
    {

    }


}
