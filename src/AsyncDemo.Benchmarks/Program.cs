using System;

namespace AsyncDemo.Benchmarks
{
    using System.Linq;
    using BenchmarkDotNet.Running;

    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<Benchmarks>();


            var doos = new MethodMethodMethodMethodMethod();

            doos.DoGetVideos(Enumerable.Range(0, 25)).Wait();
        }
    }
}