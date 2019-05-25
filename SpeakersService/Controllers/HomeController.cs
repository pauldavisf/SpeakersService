using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SpeakersService.Models;
using static SpeakersService.Helpers.DbHelper;
using Utilities;
using System;
using System.Collections.Generic;
using static SpeakersService.Helpers.SpeakerExtension;
using Models;

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
            var modelsList = _context.Files.ToList();
            var viewModels = new List<SpeakerViewModel>();

            foreach(var model in modelsList)
            {
                viewModels.Add(model.ToViewModel());
            }

            return View("Upload", viewModels);
        }

        public void SaveSpeechToFile(double[] speech, string filename)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
            }

            using (StreamWriter outputFile = new StreamWriter(filename, false, System.Text.Encoding.Default))
            {
                foreach (var val in speech)
                    outputFile.WriteLine(val);
            }
        }

        public void SaveWToFiles(Dictionary<string, double[]> W, string path)
        {
            foreach (var pair in W)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (StreamWriter outputFile = new StreamWriter(path + "/" + pair.Key, false, System.Text.Encoding.Default))
                {
                    foreach (var val in pair.Value)
                        outputFile.WriteLine(val);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetNoiseLevels(double[] w, string[] selectedSpeakers)
        {
            var speakerList = new List<SpeakerViewModel>();
            foreach(var speakerName in selectedSpeakers)
            {
                var speaker = _context.Files.FirstOrDefault(x => x.Name == speakerName);
                speakerList.Add(speaker.ToViewModel());
            }

            var levels = Core.GetNoiseLevels(speakerList, -30, 30, w[0]);

            return View("Index", levels);
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
                string filename = _appEnvironment.WebRootPath + path;

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(filename, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                var speech = FileProcessing.GetSpeechLevelsFromFile(filename);
                var w = FileProcessing.GetIntelligibilityForAllNoises(speech, DefaultParameters.DefaultNoisesDictionary);

                var speechFile = _appEnvironment.WebRootPath + "/Files/" + Path.GetFileNameWithoutExtension(filename) + "/speech.dat";
                SaveSpeechToFile(speech, speechFile);

                var wPath = _appEnvironment.WebRootPath + "/Files/" + Path.GetFileNameWithoutExtension(filename) + "/W";
                SaveWToFiles(w, wPath);

                Speaker speaker = new Speaker { Name = uploadedFile.FileName, Speech_FileName = speechFile, Path = path, W_Path = wPath };

                await _context.Files.AddAsync(speaker);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Upload");
        }

        public IActionResult Upload()
        {
            var modelsList = _context.Files.ToList();
            var viewModels = new List<SpeakerViewModel>();

            foreach (var model in modelsList)
            {
                viewModels.Add(model.ToViewModel());
            }

            return View(viewModels);
        }
    }
}
