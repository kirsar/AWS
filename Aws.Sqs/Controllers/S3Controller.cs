using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aws.Controllers
{
    [Route("api/[controller]")]
    [Consumes("multipart/form-data", "text/csv")]
    [ApiController]
    public class S3Controller : Controller
    {
        private readonly IAmazonS3 s3;
        private const string BucketName = "bucket1";

        public S3Controller(IAmazonS3 s3)
        {
            this.s3 = s3;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm(Name = "file")]IFormFile file)
        {
            // TODO this will not work with localstack due to bucket name if url issue
            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = file.FileName,
                    BucketName = BucketName,
                    CannedACL = S3CannedACL.PublicRead
                };

                var fileTransferUtility = new TransferUtility(s3);
                await fileTransferUtility.UploadAsync(uploadRequest);
            }

            return Ok();
        }
    }
}
