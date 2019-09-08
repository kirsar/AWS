using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aws.Startup
{
    public static class SnsUtil
    {
        public static async Task<SubscribeResponse> CreateAndSubsribeSsnTopic(this IApplicationBuilder appBuilder, string topicName)
        {
            var sns = appBuilder.ApplicationServices.GetService<IAmazonSimpleNotificationService>();
            var topic = await sns.CreateTopicAsync(topicName);
            
            // TODO get base url from app
            return await sns.SubscribeAsync(new SubscribeRequest(topic.TopicArn, "http", "http://localhost:64962/api/sns"));
        }
    }
}
