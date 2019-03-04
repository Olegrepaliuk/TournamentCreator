using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentCreator.Models
{
    [Table("Teams")]
    public class Team
    {
        [Key]
        public string TName { get; set; }
        public string Country { get; set; }
        public string Stadium { get; set; }
        //branch testing
    }
}