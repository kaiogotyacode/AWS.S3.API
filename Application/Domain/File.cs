namespace AWS.S3.API.Application.Domain
{
    public class File
    {
        public string Title { get; set; }
        public IFormFile Path { get; set; }
        public File()
        {

        }
        public File(string title, IFormFile path)
        {
            Title = title;
            Path = path;    
        }
    }
}
