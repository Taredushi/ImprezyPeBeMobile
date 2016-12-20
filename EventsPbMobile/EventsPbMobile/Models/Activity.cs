﻿using SQLite.Net.Attributes;
using System;

namespace EventsPbMobile.Models
{
    public class Activity
    {
        [PrimaryKey, AutoIncrement]
        public int ActivityId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        public bool Gameable { get; set; }

        public Place Place { get; set; }
    }
}