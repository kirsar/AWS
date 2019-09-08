using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aws
{
    public static class SnsUtil
    {
        public static Task<CreateTopicResponse> CreateSsnTopic(this IApplicationBuilder appBuilder, string topicName)
        {
            var sns = appBuilder.ApplicationServices.GetService<IAmazonSimpleNotificationService>();
            return sns.CreateTopicAsync(topicName);
        }

        public static Task<SubscribeResponse> SubscribeToSnsTopic(this IApplicationBuilder appBuilder, string topicArn)
        {
            var sns = appBuilder.ApplicationServices.GetService<IAmazonSimpleNotificationService>();
            return sns.SubscribeAsync(new SubscribeRequest(topicArn, "http", "http://localhost:64962/api/sns")); // TODO get base url from app
        }
    }
}
