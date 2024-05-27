using ReactiveUI;
namespace PeachPlayer.Foundation
{
    public class Interactions
    {

        public static Interaction<string, bool?> ShowNote { get; } = new();

    }
}
