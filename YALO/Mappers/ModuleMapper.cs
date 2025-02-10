using DTOs = YALO.DTOs;
using Models = YALO.BLL.Models;
using YALO.Mappers;

namespace YALO.Mappers;

public static class ModuleMapper
{
    public static DTOs.ModuleDTO ToDTO(this Models.Module model)
    {
        return new DTOs.ModuleDTO
        {
            Id = model.Id,
            Name = model.Name,
            X = model.X,
            Y = model.Y,
            Width = model.Width,
            Height = model.Height,
            Parameters = model.Parameters.ToDictionary(s => s.Key, s => s.Value?.ToDTO())
        };
    }
}