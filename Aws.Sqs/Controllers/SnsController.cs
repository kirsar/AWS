using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnsController : Controller
    {
        private readonly IAmazonSimpleNotificationService sns;

        private const string Topic = "topic1";

        public SnsController(IAmazonSimpleNotificationService sns)
        {
            this.sns = sns;
        }

        protected override void Dispose(bool disposing)
        {
            sns.UnsubscribeAsync(Topic);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await sns.SubscribeAsync(new SubscribeRequest(Topic, "http", sns.Config.ServiceURL));
            // sns.ConfirmSubscriptionAsync(Topic, res.SubscriptionArn)

            return Ok(string.Empty);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            sns.PublishAsync(new PublishRequest(Topic, "notification"));
        }
    }
}
