using System;
using System.IO;

namespace TracerApp
{
    public class XmlOut : IOutPut
    {
        public void ConsoleOut(string XmltraceResult)
        {
            Console.WriteLine(XmltraceResult);
        }
        public void FileOut(string XmltraceResult, string fileName)
        { 
        using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.WriteLine(XmltraceResult);
            }
        }
        
    }
}
