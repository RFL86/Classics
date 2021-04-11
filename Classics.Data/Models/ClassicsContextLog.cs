using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Classics.Data.Mapping;


namespace Classics.Data.Models
{
    public class ClassicsContextLog : DbContext
    {
        public ClassicsContextLog(string contextName, ExtraInfo extra = null) : base("data source=classics.database.windows.net;initial catalog=classics;persist security info=True;user id=developer;password=devapp@22;multipleActiveResultSets=True")
        {
        }

        public static string GetConnectionString()
        {
            return "data source=classics.database.windows.net;initial catalog=classics;persist security info=True;user id=developer;password=devapp@22;multipleActiveResultSets=True";
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<BlobFile> BlobFile { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<CarModel> CarModel { get; set; }
        public DbSet<MyCar> MyCar { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Alert> Alert { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        //public DbSet<SystemLogTable> SystemLogTable { get; set; }
        //public DbSet<SystemLog> SystemLog { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ClassicsContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new BlobFileMap());
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new CarModelMap());
            modelBuilder.Configurations.Add(new MyCarMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new SerieMap());
            modelBuilder.Configurations.Add(new AlertMap());
            modelBuilder.Configurations.Add(new SupplierMap());
        }
    }
}
