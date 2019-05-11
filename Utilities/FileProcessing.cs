using System;
namespace Utilities
{
    public static class FileProcessing
    {
        public static void PutFreqStatToFile(string filename, string datFile)
        {
            string command = string.Format("sox {0} -n stat -freq 2>&1 | ghead -n -17 &> {1}", filename, datFile);
            command.Bash();
        }
    }
}
