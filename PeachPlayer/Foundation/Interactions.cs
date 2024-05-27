using ReactiveUI;
namespace PeachPlayer.Foundation
{
    public class Interactions
    {

        public static Interaction<string, bool?> ShowNote { get; } = new();
        public static Interaction<string, bool?> ShowError { get; } = new();

    }
}
