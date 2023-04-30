using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Configuration;

namespace AWS.S3.API.Application.Services
{
    public class S3Service
    {

        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public BasicAWSCredentials AwsCredentials { get; set; }
        private readonly IAmazonS3 _awsS3Client;

        public S3Service()
        {
            var appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            AccessKey = appsettings.GetConnectionString("AccessKey");
            SecretKey = appsettings.GetConnectionString("SecretKey");
            AwsCredentials = new BasicAWSCredentials(AccessKey, SecretKey);

            var s3config = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast2
            };

            _awsS3Client = new AmazonS3Client(credentials: AwsCredentials, s3config);

        }

        public async Task<bool> UploadFile(string bucket, string keyPath, IFormFile file)
        {
            using var newMemoryStream = new MemoryStream();
            file.CopyTo(newMemoryStream);

            var fileTransferUtility = new TransferUtility(_awsS3Client);

            await fileTransferUtility.UploadAsync(new TransferUtilityUploadRequest
            {
                BucketName = bucket,
                Key = keyPath,
                InputStream = newMemoryStream,
                ContentType = file.ContentType
            }); 


            return true;
        }
       

    }
}
