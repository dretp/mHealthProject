using System.Text.Json.Serialization;

namespace mHealthProject.Models.Patient;

public class BasePatient()
{
    [JsonPropertyName("patientId")]
    public int id { get; set; } = -1;
    
    [JsonPropertyName("patientName")]
    public string name { get; set; } = null!;
    
    [JsonPropertyName("dob")]
    public string dob { get; set; } = null;
    
    [JsonPropertyName("gender")]
    public string gender { get; set; } = null!;
    
    
}