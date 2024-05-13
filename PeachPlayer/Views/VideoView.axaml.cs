using Avalonia;
using Avalonia.Controls;
using PeachPlayer.ViewModels;

namespace PeachPlayer.Views;

public partial class VideoView : UserControl
{
    public VideoView()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<string> TypeIdProperty = AvaloniaProperty.Register<VideoView, string>(nameof(TypeId), defaultValue: "");

    public string TypeId
    {
        get => GetValue(TypeIdProperty);
        set => SetValue(TypeIdProperty, value);
    }


}