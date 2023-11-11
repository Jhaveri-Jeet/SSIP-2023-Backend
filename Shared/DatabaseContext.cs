using CriminalDatabaseBackend.Features.Acts;
using CriminalDatabaseBackend.Features.Advocates;
using CriminalDatabaseBackend.Features.Cases;
using CriminalDatabaseBackend.Features.CaseType;
using CriminalDatabaseBackend.Features.Courts;
using CriminalDatabaseBackend.Features.Districts;
using CriminalDatabaseBackend.Features.Hearing;
using CriminalDatabaseBackend.Features.Roles;
using CriminalDatabaseBackend.Features.Sections;
using CriminalDatabaseBackend.Features.States;
using CriminalDatabaseBackend.Features.Users;
using CriminalDatabaseBackend.Features.Witness;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace CriminalDatabaseBackend.Shared
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<CaseType> CaseTypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Advocates> Advocates { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<Acts> Acts { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<Courts> Courts { get; set; }
        public virtual DbSet<Cases> Cases { get; set; }
        public virtual DbSet<Hearing> Hearing { get; set; }
        public virtual DbSet<Witness> Witness { get; set; }

    }
}
