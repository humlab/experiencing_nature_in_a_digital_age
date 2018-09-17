using ENIDABackendAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENIDABackendAPI.db
{
    public class ENIDADbContext : DbContext
    {
        public ENIDADbContext(DbContextOptions<ENIDADbContext> options) : base(options) { }

        public DbSet<Image> Images { get; set; }
        public DbSet<Information> Information { get; set; }
    }
}
