using NUnit.Framework;
using System;
using System.Threading;
using TracerLibrary;
namespace TestsForTracer
{
    [TestFixture()]
    public class TracerTest
    {
        public Tracer Setup()
        {
            Tracer tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(70);
            tracer.StopTrace();
            return tracer;
        }

        [Test()]
        public void TestMethodCheckMethodName()
        {
            Tracer tracer = Setup();
            Assert.AreEqual("Setup", tracer.GetTraceResult().threadsMethodList[Thread.CurrentThread.ManagedThreadId][0].name);
        }

        [Test()]
        public void TestMethodCheckMethodExecutionTime()
        {
            Tracer tracer = Setup();
            Assert.AreEqual(70, tracer.GetTraceResult().threadsMethodList[Thread.CurrentThread.ManagedThreadId][0].executionTime, 2);
        }

        [Test()]
        public void TestMethodCheckThreadID()
        {
            Tracer tracer = Setup();
            Assert.IsTrue(tracer.GetTraceResult().threadsMethodList.ContainsKey(Thread.CurrentThread.ManagedThreadId));
        }

        [Test()]
        public void TestMethodClassName()
        {
            Tracer tracer = Setup();
            Assert.AreEqual("TracerTest", tracer.GetTraceResult().threadsMethodList[Thread.CurrentThread.ManagedThreadId][0].className);
        }

        [Test()]
        public void TestMethodCheckDepthLevel()
        {
            Tracer tracer = new Tracer();
            tracer.StartTrace();
            ExampleDepthLevel();
            tracer.StopTrace();

            Assert.IsNotNull(tracer.GetTraceResult().threadsMethodList[Thread.CurrentThread.ManagedThreadId][0].childMethods);
        }

        public void ExampleDepthLevel()
        {
            Tracer tracer = Setup();
        }
    }
}
