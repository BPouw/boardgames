using System;
using Core.Domain;
namespace Webservice.Dtos
{
    public static class DtoHelper
    {
        public static GameNightDto ToDTO(this GameNight GameNight)
        {
            return new GameNightDto()
            {
                name = GameNight.Name,
                game_time = GameNight.DateTime,
                address = GameNight.Address,
                organiser = ToDTO(GameNight.Organiser),
                max_players = GameNight.MaxPlayers,
                vegan = GameNight.Vegan,
                lactose_intolerant = GameNight.LactoseIntolerant,
                nut_allergy = GameNight.NutAllergy,
                alcohol_free = GameNight.AlcoholFree
            };
        }

        public static PersonDto ToDTO(this Person Person)
        {
            return new PersonDto() {name = Person.Name };
        }

        public static WarningDto ToDTO(this String warning)
        {
            return new WarningDto() { warning = warning };
        }

        public static List<WarningDto> ToDTO(this List<String> Warnings)
        {
            var result = new List<WarningDto>();
            foreach (string warning in Warnings)
            {
                result.Add(warning.ToDTO());
            }
            return result;
        }
    }
}
