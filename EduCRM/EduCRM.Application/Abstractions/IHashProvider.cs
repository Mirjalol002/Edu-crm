namespace EduCRM.Application.Abstractions
{
    public interface IHashProvider
    {
        string GetHash(string value);
    }
}
