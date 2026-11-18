using System.Globalization;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

internal class SessionManager
{
    private const string PatientsSection = "[PATIENTS]";
    private const string EndSection = "[END]";

    private readonly string _sessionPath;

    public SessionManager(string sessionPath = "session.dat")
    {
        _sessionPath = sessionPath;
    }

    public bool Exists()
    {
        return File.Exists(_sessionPath);
    }

    public void Save(Clinic clinic)
    {
        using StreamWriter writer = new StreamWriter(_sessionPath, false, Encoding.UTF8);

        writer.WriteLine(PatientsSection);
        for (int i = 0; i < clinic.Patients.Count; i++)
        {
            Patient p = clinic.Patients[i];
            writer.WriteLine($"{p.FirstName},{p.LastName},{p.DateOfBirth:dd.MM.yyyy},{p.BloodType},{p.Phone}");
        }
        writer.WriteLine(EndSection);
    }

    public int Load(Clinic clinic)
    {
        if (!Exists()) return 0;

        string[] lines = File.ReadAllLines(_sessionPath, Encoding.UTF8);
        string section = "";   
        int added = 0;

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            if (line == "") continue;

            if (line.StartsWith("["))
            {
                section = line;
                continue;
            }

            if (section != PatientsSection) 
                continue;  

            try
            {
                Patient patient = ParsePatient(line);

                if (!PatientExists(clinic, patient))
                {
                    clinic.Patients.Add(patient);
                    added++;
                }
            }
            catch (Exception ex)
            {
                clinic.Logger.LogWarning($"Сесія: пропущено пошкоджений рядок \"{line}\" ({ex.Message})");
            }
        }

        return added;
    }

    private Patient ParsePatient(string line)
    {
        string[] parts = line.Split(',');
        if (parts.Length != 5)
            throw new FormatException("Невірна кількість полів.");

        DateTime dateOfBirth = DateTime.ParseExact(
            parts[2].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
        BloodType bloodType = (BloodType)Enum.Parse(typeof(BloodType), parts[3].Trim());

        return new Patient(parts[0].Trim(), parts[1].Trim(), dateOfBirth, bloodType, parts[4].Trim());
    }

    private bool PatientExists(Clinic clinic, Patient candidate)
    {
        for (int i = 0; i < clinic.Patients.Count; i++)
        {
            Patient p = clinic.Patients[i];
            if (p.FirstName == candidate.FirstName
                && p.LastName == candidate.LastName
                && p.DateOfBirth.Date == candidate.DateOfBirth.Date)
            {
                return true;
            }
        }
        return false;
    }
}