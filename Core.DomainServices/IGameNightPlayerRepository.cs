﻿using System;
using Core.Domain;

namespace Core.DomainServices
{
    public interface IGameNightPlayerRepository
    {
        Task AddPlayer(GameNightPlayer player);
        GameNightPlayer getGameNightPlayer(GameNightPlayer player);
    }
}

