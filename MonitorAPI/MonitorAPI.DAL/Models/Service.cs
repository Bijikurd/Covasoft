using System;
using System.ComponentModel.DataAnnotations;

namespace MonitorAPI.DAL.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        public string Link { get; set; }

        public bool Status { get; set; }

        public string TimeStamp { get; set; }
    }
}
