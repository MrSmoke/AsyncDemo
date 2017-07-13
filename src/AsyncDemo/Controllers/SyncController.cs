namespace AsyncDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("sync")]
    public class SyncController : Controller
    {
        private readonly DataRepository _dataRepository;

        public SyncController(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var strings = _dataRepository.GetStrings();

            return Ok("ok");
        }

        [Route("seed")]
        public IActionResult Seed()
        {
            var strings = DemoHelper.GetStringList();

            _dataRepository.Seed(strings);

            return Ok();
        }

        [Route("deadlock")]
        public IActionResult DeadLock()
        {
            var strings = _dataRepository.GetStringsAsyncConfigureAwait(true).Result;

            return Ok(strings);
        }
    }
}
