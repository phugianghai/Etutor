using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Etutor.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string BodyText { get; set; }
        public int From { get; set; }
        public DateTime Time { get; set; }
        public virtual Assign Assign { get; set; }
    }
}