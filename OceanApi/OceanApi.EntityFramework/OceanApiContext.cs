using Microsoft.EntityFrameworkCore;
using OceanApi.EntityFramework.Entities;
using System;

namespace OceanApi.EntityFramework
{
    public class OceanApiContext: DbContext
    {
        public DbSet<Address> Address { get; set; }

        public OceanApiContext(DbContextOptions<OceanApiContext> options): base(options)
        {
            //
        }
    }
}
