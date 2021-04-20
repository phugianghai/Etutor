using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Etutor.Models
{
    public class Assign
    {
        [ForeignKey("Student")]
        public int Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual Staff Staff { get; set; }
        public ICollection<Document> Document { get; set; }
        public ICollection<Record> Record { get; set; }
        public ICollection<Message> Message { get; set; }


    }
}