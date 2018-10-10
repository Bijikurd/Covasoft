using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class SystemStatus
    {
        public int Id { get; set; }
        public int SystemId { get; set; }
        public string StatusCode { get; set; }
        public string Address { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
