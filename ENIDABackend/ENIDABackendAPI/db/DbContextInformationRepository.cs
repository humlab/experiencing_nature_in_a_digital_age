using ENIDABackendAPI.Model;
using ENIDABackendAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ENIDABackendAPI.db
{
    public class DbContextInformationRepository : InformationRepository
    {
        private readonly ENIDADbContext dbContext;

        public DbContextInformationRepository(ENIDADbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Information> GetInformation()
        {
            return dbContext.Information
                .Include(info => info.Image);
        }

        public IQueryable<Information> GetInformationByImageIdOrderedByOffset(string imageId)
        {
            return GetInformation()
                .Where(info => info.Image.Id == imageId)
                .OrderBy(info => info.YOffset);
        }
    }
}
