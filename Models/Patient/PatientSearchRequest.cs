using System.Text.Json.Serialization;

namespace mHealthProject.Models.Patient;

public class PatientSearchRequest()
{
    [JsonPropertyName("searchText")]
    public string SearchText { get; set; }
}