using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aws.Startup
{
    public static class S3Util
    {
        public static Task<PutBucketResponse> CreateS3Bucket(this IApplicationBuilder appBuilder, string bucketName)
        {
            var s3 = appBuilder.ApplicationServices.GetService<IAmazonS3>();
            // An issue with creation of bucket for localstack
            // https://github.com/localstack/localstack/issues/43
            // use aws cli now:  aws --endpoint-url=http://localhost:4572 s3api create-bucket --bucket=bucket1
            // return s3.PutBucketAsync(bucketName);
            return new Task<PutBucketResponse>(() => new PutBucketResponse());
        }
    }
}
