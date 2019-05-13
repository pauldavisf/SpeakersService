using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SpeakersService.Models;
using static SpeakersService.Helpers.DbHelper;

namespace SpeakersService.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _context;
        IHostingEnvironment _appEnvironment;

        public HomeController(ApplicationContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View(_context.Files.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var fileFromDb = _context.Files.FirstOrDefault(f => f.Name == uploadedFile.FileName);
                if (fileFromDb != null)
                {
                    _context.Files.Remove(fileFromDb);
                    await _context.SaveChangesAsync();
                }

                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };

                await _context.Files.AddAsync(file);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}