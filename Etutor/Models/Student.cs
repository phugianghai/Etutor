using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Etutor.Models
{
    public class Student
    {
        [ForeignKey("Account")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
        public virtual Account Account { get; set; }
        public virtual Assign Assign { get; set; }
    }
}