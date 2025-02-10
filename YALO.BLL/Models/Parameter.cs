namespace YALO.BLL.Models;

public class Parameter(Type type, dynamic value)
{
    public Type Type { get; set; } = type;
    public dynamic Value { get; set; } = value;
}