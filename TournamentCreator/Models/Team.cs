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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        public Guid Id { get; private set; }

        [Required]
        public string TName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        public string Stadium { get; set; }

        public Team()
        {
            Id = Guid.NewGuid();
        }
        //branch testing
    }
}