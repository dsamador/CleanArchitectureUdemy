using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {
        //Vamos a iniciar esta instancia de EF con ayuda de un constructor
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options)
        {

        }

        //Se va a ejecutar antes de que se inserte o actualice el nuevo registro dentro de la bd
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //recorre todas las entidades antes de hacer la insercion
            foreach(var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {//valores por defecto en el momento de insertar un registro
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);//con esto confirmamos el registro
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder
        //    optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.; " +
        //        "Initial Catalog=Streamer; Integrated Security=True")
        //        .LogTo
        //        (
        //            Console.WriteLine, 
        //            new[] {DbLoggerCategory.Database.Command.Name},
        //            Microsoft.Extensions.Logging.LogLevel.Information
        //        )
        //        .EnableSensitiveDataLogging();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Video>()
                .HasMany(p => p.Actores)
                .WithMany(t => t.Videos)
                .UsingEntity<VideoActor>(
                    pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );
                
        }

        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }
        
    }
}
