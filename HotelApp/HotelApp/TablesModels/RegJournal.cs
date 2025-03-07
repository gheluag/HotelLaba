using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.TablesModels
{
    public class RegJournal
    {
        public int Id { get; set; }
        public TimeOnly RegTime { get; set; }
        public DateOnly Entry { get; set; }
        public DateOnly Departure { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoomNum { get; set; }
    }
}
