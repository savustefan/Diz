
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace LucrareDisertatie.Repositories
{
    public class CloudImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account _account;


        public CloudImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _account = new Account (
                configuration.GetSection("Cloud")["Name"],
                configuration.GetSection("Cloud")["apiKey"],
                configuration.GetSection("Cloud")["apiSecret"]);
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var user = new Cloudinary(_account);
            

            var cloudinaryUpload = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var cloudinaryResult = await user.UploadAsync(cloudinaryUpload);

            if (cloudinaryResult != null && cloudinaryResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return cloudinaryResult.SecureUrl.ToString();

            }

            return null;
        }
    }
}
