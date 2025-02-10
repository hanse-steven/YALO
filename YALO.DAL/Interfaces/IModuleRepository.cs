namespace YALO.DAL.Interfaces;

public interface IModuleRepository
{
    string GetAll();
    public void SaveAll(string modules);
}