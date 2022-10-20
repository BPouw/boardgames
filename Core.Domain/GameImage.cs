using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class GameImage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Picture { get; set; }

        public string PictureFormat { get; set; }

        public Game Game { get; private set; }
        public int? GameID { get; set; }
    }
}