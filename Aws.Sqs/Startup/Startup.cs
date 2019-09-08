using Amazon.S3;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aws.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSQS>(Configuration.GetAWSOptions("sqs"));
            services.AddAWSService<IAmazonSimpleNotificationService>(Configuration.GetAWSOptions("sns"));
            services.AddAWSService<IAmazonS3>(Configuration.GetAWSOptions("sns"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseMvc();

            app.CreateSqsQueue("queue1");
            app.CreateAndSubsribeSsnTopic("topic1");
            app.CreateS3Bucket("bucket1");
        }
        #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }
}
