using System;
using TracerLibrary;

namespace TracerApp
{
    public interface IConverter
    {
        void Convert(TraceResult traceResult);
    }
}
