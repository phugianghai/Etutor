using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Etutor.Models
{
    public class Tutor
    {
        [ForeignKey("Account")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Faculty { get; set; }
        public virtual Account Account { get; set; }
        public ICollection<Assign> Assign { get; set; }
    }
}