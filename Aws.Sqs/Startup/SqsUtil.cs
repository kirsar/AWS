using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aws.Startup
{
    public static class SqsUtil
    {
        public static Task<CreateQueueResponse> CreateSqsQueue(this IApplicationBuilder appBuilder, string queueName)
        {
            var sqs = appBuilder.ApplicationServices.GetService<IAmazonSQS>();
            return sqs.CreateQueueAsync(queueName);
        }
    }
}
