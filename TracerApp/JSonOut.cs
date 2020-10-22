using System;
using System.IO;

namespace TracerApp
{
    public class JSonOut : IOutPut
    {
        
        public void ConsoleOut(string JSontraceResult)
        {
            Console.WriteLine(JSontraceResult);
        }
        public void FileOut(string JSontraceResult, string fileName)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.WriteLine(JSontraceResult);
            }
        }
    }
}
