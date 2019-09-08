using System.Collections.Generic;
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

        private List<string> messages = new List<string>();

        public SnsController(IAmazonSimpleNotificationService sns)
        {
            this.sns = sns;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(messages);
        }

        // same method will be used by AWS to post messages onto end point
        // doesn't work now, due need to setup docker to access localhost from aws.localstack
        [HttpPost]
        public void Post(string value = "")
        {
            // TODO get topic here
            sns.PublishAsync(new PublishRequest(string.Empty, "notification"));

            // TODO handle AWS notifications and update mesages
            // https://stackoverflow.com/questions/15079829/example-sns-subscription-confirmation-using-aws-net-sdk
        }
    }
}
