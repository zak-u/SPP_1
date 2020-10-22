using System;
namespace TracerApp
{
    public interface IOutPut
    {
        void ConsoleOut(string traceResultForConsole);
        void FileOut(string traceResultForConsole, string fileName);
    }
}
