using System.Data.Entity;
using Meru_Web.Models;

namespace Meru_Web.DatabaseContext
{
    public class MeruContext:DbContext
    {
        public MeruContext():base("DefaultConnection")
        {
            //Database.SetInitializer<MeruContext>(new CreateDatabaseIfNotExists<MeruContext>());
            //Database.SetInitializer<MeruContext>(new DropCreateDatabaseIfModelChanges<MeruContext>());
            //Database.SetInitializer<MeruContext>(new DropCreateDatabaseAlways<MeruContext>());
            //Database.SetInitializer<MeruContext>(new SchoolDBInitializer());
        }

        public DbSet<RegisteredUser> User { get; set; }

        public DbSet<UserProfile> Profile { get; set; }

        public DbSet<UserHistory> History { get; set; }

        public DbSet<ClientKey> Key { get; set; }

        public DbSet<TokenManager> Token { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}