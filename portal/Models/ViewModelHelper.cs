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
                AddressID = gameNight.AddressId,
                DateTime = gameNight.DateTime,
                MaxPlayers = gameNight.MaxPlayers,
                OrganizerId = gameNight.OrganiserId,
                Organizer = gameNight.Organiser

            };
            return result;
        }

        public static List<PlayersViewModel> ToViewModel(this IEnumerable<Players> es)
        {
            var result = new List<PlayersViewModel>();

            foreach (var e in es)
            {
                result.Add(e.ToViewModel());
            }
            return result;
        }

        public static PlayersViewModel ToViewModel(this Players e)
        {
            var result = new PlayersViewModel
            {
                PersonID = e.PersonId,
                GameNightID = e.GameNightId,
                Person = e.Person,
                GameNight = e.GameNight

            };

            return result;
        }
        public static List<GameListViewModel> ToViewModel(this IEnumerable<GameList> es)
        {
            var result = new List<GameListViewModel>();

            foreach (var e in es)
            {
                result.Add(e.ToViewModel());
            }
            return result;
        }

        public static GameListViewModel ToViewModel(this GameList e)
        {
            var result = new GameListViewModel
            {
                GameID = e.GameId,
                GameNightID = e.GameNightId,
                Game = e.Game,
                GameNight = e.GameNight

            };

            return result;
        }
    }
}

