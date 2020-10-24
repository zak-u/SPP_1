using System;
using System.IO;

namespace TracerApp
{
    public class FileOut : IOutPut
    {
        private string fileName;
        public FileOut(string File)
        {
            this.fileName = File;
        }
        public void Out(string stringResult)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.WriteLine(stringResult);
            }
        }
    }
}
