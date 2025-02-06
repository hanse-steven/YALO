namespace YALO.DTOs;

public class PositionDimension
{
    public int Id { get; set; }
    public int Left { get; set; }
    public int Top { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public override string ToString()
    {
        return $"Id: {Id.ToString(),4} Left: {Left.ToString(),4} Top: {Top.ToString(),4} Width: {Width.ToString(),4} Height: {Height.ToString(),4}";
    }
}