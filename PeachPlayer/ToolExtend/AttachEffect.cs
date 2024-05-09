using Avalonia.Controls;
using Avalonia;


namespace PeachPlayer.ToolExtend
{
    public class AttachEffect
    {

        public static readonly AttachedProperty<string> IconProperty =AvaloniaProperty.RegisterAttached<AttachEffect, RadioButton, string>("Icon");

        public static string GetColumn(RadioButton element)
        {
            return element.GetValue(IconProperty);
        }
        public static void SetColumn(RadioButton element, string value)
        {
            element.SetValue(IconProperty, value);
        }


    }

}
