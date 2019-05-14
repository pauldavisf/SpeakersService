﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeakersService.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public string Speech_FileName { get; set; } // уровни речи для каждой полосы (имя файла с этими данными)

        public string W_Path { get; set; } // словарь "вид шума строкой" -> массив разборчивостей для каждой полосы (путь к файлам с этими данными)
    }
}