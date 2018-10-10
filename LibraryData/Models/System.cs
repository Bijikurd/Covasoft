using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class System
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Interval { get; set; }
        public int SystemStatus_id { get; set; }
    }
}
