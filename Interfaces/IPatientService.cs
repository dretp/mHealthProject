using mHealthProject.Models.Patient;

namespace mHealthProject.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<BasePatient>> ViewPatients();
    Task<PatientDetail> ViewPatientDetails(int id);
    
    Task<IEnumerable<BasePatient>> SearchPatients(string searchTerms);
}