using System.Diagnostics;

namespace Peach.Drpy
{
    public class console
    {
        public event Action<string> WriterLog;
        public void log(string mgs)
        {
            Debug.WriteLine(mgs);
            Console.WriteLine(mgs);
            WriterLog?.Invoke(mgs);
        }

    }
}
