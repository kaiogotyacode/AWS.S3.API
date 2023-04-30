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
        public async Task<ActionResult> AddFile([FromForm] Application.Domain.File file)
        {
            try
            {
                var awsS3 = new S3Service();
                var key = "imagens/" + Guid.NewGuid();
                var uploadFile = await awsS3.UploadFile("apis3learning", key, file.Path);

                return Ok(uploadFile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
