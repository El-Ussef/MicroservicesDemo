using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PurchaseManagementApi.Contracts;
using PurchaseManagementApi.Contracts.Requests;
using PurchaseManagementApi.Entities;

namespace PurchaseManagementApi.DataBase;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;

    public AppDbContext(DbContextOptions<AppDbContext> options,
        IMediator mediator,
        IPublishEndpoint publishEndpoint) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Purchase>()
            .Ignore(p => p.Events);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("DataSource=PurchaseDb.db");
    }
    
    public DbSet<Purchase> Purchases { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (_mediator == null) return result;

        // Get Entity that have an event
        var entitiesWithEvents = ChangeTracker
            .Entries()
            .Select(e => e.Entity as IHasEvent)
            .Where(e => e?.Events != null && e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity?.Events.ToArray();
            entity?.Events.Clear();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent,cancellationToken).ConfigureAwait(false);
                //TODO: we can publish directly to MassTransit/RMQ this directly 
                //await _publishEndpoint.Publish(domainEvent,cancellationToken).ConfigureAwait(false);
            }
        }
        return result;
    }
}