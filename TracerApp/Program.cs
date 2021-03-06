﻿using System;
using System.Collections.Generic;
using System.Threading;
using TracerLibrary;

namespace TracerApp
{
    class MainClass
    {
        static Tracer tracer = new Tracer();
        public static ClassForExample classExample = new ClassForExample(tracer);
        public static void Main(string[] args)
        {
            //Создаем лист потоков
            List<Thread> threads = new List<Thread>()
             {
                new Thread(ExampleMethod1),
                new Thread(classExample.InnerClassMethod ),
                new Thread(ExampleMethod4),
             };

            foreach (var thread in threads)
            {
                thread.Start();
            }

            ExampleMethod5();

            foreach (var thread in threads)
            {
                thread.Join();
            }
            //Вывод результатов
            GetConverting();
        }

        static void GetConverting()
        {
            string xmlFileName = "/Users/admin/Projects/TracerApp/Results/3.xml";
            string jsonFileName = "/Users/admin/Projects/TracerApp/Results/3.json";

            IConverter iConverter;
            IOutPut iOut;
            string stringResult;

            iConverter = new XmlConverter();
            stringResult = iConverter.Convert(tracer.GetTraceResult());

            iOut = new ConsoleOut();
            iOut.Out(stringResult);
            
            iOut = new FileOut(xmlFileName);
            iOut.Out(stringResult);

            Console.WriteLine();
           
            iConverter = new JSonConverter();
            stringResult = iConverter.Convert(tracer.GetTraceResult());

            iOut = new ConsoleOut();
            iOut.Out(stringResult);

            iOut = new FileOut(jsonFileName);
            iOut.Out(stringResult);
        }



        public class ClassForExample
        {
            private Tracer _tracer;

            internal ClassForExample(Tracer tracer)
            {
                _tracer = tracer;
            }

            public void InnerClassMethod()
            {
                _tracer.StartTrace();
                Thread.Sleep(7);
                _tracer.StopTrace();
            }
        }

        private static void ExampleMethod1()
        {
            tracer.StartTrace();
            ExampleMethod11();
            tracer.StopTrace();
        }

        private static void ExampleMethod11()
        {
            tracer.StartTrace();
            Thread.Sleep(7);
            tracer.StopTrace();
        }

        private static void ExampleMethod3()
        {
            tracer.StartTrace();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(12);
            }
            tracer.StopTrace();
        }

        private static void ExampleMethod42()
        {
            tracer.StartTrace();
            Thread.Sleep(110);
            tracer.StopTrace();
        }

        private static void ExampleMethod4()
        {

            ExampleMethod11();
            ExampleMethod42();

        }

        private static void ExampleMethod5()
        {
            tracer.StartTrace();
            Thread.Sleep(20);
            tracer.StopTrace();
        }
    

    }
}
