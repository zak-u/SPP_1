using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TracerLibrary;

namespace TracerApp
{
    public class XmlConverter : IConverter
    {
        
        private XDocument CreateXDocument(TraceResult traceResult)
        {
            XDocument xDocument = new XDocument();
            XElement root = new XElement("root");
            xDocument.Add(root);
            root.Add(AddThreadInformation(traceResult));
            return xDocument;
        }

        private List<XElement> AddThreadInformation(TraceResult traceResult)
        {
            List<XElement> threadList = new List<XElement>();
            foreach (var threadID in traceResult.threadsMethodList.Keys)
            {
                XAttribute[] threadAttributes =
                {
                    new XAttribute("id", threadID),
                    new XAttribute("time", CountThreadExecutionTime(traceResult.threadsMethodList[threadID]))
                };
                XElement threadElement = new XElement("thread", threadAttributes);
                threadElement.Add(AddMethodInformation(traceResult.threadsMethodList[threadID]));
                threadList.Add(threadElement);
            }
            return threadList;
        }

        private List<XElement> AddMethodInformation(List<MethodInformation> methodInformation)
        {
            List<XElement> methodList = new List<XElement>();
            foreach (var method in methodInformation)
            {
                XAttribute[] methodAttributes =
                    {
                    new XAttribute("name", method.name),
                    new XAttribute("class", method.className),
                    new XAttribute("time", method.executionTime)
                    };
                XElement methodElement = new XElement("method", methodAttributes);
                methodList.Add(methodElement);
                if (method.childMethods.Count != 0)
                {
                    methodElement.Add(AddMethodInformation(method.childMethods));
                }

            }
            return methodList;
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

        public string Convert(TraceResult traceResult)
        {
            XDocument xDocument = CreateXDocument(traceResult);
            string xmlString =  xDocument.ToString();
            return xmlString;
            
        }
    }
}
