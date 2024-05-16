using Avalonia.Labs.Controls;
using ReactiveUI;
using System;
using System.Reactive;
using static System.Net.Mime.MediaTypeNames;

namespace PeachPlayer.ViewModels
{
    public class SetUpViewModel : ViewModelBase
    {

        public ReactiveCommand<Unit, Unit> SaveConfigCommand { get; }

        private string sourceUrl;
        public string SourceUrl
        {
            get => sourceUrl;
            set => this.RaiseAndSetIfChanged(ref sourceUrl, value);
        }

        public SetUpViewModel()
        {
            IObservable<bool> isInputValid = this.WhenAnyValue(x => x.SourceUrl,
                x => !string.IsNullOrWhiteSpace(x) && x.ToLower().StartsWith("http"));

            SaveConfigCommand = ReactiveCommand.Create(SaveConfig, isInputValid);
        }


        public void SaveConfig()
        {
            var configStorage = ConfigStorage.Instance;
            configStorage.AppConfig.SourceUrl = this.SourceUrl;
            configStorage.Save();


        }


    }
}
