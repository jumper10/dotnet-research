using Data.Local.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Local.Contexts
{
    public class AppDBContext: DbContext
    {
        private readonly string _connetionString= null;

        public AppDBContext(string connectionString)
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
