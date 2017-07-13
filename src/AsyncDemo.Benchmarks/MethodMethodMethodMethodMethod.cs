namespace AsyncDemo.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class MethodMethodMethodMethodMethod
    {
        public async Task<string> Method()
        {
            return await MethodMethod();
        }

        public async Task<string> MethodMethod()
        {
            await Task.Delay(1).ConfigureAwait(false);

            return "Hello";
        }


        public class MasterVideo
        {

        }

        public int CurrentRequests;
        public int Counter;
        private readonly Random _rnd = new Random();

        public async Task<IEnumerable<MasterVideo>> SemaphoreSlimGetVideos(IEnumerable<int> ids)
        {
            var requestLimiter = new SemaphoreSlim(5, 5);

            //sweet local function (thanks c# 7.0)
            async Task<MasterVideo> Get(int id)
            {
                try
                {
                    await requestLimiter.WaitAsync().ConfigureAwait(false);

                    return await GetMasterVideo(id).ConfigureAwait(false);
                }
                finally
                {
                    requestLimiter.Release();
                }
            }

            return await Task.WhenAll(ids.Select(Get)).ConfigureAwait(false);
        }

        public async Task<MasterVideo> GetMasterVideo(int id)
        {
            Interlocked.Increment(ref CurrentRequests);
            Interlocked.Increment(ref Counter);

            Console.WriteLine(Counter + ": " + CurrentRequests);
            await Task.Delay(_rnd.Next(10, 1000)).ConfigureAwait(false);

            Interlocked.Decrement(ref CurrentRequests);

            return new MasterVideo();
        }

        public async Task<object> DontGetVideo(string trackingCode)
        {
            var homeVideo = await GetHomeVideo(trackingCode).ConfigureAwait(false);
            var onlineVideo = await GetOnlineVideo(trackingCode).ConfigureAwait(false);
            var masterVideo = await GetMasterVideo(trackingCode).ConfigureAwait(false);

            return new
            {
                homeVideo,
                onlineVideo,
                masterVideo
            };
        }

        public async Task<object> DoGetVideo(string trackingCode)
        {
            var homeVideoTask = GetHomeVideo(trackingCode);
            var onlineVideoTask = GetOnlineVideo(trackingCode);
            var masterVideoTask = GetMasterVideo(trackingCode);

            return new
            {
                homeVideo = await homeVideoTask.ConfigureAwait(false),
                onlineVideo = await onlineVideoTask.ConfigureAwait(false),
                masterVideo = await masterVideoTask.ConfigureAwait(false)
            };
        }



        public Task <string>GetHomeVideo(string trackingCode)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetOnlineVideo(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetMasterVideo(string id)
        {
            throw new NotImplementedException();
        }
    }
}
