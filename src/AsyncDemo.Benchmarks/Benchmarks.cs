namespace AsyncDemo.Benchmarks
{
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;

    public class Benchmarks
    {
        [Benchmark]
        public async Task ConfigureAwaitTrue()
        {

        }

        [Benchmark]
        public async Task ConfigureAwaitFalse()
        {

        }

        private async Task ConfigureAwait(bool continueOnCapturedContext)
        {
            await Task.Delay(1).ConfigureAwait(continueOnCapturedContext);
        }
    }
}
