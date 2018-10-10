using System;
using System.ComponentModel.DataAnnotations;

namespace MonitorAPI.DAL.Models
{
    /// <summary>
    /// The Service class. Which is used as an object.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Contains different elements which make up a service.
        /// </summary>

        /// <param>
        /// Make the <c>Id</c> the primary key.
        /// </param>
        [Key]
        /// <value>The Id of a service. <c>int</c></value>
        public int Id { get; set; }
        /// <value>The URL of a service. <c>string</c></value>
        public string Link { get; set; }
        /// <value>The Status of a service. <c>bool</c></value>
        public bool Status { get; set; }
        /// <value>The Timestamp of a service. <c>bool</c></value>
        public string TimeStamp { get; set; }
    }
}
