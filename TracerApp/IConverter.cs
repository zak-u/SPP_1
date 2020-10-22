using System;
using TracerLibrary;

namespace TracerApp
{
    public interface IConverter
    {
        string Convert(TraceResult traceResult);
       
    }
}
