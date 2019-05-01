using DataAccess.Local.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Local.Contexts
{
    public class AppDBContext: DbContext
    {
        private readonly string _connetionString = "Data Source=app_data.bat";

        public AppDBContext(DbContextOptions<AppDBContext> optionsBuilder)
        {
           
        }

        public AppDBContext(string connectionString = "Data Source=app_data.bat")
        {
            _connetionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_connetionString);
        }
       

        public DbSet<Music> Musics { get; set; }
        public DbSet<Video> Videos { get; set; }
    }
}
