using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Poc_WebPortalHiP.Api.Domain.Contracts;
using Poc_WebPortalHiP.Api.Domain.Contracts.Repositories;
using Poc_WebPortalHiP.Api.Domain.Entities;
using Poc_WebPortalHiP.Api.Infra.Contexts;

namespace Poc_WebPortalHiP.Api.Infra.Repositories;

public abstract class Repository<T> : IRepository<T> where T : Entity, IAggregateRoot
{
    private bool _isDisposed;
    private readonly DbSet<T> _dbSet;
    protected readonly BaseApplicationDbContext Context;
    
    protected Repository(BaseApplicationDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().Where(expression).FirstOrDefaultAsync();
    }

    public IUnitOfWork UnitOfWork => Context;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            // free managed resources
            Context.Dispose();
        }

        _isDisposed = true;
    }
    
    ~Repository()
    {
        Dispose(false);
    }
}