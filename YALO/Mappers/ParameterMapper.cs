using DTOs = YALO.DTOs;
using Models = YALO.BLL.Models;

namespace YALO.Mappers;

public static class ParameterMapper
{
    public static DTOs.ParameterDTO ToDTO(this Models.Parameter model)
    {
        return new DTOs.ParameterDTO
        {
            Type = model.Type,
            Value = model.Value
        };
    }
}