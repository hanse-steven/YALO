namespace YALO.DTOs;

public class ModuleDTO
{
    public Guid Id {get; set;}
    public string Name {get; set;}
    public int X {get; set;}
    public int Y {get; set;}
    public int Width {get; set;}
    public int Height {get; set;}
    public string Color => "greenyellow";
    public Dictionary<string, ParameterDTO?> Parameters { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, X: {X}, Y: {Y}, Width: {Width}, Height: {Height}, Color: {Color}";
    }
}