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
    [Reactive]
    public ObservableCollection<ClassModel> Types { get; set; }
    [Reactive]
    public ObservableCollection<SmallVodModel> VodList { get; set; }

    public ReactiveCommand<SiteModel, Unit> SwitchSiteCommand { get; }

    [Reactive]
    public ObservableCollection<SiteModel> ListItems { get; set; }

    private ISourceService source;
    private IVodInfoService vod;
    public FilmTelevisionViewModel(ISourceService _source = null)
    {
        source = _source ?? Locator.Current.GetService<ISourceService>();
        vod = Locator.Current.GetService<IVodInfoService>();
        SwitchSiteCommand = ReactiveCommand.Create<SiteModel>(SwitchSite);
        LoadSource();
    }

    private async void LoadSource()
    {
        string url = "http://drpy.nokia.press/config/2?ver=2";
        string url1 = "https://jihulab.com/yw88075/tvbox/-/raw/main/dr/js.json";
        //测试加载数据
        var req = await source.LoadConfig(url);
        if (req)
            ListItems = new ObservableCollection<SiteModel>(source.Source.Sites);
    }

    public async void SwitchSite(SiteModel site)
    {
        await vod.InitSite(site);
        var filter = await vod.HomeAsync();
        Types = new ObservableCollection<ClassModel>(filter.Class);
        //var vods = await vod.HomeVodAsync("");
        //VodList = new ObservableCollection<SmallVodModel>(vods.List);
    }


}
