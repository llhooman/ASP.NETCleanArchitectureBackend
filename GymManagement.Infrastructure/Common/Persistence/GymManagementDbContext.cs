using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Common.Persistence
{
    public class GymManagementDbContext :DbContext,IUnitOfWork
    {
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Gym> Gyms { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public GymManagementDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpcontextAccessor = httpContextAccessor;
        }

        public async Task CommitChangesAsync()
        {
            //get hold of all the domain events
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(entry => entry.Entity.PopDomainEvents())
                .SelectMany(x => x)
                .ToList();
            //store them in the http context for later
            AddDomainEventsToOfflineProcessingQueue(domainEvents);
            await base.SaveChangesAsync(); 
        }

        private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
        {
            // fetch queue from http context or create new queue if it dosen't exist
            //if its the second time fetching the queue we
            //don't want to initialize it and just add the domainEvents to the end of the queue
            var domainEventsQueue = _httpcontextAccessor.HttpContext!.Items
                .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents
                ? existingDomainEvents
                : new Queue<IDomainEvent>();
             
            // add the domain events to the end of the queue
            domainEvents.ForEach(domainEventsQueue.Enqueue);
            //store the queue in the http context
            _httpcontextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
