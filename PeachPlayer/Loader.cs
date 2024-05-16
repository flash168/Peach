using System;
using System.IO;

namespace PeachPlayer
{
    public class Loader
    {
        internal static string WorkFolder
        {
            get { return workFolder; }
        }

        private static string workFolder = AppDomain.CurrentDomain.BaseDirectory;

        internal static string LibPath
        {
            get { return Path.Combine(WorkFolder, "lib"); }
        }
        internal static string LocalAddData
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PeachPlayer"); }
        }

    }
}
