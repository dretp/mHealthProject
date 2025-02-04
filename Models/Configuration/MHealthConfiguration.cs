namespace mHealthProject.Models.Configuration;

public class MHealthConfiguration
{
    public static MHealthConfiguration Instance = null;

    public MHealthConfiguration()
    {
        Init();
    }

    private void Init()
    {
        Instance ??= this;
    }

    public MHealthConfiguration GetInstance()
    {
        return Instance;
    }
    
    public string ConnectionString { get; set; } = string.Empty;
    public string JwtSecret { get; set; } = string.Empty;
}