using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace TracerLibrary
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, List<MethodInformation>> threadsMethodList = new ConcurrentDictionary<int, List<MethodInformation>>();
        private List<MethodInformation> GetLastMethodNode(int threadID, int depthLevel)
        {
            List<MethodInformation> methodsList = threadsMethodList[threadID];
            for (int i = 0; i < depthLevel; i++)
            {
                if (methodsList.Count != 0)
                {
                    methodsList = methodsList[methodsList.Count - 1].childMethods;
                }
                else
                {
                    methodsList = methodsList[methodsList.Count].childMethods;
                }
            }
            return methodsList;
        }


        public void AddMethodNode(MethodInformation methodInformation, int threadID, int depthLevel)
        {
            List<MethodInformation> methodsList;

            //Проверяем, в словаре текуущий поток 
            if (threadsMethodList.ContainsKey(threadID))
            {
                //Если есть, то находим список методов, нужного уровня 
                methodsList = GetLastMethodNode(threadID, depthLevel);
            }
            else
            {
                methodsList = new List<MethodInformation>();
                threadsMethodList.TryAdd(threadID, methodsList);
            }

            methodsList.Add(methodInformation);
        }

        public void AddMethodExecutionTime(int depthLevel)
        {
            List<MethodInformation> methodsList = GetLastMethodNode(Thread.CurrentThread.ManagedThreadId, depthLevel);
            methodsList[methodsList.Count - 1].stopwatch.Stop();
            methodsList[methodsList.Count - 1].executionTime = (int)methodsList[methodsList.Count - 1].stopwatch.ElapsedMilliseconds;
            methodsList[methodsList.Count - 1].stopwatch = null;
        }
    }
}
