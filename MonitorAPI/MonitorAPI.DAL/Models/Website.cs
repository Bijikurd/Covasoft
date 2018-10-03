using System;
using System.ComponentModel.DataAnnotations;

namespace MonitorAPI.DAL.Models
{
    /// <summary>
    /// The Website class. Which is used as an object.
    /// </summary>
    public class Website
    {
        /// <summary>
        /// Contains different elements which make up a Website.
        /// </summary>

        /// <param>
        /// Make the <c>Id</c> the primary key.
        /// </param>
        [Key]
        /// <value>The Id of a service. <c>int</c></value>
        public int Id { get; set; }
        /// <value>The URL of a service. <c>string</c></value>
        public string Link { get; set; }
        /// <value>The Word which will be used to check the service. <c>string</c></value>
        public string Word { get; set; }
        /// <value>The Status of a service. <c>string</c></value>
        public bool Status { get; set; }
        /// <value>The TimeStamp of a service at the time of the check. <c>string</c></value>
        public string TimeStamp { get; set; }
    }
}
