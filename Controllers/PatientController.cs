using mHealthProject.Interfaces;
using mHealthProject.Models.Patient;
using Microsoft.AspNetCore.Mvc;

namespace mHealthProject.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController(ILogger<PatientController> logger, IPatientService svc) : Controller
{
    // GET
    [HttpGet("list")]
    public async Task<IActionResult> GetPatientList()
    {
        try
        {
            var patientList = await svc.ViewPatients();
            return Ok(patientList);
        }
        catch (Exception e)
        {
            return BadRequest("Unable to retrieve patients");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatientById(int id)
    {
        try
        {
            var patient = await svc.ViewPatientDetails(id);
            return Ok(patient);
        }
        catch (Exception e)
        {
            return BadRequest("Unable to retrieve patient");
        }
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchPatients(PatientSearchRequest searchTerms)
    {
        var patient = await svc.SearchPatients(searchTerms.SearchText);
        return Ok(patient);
    }
}