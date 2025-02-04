using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using mHealthProject.Models.Patient;
using mHealthProject.Utils.Base;

namespace mHealthProject.Utils.Patient;

public class PatientUtil() : BaseUtil
{

    #region Public Method

    public async Task<IEnumerable<BasePatient>> RetrievePatientList()
    {
        var sql = new StringBuilder(
            "SELECT id, first_name, last_name, dob, gender ");
        sql.Append("FROM patient ORDER BY last_name, first_name; ");

        try
        {
            await using var conn = connection();
            var cmd = command(sql.ToString(), conn);
            
            await using var reader = await cmd.ExecuteReaderAsync();
            
            var patientList = new List<BasePatient>();

            while (await reader.ReadAsync())
            {
                var patient = new BasePatient
                {
                    id = reader.GetInt32(reader.GetOrdinal("id"))
                };

                var firstname = reader.GetString(reader.GetOrdinal("first_name"));
                var lastname = reader.GetString(reader.GetOrdinal("last_name"));
                patient.gender = reader.GetString(reader.GetOrdinal("gender"));
                
                
                var dob = reader.GetDateTime(reader.GetOrdinal("dob")).ToString("MM/dd/yyyy");
                patient.dob = dob;
                
                patient.name = $"{firstname} {lastname}";
                
                patientList.Add(patient);
            }
            
            
            return patientList;
        }
        catch (Exception e)
        {
            LogError(e, "PatientService.RetrievePatientList");
            return new List<BasePatient>();
        }
    }


    public async Task<PatientDetail> RetrievePatientDetails(int id)
    {

        var sql = new StringBuilder("SELECT id, first_name, last_name, dob, gender, age, med_condition, meds, results, blood_type ");
        sql.Append(" FROM patient WHERE id = @id;");

        try
        {
            await using var conn = connection();
            var cmd = command(sql.ToString(), conn);
            
            cmd.Parameters.AddWithValue("id", id);
            
            await using var reader = await cmd.ExecuteReaderAsync();

            var patient = new PatientDetail();
            
            while (await reader.ReadAsync())
            {
                patient.id = reader.GetInt32(reader.GetOrdinal("id"));
                
                var firstname = reader.GetString(reader.GetOrdinal("first_name"));
                var lastname = reader.GetString(reader.GetOrdinal("last_name"));
                patient.name = $"{firstname} {lastname}";
                
                patient.gender = reader.GetString(reader.GetOrdinal("gender"));
                
                var dob = reader.GetDateTime(reader.GetOrdinal("dob")).ToString("MM/dd/yyyy");
                patient.dob = dob;
                
                patient.Age = reader.GetInt32(reader.GetOrdinal("age"));
                patient.Condition = reader.GetString(reader.GetOrdinal("med_condition"));
                patient.Medications = reader.GetString(reader.GetOrdinal("meds"));
                patient.BloodType = reader.GetString(reader.GetOrdinal("blood_type"));
                patient.TestResults = reader.GetString(reader.GetOrdinal("results"));
            }

            patient.Recommendations = (List<string>)await RetrievePatientRecommendations(patient.Condition);
            
            return patient;
        }
        catch (Exception e)
        {
            LogError(e, "PatientService.RetrievePatientDetails");
            throw new Exception("Unable to retrieve patient details");
        }
    }

    public async Task<IEnumerable<BasePatient>> SearchPatients(string searchTerm)
    {
        var sql = new StringBuilder("SELECT id, first_name, last_name, dob, gender, age, med_condition, meds, results, blood_type ");
        sql.Append(" FROM patient WHERE ");
        sql.Append(" (lower(first_name) LIKE @search1)  ");
        sql.Append(" OR (lower(last_name) LIKE @search2) ");
        sql.Append(" OR (lower(med_condition) LIKE @search3) ");

        try
        {
            await using var conn = connection();
            var cmd = command(sql.ToString(), conn);

            cmd.Parameters.AddWithValue("search1", $"%{searchTerm.ToLower()}%");
            cmd.Parameters.AddWithValue("search2", $"%{searchTerm.ToLower()}%");
            cmd.Parameters.AddWithValue("search3", $"%{searchTerm.ToLower()}%");
            
            await using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return new List<BasePatient>();
            }

            var patientList = new List<BasePatient>();
            
            while (await reader.ReadAsync())
            {
                var patient = new BasePatient
                {
                    id = reader.GetInt32(reader.GetOrdinal("id"))
                };

                var firstname = reader.GetString(reader.GetOrdinal("first_name"));
                var lastname = reader.GetString(reader.GetOrdinal("last_name"));
                patient.gender = reader.GetString(reader.GetOrdinal("gender"));
                
                
                var dob = reader.GetDateTime(reader.GetOrdinal("dob")).ToString("MM/dd/yyyy");
                patient.dob = dob;
                
                patient.name = $"{firstname} {lastname}";
                
                patientList.Add(patient);
            }
            
            return patientList;
        }
        catch (Exception e)
        {
            LogError(e, "PatientService.SearchPatients");
            return new List<BasePatient>();    
        }
        
    }

    #endregion




    #region Private Method

    private async Task<IEnumerable<string>> RetrievePatientRecommendations(string condition)
    {
        var sql = new StringBuilder("SELECT sample_name FROM recommendations WHERE keywords LIKE (@condition);");

        try
        {
            await using var conn = connection();
            var cmd = command(sql.ToString(), conn);
            
            cmd.Parameters.AddWithValue("condition", $"%{condition}%");
            
            await using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return new List<string>();
            }

            var recommendations = new List<string>();
            
            while (await reader.ReadAsync())
            {
                recommendations.Add(reader.GetString(reader.GetOrdinal("sample_name")));    
            }
            
            return recommendations;
        }
        catch (Exception e)
        {
            LogError(e, "PatientService.RetrievePatientRecomendations");
            return [];
        }
    }

    #endregion
}