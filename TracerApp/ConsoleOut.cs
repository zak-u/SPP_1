using System;
namespace TracerApp
{
    public class ConsoleOut : IOutPut
    {
        public void Out(string stringResult)
        {
            Console.WriteLine(stringResult);
        }
    }
}