
/// <summary>
/// Allows using the pattern adapter for parser
/// </summary>
public interface IParser 
{
    string Serialize<T>(T data);
    T Deserialize<T>(string data);
}
