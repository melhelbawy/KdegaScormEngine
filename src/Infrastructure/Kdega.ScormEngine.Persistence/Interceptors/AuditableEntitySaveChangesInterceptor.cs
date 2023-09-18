using Kdega.ScormEngine.Application.Services;
using Kdega.ScormEngine.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdega.ScormEngine.Persistence.Interceptors;
public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly CurrentUserService _currentUserService;

    public AuditableEntitySaveChangesInterceptor(CurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<EntityCreation>())
        {
            if (entry.State == EntityState.Added)
            {
                //entry.Entity.CreatedBy = _currentUserService.UserId!.Value;      // ToDo: Uncomment
                entry.Entity.CreatedBy = _currentUserService.UserId ?? Guid.Empty; // ToDo: Remove 
                entry.Entity.CreatedOn = DateTime.Now;
            }

        }

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.ModifiedBy = _currentUserService.UserId;
                entry.Entity.ModifiedOn = DateTime.Now;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned());
}
