using Etutor.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Etutor.DAL
{
    public class EtutorContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Assign> Assigns { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
    }
}