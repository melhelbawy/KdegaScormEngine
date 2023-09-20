using Kdega.ScormEngine.Application.Interfaces;
using Kdega.ScormEngine.Domain.Entities.LearnerScorms;
using Kdega.ScormEngine.Domain.Entities.ScormPackages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Kdega.ScormEngine.Persistence;
public class KseDbContext : DbContext, IKseDbContext
{
    public KseDbContext(DbContextOptions options) : base(options)
    {

    }
    #region SCORMs
    public DbSet<ScormPackage> ScormPackages => Set<ScormPackage>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<OrganizationItem> OrganizationItems => Set<OrganizationItem>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<ResourceFile> ResourceFiles => Set<ResourceFile>();
    public DbSet<ResourceDependency> ResourceDependencies => Set<ResourceDependency>();
    public DbSet<LearnerScormPackage> LearnerScormPackages => Set<LearnerScormPackage>();

    public DbSet<CmiCore> CmiCores => Set<CmiCore>();
    public DbSet<CmiData> CmiData => Set<CmiData>();
    public DbSet<CmiCommentFromLms> CmiCommentFromLms => Set<CmiCommentFromLms>();
    public DbSet<CmiCommentFromLearner> CmiCommentFromLearners => Set<CmiCommentFromLearner>();
    public DbSet<CmiInteraction> CmiInteractions => Set<CmiInteraction>();
    public DbSet<CmiInteractionsCorrectResponse> CmiInteractionsCorrectResponses => Set<CmiInteractionsCorrectResponse>();
    public DbSet<CmiInteractionsObjective> CmiInteractionsObjectives => Set<CmiInteractionsObjective>();
    public DbSet<CmiLearnerPreference> CmiLearnerPreferences => Set<CmiLearnerPreference>();
    public DbSet<CmiObjective> CmiObjectives => Set<CmiObjective>();
    public DbSet<ScormSession> ScormSessions => Set<ScormSession>();
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
