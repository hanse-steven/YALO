using Models = YALO.BLL.Models;
using Entities = YALO.DAL.Entities;

namespace YALO.BLL.Mappers;

public static class ModuleMapper
{
    public static Models.Module ToModel(this Entities.Module entity)
    {
        return new Models.Module
        {
            Id = entity.Id,
            Name = entity.Name,
            Color = entity.Color,
            X = entity.X,
            Y = entity.Y,
            Width = entity.Width,
            Height = entity.Height
        };
    }
    
    public static Entities.Module ToEntity(this Models.Module model)
    {
        return new Entities.Module
        {
            Id = model.Id,
            Name = model.Name,
            Color = model.Color,
            X = model.X,
            Y = model.Y,
            Width = model.Width,
            Height = model.Height
        };
    }
}