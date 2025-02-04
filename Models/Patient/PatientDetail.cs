using System.Text.Json.Serialization;

namespace mHealthProject.Models.Patient;

public class PatientDetail() : BasePatient
{
    [JsonPropertyName("age")]
    public int Age { get; set; }
    
    [JsonPropertyName("bloodType")]
    public string BloodType { get; set; }
    
    [JsonPropertyName("medCondition")]
    public string Condition { get; set; }
    
    [JsonPropertyName("meds")]
    public string Medications { get; set; }
    
    [JsonPropertyName("results")]
    public string TestResults {get; set;}
    
    [JsonPropertyName("recommendations")]
    public List<string> Recommendations { get; set; } = new List<string>();
}