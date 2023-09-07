using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Poc_WebPortalHiP.Api.Domain.Contracts;
using Poc_WebPortalHiP.Api.Domain.Entities;
using Poc_WebPortalHiP.Api.Infra.Extensions;

namespace Poc_WebPortalHiP.Api.Infra.Contexts;

public abstract class BaseApplicationDbContext : DbContext, IUnitOfWork
{
    protected BaseApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ApplyConfigurations(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit() => await SaveChangesAsync() > 0;

    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        
        modelBuilder.ApplyEntityConfiguration();
    }
}