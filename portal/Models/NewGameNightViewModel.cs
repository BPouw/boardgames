using System;
using System.ComponentModel.DataAnnotations;
using Core.Domain;
using Core.DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace portal.Models
{
    public class NewGameNightViewModel 
    {

        public string Name { get; set; }

        public DateTime GameTime { get; set; }

        public int MaxPlayers { get; set; }

        public bool AdultsOnly { get; set; }

        public int AddressId { get; set; }

        public int OrganiserId { get; set; }

        public int[] GameIds { get; set; }
    }
}

