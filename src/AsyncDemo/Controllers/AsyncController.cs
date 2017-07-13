namespace AsyncDemo.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [Route("async")]
    public class AsyncController : Controller
    {
        private readonly DataRepository _dataRepository;

        public AsyncController(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var strings = await _dataRepository.GetStringsAsyncConfigureAwait(false).ConfigureAwait(false);

            return Ok("ok");
        }


        [Route("reps")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(int reps, bool continueOnCapturedContext)
        {
            var tasks = new Task[reps];
            for (var i = 0; i < reps; i++)
            {
                tasks[i] = _dataRepository.GetStringsAsyncConfigureAwait(continueOnCapturedContext);
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return Ok();
        }
    }
}