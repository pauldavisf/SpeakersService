using System;
using SpeakersService;
using SpeakersService.Models;
using System.Linq;

namespace SpeakersService.Helpers
{
    public static class DbHelper
    {
        public static bool FileExitstsInDatabase(string filename, ApplicationContext context)
        {
            return context.Files.AsEnumerable().Any(file => file.Name == filename);
        }
    }
}
