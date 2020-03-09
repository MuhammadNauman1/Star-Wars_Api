using ReactStartApi.DataAccess.Models;

namespace ReactStartApi.DataAccess
{
    using System.Data.Entity;

    public partial class AppDataContext : DbContext
    {
        public AppDataContext() : base("name=AppDataContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<AuthorizedApp> AuthorizedApps { get; set; }

        public virtual DbSet<FILMS> Films { get; set; }

        public virtual DbSet<films_characters> films_characters { get; set; }

        public virtual DbSet<people> people { get; set; }


        public virtual DbSet<films_species> films_species { get; set; }

        public virtual DbSet<species_people> species_people { get; set; }
        public virtual DbSet<planets_count> planets_count { get; set; }
        
        public virtual DbSet<species> species { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // AuthorizedApp
            modelBuilder.Entity<AuthorizedApp>()
                .Property(e => e.AppToken)
                .IsUnicode(false);
            modelBuilder.Entity<AuthorizedApp>()
                .Property(e => e.AppSecret)
                .IsUnicode(false);

        }
    }
}
