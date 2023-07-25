using Jint.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
