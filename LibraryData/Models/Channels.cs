using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
   public class Channels
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SlackId { get; set; }
        public string DiscordId { get; set; }
    }
}
