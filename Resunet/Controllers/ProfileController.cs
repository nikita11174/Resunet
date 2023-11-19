using Microsoft.AspNetCore.Mvc;
using Resunet.ViewModels;
using System.Security.Cryptography;

namespace Resunet.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        public async Task<IActionResult> IndexSave()
        {
            //if(ModelState.IsValid())
            string filename = "";
            var image = Request.Form.Files[0];
            if (image != null)
            {
                MD5 md5hhash = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(image.FileName);
                byte[] hashBytes = md5hhash.ComputeHash(inputBytes);

                string hash = Convert.ToHexString(hashBytes);

                var dir = "./wwwroot/images/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                filename = dir + "/" + image.FileName;

                using (var stream = System.IO.File.Create(filename))
                    await image.CopyToAsync(stream);

            }
            return View("Index", new ProfileViewModel());
        }
    }
}
