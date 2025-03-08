using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp_net8.Tables
{
    public class Roomers
    {
        public int IdRoomer { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Passport { get; set; }
        public string Phone { get; set; }
        public int RoomNum { get; set; }

        public string FullName => $"{Surname} {FirstName}";
    }
}
