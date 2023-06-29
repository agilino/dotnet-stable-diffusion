using System;
using backend_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        #region DbSet
        // Add your DB here
        // TODO: move to readme
        // Add-migarion {migration name}
        // Remove-migration
        // update-database

        public DbSet<Image> Images { get; set; }

        #endregion
    }
}

