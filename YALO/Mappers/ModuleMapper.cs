using DTOs = YALO.DTOs;
using Models = YALO.BLL.Models;

namespace YALO.Mappers;

public static class ModuleMapper
{
    public static Models.Module ToModel(this DTOs.ModuleDTO dto)
    {
        return new Models.Module
        {
            Id = dto.Id,
            Name = dto.Name,
            X = dto.X,
            Y = dto.Y,
            Width = dto.Width,
            Height = dto.Height,
            Color = dto.Color
        };
    }
    
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
            Color = model.Color
        };
    }
}