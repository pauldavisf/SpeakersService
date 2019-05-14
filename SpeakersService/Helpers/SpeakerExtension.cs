using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SpeakersService.Models;
using Utilities;

namespace SpeakersService.Helpers
{
    public static class SpeakerExtension
    {
        public static SpeakerViewModel ToViewModel(this Speaker speaker)
        {
            var speech = new List<double>();
            var w = new Dictionary<string, double[]>();

            var fileStream = new FileStream(speaker.Speech_FileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    speech.Add(double.Parse(line));
                }
            }

            foreach(var wFilename in DefaultParameters.DefaultNoisesDictionary.Keys)
            {
                fileStream = new FileStream(speaker.W_Path + "/" + wFilename, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    var listOfW = new List<double>();

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        listOfW.Add(double.Parse(line));
                    }

                    w[wFilename] = listOfW.ToArray();
                }
            }

            return new SpeakerViewModel { Name = speaker.Name, Path = speaker.Path, Speech = speech.ToArray(), W = w };
        }
    }
}
