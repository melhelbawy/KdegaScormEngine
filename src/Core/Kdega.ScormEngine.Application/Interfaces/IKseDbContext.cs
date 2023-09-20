using Kdega.ScormEngine.Domain.Entities.Base;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Kdega.ScormEngine.Application.Interfaces;
public interface IKseDbContext
{
    DatabaseFacade Database { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<EventLog> EventLogs { get; }
    #region SCORMs 
    #region Scorm Packages
    DbSet<ScormPackage> ScormPackages { get; }
    DbSet<Organization> Organizations { get; }
    DbSet<OrganizationItem> OrganizationItems { get; }
    DbSet<Resource> Resources { get; }
    DbSet<ResourceFile> ResourceFiles { get; }
    DbSet<ResourceDependency> ResourceDependencies { get; }
    DbSet<LearnerScormPackage> LearnerScormPackages { get; }

    #endregion

    #region Learner Scorms
    DbSet<CmiCore> CmiCores { get; }
    DbSet<CmiData> CmiData { get; }
    DbSet<CmiCommentFromLms> CmiCommentFromLms { get; }
    DbSet<CmiCommentFromLearner> CmiCommentFromLearners { get; }
    DbSet<CmiInteraction> CmiInteractions { get; }
    DbSet<CmiInteractionsCorrectResponse> CmiInteractionsCorrectResponses { get; }
    DbSet<CmiInteractionsObjective> CmiInteractionsObjectives { get; }
    DbSet<CmiLearnerPreference> CmiLearnerPreferences { get; }
    DbSet<CmiObjective> CmiObjectives { get; }
    DbSet<ScormSession> ScormSessions { get; }

    #endregion

    #endregion

    #region XAPIs

    #endregion
}
