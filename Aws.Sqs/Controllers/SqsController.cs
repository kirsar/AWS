using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Sqs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqsController : ControllerBase
    {
        private readonly IAmazonSQS sqs;

        private const string QueueName = "queue1";

        public SqsController(IAmazonSQS sqs)
        {
            this.sqs = sqs;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var queueUrl = $"{sqs.Config.ServiceURL}/queue/{QueueName}";
            var res = await sqs.ReceiveMessageAsync(new ReceiveMessageRequest(queueUrl));

            Parallel.ForEach(res.Messages, message =>
                sqs.DeleteMessageAsync(new DeleteMessageRequest(queueUrl, message.ReceiptHandle)));

            return Ok(res.Messages.Select(message => new SqsMessage(message.Body)));
        }

        [HttpPost]
        public void Post([FromBody] SqsMessage message)
        {
            sqs.SendMessageAsync(new SendMessageRequest($"{sqs.Config.ServiceURL}/queue/{QueueName}", message.Text));
        }
    }
}
