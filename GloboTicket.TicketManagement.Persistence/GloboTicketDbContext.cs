using System;
using System.Threading;
using System.Threading.Tasks;
using GloboTicket.TicketManagement.Domain.Common;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.TicketManagement.Persistence
{
    public class GloboTicketDbContext : DbContext
    {
        public GloboTicketDbContext(DbContextOptions<GloboTicketDbContext> options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GloboTicketDbContext).Assembly);
            
            //seed data, added through migrations
            var concertGuid = Guid.Parse("{E81A1D30-9AE8-4211-9A90-2FD2E86EC204}");
            var musicalGuid = Guid.Parse("{6C434217-93FD-4ECC-8E7A-6492067AD19E}");
            var playGuid = Guid.Parse("{A3D29490-DEC6-49AF-B171-384F6EB24174}");
            var conferenceGuid = Guid.Parse("{4274CBF8-839D-4298-95D4-3C9DF8FB4A36}");

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = concertGuid,
                Name = "Concerts"
            });       
            
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = musicalGuid,
                Name = "Musicals"
            });         
            
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = playGuid,
                Name = "Plays"
            });            
            
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = conferenceGuid,
                Name = "Conferences"
            });            
            
            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{3888CBF8-839D-4298-95D4-3C9DF8FB4A36}"),
                CategoryId = concertGuid,
                Name = "John Egbert Live",
                Price = 65,
                Artist = "John Egbert",
                Date = DateTime.Now.AddMonths(6),
                Description = "Johns concert",
                ImageUrl = "https://picsum.photos/200/300"
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)    
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}