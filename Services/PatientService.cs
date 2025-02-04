using mHealthProject.Interfaces;
using mHealthProject.Models.Patient;
using mHealthProject.Utils.Patient;

namespace mHealthProject.Services;

public class PatientService() : IPatientService
{
    private readonly PatientUtil _patientUtil = new();
    
    public async Task<IEnumerable<BasePatient>> ViewPatients()
    {
        return await _patientUtil.RetrievePatientList();
    }

    public async Task<PatientDetail> ViewPatientDetails(int id)
    {
        return await _patientUtil.RetrievePatientDetails(id);
    }

    public async Task<IEnumerable<BasePatient>> SearchPatients(string searchTerms)
    {
        return await _patientUtil.SearchPatients(searchTerms);
    }
}