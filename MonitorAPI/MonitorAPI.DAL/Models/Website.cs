using System.ComponentModel.DataAnnotations;

namespace MonitorAPI.DAL.Models
{
    public class Website
    {
        [Key]
        public int Id { get; set; }

        public string Link { get; set; }

        public string Word { get; set; }

        public bool Status { get; set; }

    }
}
