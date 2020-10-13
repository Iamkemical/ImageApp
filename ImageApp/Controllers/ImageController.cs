using System.IO;
using System.Threading.Tasks;
using ImageApp.DataAccess;
using ImageApp.Models;
using ImageApp.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ImageApp.Controllers
{
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImageController(ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ImageServerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

             _dbContext.ImageServer.Add(model);
            await _dbContext.SaveChangesAsync();

            //Work on the image saving section
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var imageIdFromDb = await _dbContext.ImageServer.FindAsync(model.Id);

            if(files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, imageIdFromDb.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                imageIdFromDb.Image = @"\images\" + imageIdFromDb.Id + extension;
            }
            else
            {
                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + imageIdFromDb.Id + ".png");
                imageIdFromDb.Image = @"\images\" + imageIdFromDb.Id + ".png";
            }
            await _dbContext.SaveChangesAsync();
            return View();
        }

        public IActionResult Database()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Database(ImageDatabaseModel model)
        {
            if (ModelState.IsValid)
            { 
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using(var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    model.Picture = p1;
                }
                _dbContext.ImageDatabase.Add(model);
                await _dbContext.SaveChangesAsync();
                return View();
            }
            return View(model);
        }
    }
}