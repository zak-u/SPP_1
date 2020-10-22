using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace TracerLibrary
{
    public class Tracer : ITracer
    {
        private TraceResult traceResult = new TraceResult();
        private ConcurrentDictionary<int, ThreadInformation> threadsInformation = new ConcurrentDictionary<int, ThreadInformation>();

        public void StartTrace()
        {
            ThreadInformation currentThreadInformation;
            int currentThreadID = Thread.CurrentThread.ManagedThreadId;

            //Добавляем поток в словь, если его нету
            threadsInformation.TryAdd(currentThreadID, new ThreadInformation());

            currentThreadInformation = threadsInformation[currentThreadID];

            //Перемещаемся в текущем потоке на 1 уровень
            currentThreadInformation.depthLevel++;

            //Добавляем измеряемый метод
            traceResult.AddMethodNode(GetMethodInformation(), currentThreadID, currentThreadInformation.depthLevel);

        }

        public void StopTrace()
        {
            ThreadInformation currentThreadInformation;
            int currentThreadID = Thread.CurrentThread.ManagedThreadId;
            currentThreadInformation = threadsInformation[currentThreadID];

            //Останавливаем таймер и записываем время метода
            traceResult.AddMethodExecutionTime(currentThreadInformation.depthLevel);

            //Поднимаемся на уровень 
            currentThreadInformation.depthLevel--;
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }

        //Получаем информацию об измеряемом методе из стека и запускаем таймер 
        private MethodInformation GetMethodInformation()
        {
            MethodInformation methodInformation = new MethodInformation();

            //Объект для трасировки стека вызова
            StackTrace stackTrace = new StackTrace();

            //Получаем измеряемый метод, GetFrame(2), т.к. по стеку это 3 вызванный метод 
            MethodBase currentMethod = stackTrace.GetFrame(2).GetMethod();

            methodInformation.name = currentMethod.Name;
            methodInformation.className = currentMethod.DeclaringType.Name;

            //Запускаем таймер метода
            methodInformation.stopwatch.Start();

            return methodInformation;
        }
    }
}
