using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Etutor.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public virtual Student Student { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Tutor Tutor { get; set; }

    }
}