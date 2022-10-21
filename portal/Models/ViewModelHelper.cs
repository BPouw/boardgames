using System;
using Core.Domain;

namespace portal.Models
{
    public static class ViewModelHelpers
    {
        public static List<GamenightViewModel> ToViewModel(this IEnumerable<GameNight> gameNights)
        {
            var result = new List<GamenightViewModel>();

            foreach (var gameNight in gameNights)
            {
                result.Add(gameNight.ToViewModel());
            }

            return result;
        }

        public static GamenightViewModel ToViewModel(this GameNight gameNight)
        {
            var result = new GamenightViewModel
            {
                Id = gameNight.Id,
                Name = gameNight.Name,
                AddressId = gameNight.AddressId,
                DateTime = gameNight.DateTime,
                MaxPlayers = gameNight.MaxPlayers,
                OrganiserId = gameNight.OrganiserId,
                Organiser = gameNight.Organiser,
                AdultsOnly = gameNight.AdultsOnly,
                Address = gameNight.Address,
                Players = gameNight.Players,
                Games = gameNight.Games,
                Vegan = gameNight.Vegan,
                 LactoseIntolerant = gameNight.LactoseIntolerant,
                NutAllergy = gameNight.NutAllergy,
                  AlcoholFree = gameNight.AlcoholFree

    };
            return result;
        }

        public static List<GameNightPlayerViewModel> ToViewModel(this IEnumerable<GameNightPlayer> es)
        {
            var result = new List<GameNightPlayerViewModel>();

            foreach (var e in es)
            {
                result.Add(e.ToViewModel());
            }
            return result;
        }

        public static GameNightPlayerViewModel ToViewModel(this GameNightPlayer e)
        {
            var result = new GameNightPlayerViewModel
            {
                PersonID = e.PersonId,
                GameNightID = e.GameNightId,
                Person = e.Person,
                GameNight = e.GameNight

            };

            return result;
        }
        public static List<GameNightGameViewModel> ToViewModel(this IEnumerable<GameNightGame> es)
        {
            var result = new List<GameNightGameViewModel>();

            foreach (var e in es)
            {
                result.Add(e.ToViewModel());
            }
            return result;
        }

        public static GameNightGameViewModel ToViewModel(this GameNightGame e)
        {
            var result = new GameNightGameViewModel
            {
                GameID = e.GameId,
                GameNightId = e.GameNightId,
                Game = e.Game,
                GameNight = e.GameNight

            };

            return result;
        }

        public static GameViewModel ToViewModel(this Game e)
        {
            var result = new GameViewModel
            {
                Name = e.Name,
                genre = e.genre,
                category = e.category,
                Description = e.Description,
                AdultsOnly = e.AdultsOnly,
                GameImage = e.GameImage,
                Picture = Convert.ToBase64String(e.GameImage.Picture),
                PictureFormat = e.GameImage.PictureFormat
            };

            return result;
        }

    }
}

