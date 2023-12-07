using Application.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Yelload.WebAPI.Controllers
{
    public class Uploadsuccess
    {
        public int Uploaded { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
    }
    public class UploadCkController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private CancellationToken cancellationToken;

        public UploadCkController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload)
        {
            if (upload.Length <= 0) return null;

            string newImageName = upload.GetRandomImagePath("blog");
            await _env.SaveAsync(upload, newImageName, cancellationToken);

            var url = $"{"/Images/"}{newImageName}";

            var success = new Uploadsuccess
            {
                Uploaded = 1,
                FileName = newImageName,
                Url = url
            };

            return new JsonResult(success);
        }
    }
}
