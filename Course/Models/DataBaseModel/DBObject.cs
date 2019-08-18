using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Course.Models;
using Course.Models.DataBaseModel;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Models.DataBaseModel
{
    public class DBObject : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AccessTokens> AccessTokenses {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ForUsers;Username=postgres;Password=password");
        }
    }
}