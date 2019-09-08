using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aws
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseMvc();

            var topicArn = (await app.CreateSsnTopic("topic1")).TopicArn;
            await app.SubscribeToSnsTopic(topicArn);
        }
    }
}
