﻿using System;
using Core.Domain;

namespace portal.Models
{
    public class PlayersViewModel
    {
        public Person Person { get; set; }
        public int PersonID { get; set; }

        public GameNight GameNight { get; set; }
        public int GameNightID { get; set; }
    }
}

