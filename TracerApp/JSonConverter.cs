using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TracerLibrary;

namespace TracerApp
{
    public class JSonConverter : IConverter
    {
        private class ThreadInformationForJSon
        {
            public int threadID;
            public int threadExecutionTime;
            public List<MethodInformation> methodInformation;
        }

        private string fileName;
        private List<ThreadInformationForJSon> threadInformationListForJSon = new List<ThreadInformationForJSon>();

        public JSonConverter(string fileName)
        {
            this.fileName = fileName;
        }

        private int CountThreadExecutionTime(List<MethodInformation> methodInformation)
        {
            int executionTime = 0;
            foreach (var method in methodInformation)
            {
                executionTime += method.executionTime;
            }
            return executionTime;
        }

        private void PrepareThreadInformationListForJSon(TraceResult traceResult)
        {
            foreach (var threadsMethodList in traceResult.threadsMethodList)
            {
                ThreadInformationForJSon threadInformationForJSon = new ThreadInformationForJSon();
                threadInformationForJSon.threadID = threadsMethodList.Key;
                threadInformationForJSon.methodInformation = threadsMethodList.Value;
                threadInformationForJSon.threadExecutionTime = CountThreadExecutionTime(threadsMethodList.Value);
                threadInformationListForJSon.Add(threadInformationForJSon);
            }
        }

        public void Convert(TraceResult traceResult)
        {
            PrepareThreadInformationListForJSon(traceResult);

            string jsonString = JsonConvert.SerializeObject(threadInformationListForJSon, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.WriteLine(jsonString);
            }
            Console.WriteLine(jsonString);
        }

    }
}
