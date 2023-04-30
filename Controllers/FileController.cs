using Amazon.S3;
using AWS.S3.API.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace AWS.S3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddFile([FromForm] Application.Domain.File file)
        {
            try
            {
                var awsS3 = new S3Service();
                var path = "imagens/" + Guid.NewGuid();
                var uploadFile = await awsS3.UploadFile("apis3learning", path, file.Path);

                return Ok(uploadFile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetFile(string bucket, string keyPath)
        {
            var s3config = new S3Service();
            var awsS3 = new AmazonS3Client(s3config.AwsCredentials, Amazon.RegionEndpoint.USEast2);
            var archive = await awsS3.GetObjectAsync(bucket, keyPath);

            return Ok(archive);
        }
      
    }
}
