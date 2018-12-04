
using FriendOrganizer.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FriendOrganizer.DataAccess
{
    /// <summary>
    /// We need to enable migrations for the DataAccess project
    /// In Visual Studio go to Tools--> Nuget Package Manager
    /// open up the package manager console and Enable-Migrations
    /// seed the datbase and then call Add-Migration InitialDatabase
    /// </summary>
    public class FriendOrganizerDbContext : DbContext
    {
        public FriendOrganizerDbContext():base("FriendOrganizerDb")
        {

        }
        // access the friend class to load and save friends
        public DbSet<Friend> Friends { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        //entity framework will pluralise the table names by default
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //remove a default convention
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //fluent API to Add Constraints - move to its own class
            //modelBuilder.Entity<Friend>()
            //    .Property(f => f.FirstName)
            //    .IsRequired()
            //    .HasMaxLength(50);

            //modelBuilder.Configurations.Add(new FriendConfiguration());
        }

    }
    /// <summary>
    ///  //fluent API to Add Constraints in seperate class - use data annotations instead in your model calss
    /// </summary>
    //public class FriendConfiguration : EntityTypeConfiguration<Friend>
    //{
    //    public FriendConfiguration()
    //    {
    //         Property(f => f.FirstName)
    //            .IsRequired()
    //            .HasMaxLength(50);
    //    }
          
    //}
}
