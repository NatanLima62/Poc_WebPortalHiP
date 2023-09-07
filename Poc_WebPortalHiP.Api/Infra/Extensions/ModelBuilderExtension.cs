using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Poc_WebPortalHiP.Api.Domain.Contracts;

namespace Poc_WebPortalHiP.Api.Infra.Extensions;

public static class ModelBuilderExtension
{
    public static void ApplyEntityConfiguration(this ModelBuilder modelBuilder)
    {
        var entities = modelBuilder.GetEntities<IEntity>();
        var props = entities.SelectMany(c => c.GetProperties()).ToList();

        foreach (var property in props.Where(c => c.ClrType == typeof(int) && c.Name == "Id"))
        {
            property.IsKey();
        }
    }

    private static List<IMutableEntityType> GetEntities<T>(this ModelBuilder modelBuilder)
    {
        var entities = modelBuilder.Model.GetEntityTypes()
            .Where(c => c.ClrType.GetInterface(typeof(T).Name) != null).ToList();

        return entities;
    }
}
