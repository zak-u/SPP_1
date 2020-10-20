using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TracerLibrary
{
    public class MethodInformation
    {
        public string name;
        public string className;
        public int executionTime;
        public Stopwatch stopwatch = new Stopwatch();
        public List<MethodInformation> childMethods = new List<MethodInformation>();
    }
}
