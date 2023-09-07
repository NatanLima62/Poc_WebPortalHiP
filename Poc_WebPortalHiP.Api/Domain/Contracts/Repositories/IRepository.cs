using System.Linq.Expressions;
using Poc_WebPortalHiP.Api.Domain.Entities;

namespace Poc_WebPortalHiP.Api.Domain.Contracts.Repositories;

public interface IRepository<T> : IDisposable where T : Entity, IAggregateRoot
{
    public IUnitOfWork UnitOfWork { get; }
    Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression);
}