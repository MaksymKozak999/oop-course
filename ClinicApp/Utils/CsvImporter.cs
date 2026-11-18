using System.Globalization;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

internal class CsvImporter
{
    private const int ExpectedFields = 5;

    public ImportResult ImportPatients(Clinic clinic, string filePath)
    {
        ImportResult result = new ImportResult();

        if (!File.Exists(filePath))
        {
            result.AddError(0, $"Файл не знайдено: {filePath}");
            return result;
        }

        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        for (int i = 0; i < lines.Length; i++)
        {
            int lineNumber = i + 1;         
            string line = lines[i];

            if (i == 0) continue;                          
            if (string.IsNullOrWhiteSpace(line)) continue; 

            try
            {
                Patient patient = ParsePatient(line);
                clinic.Patients.Add(patient);
                result.AddSuccess();
            }
            catch (Exception ex)
            {
                result.AddError(lineNumber, ex.Message);
            }
        }

        return result;
    }

    private Patient ParsePatient(string line)
    {
        string[] parts = line.Split(',');

        if (parts.Length != ExpectedFields)
            throw new FormatException(
                $"Рядок має {parts.Length} {FieldWord(parts.Length)} замість {ExpectedFields}.");

        string firstName = parts[0].Trim();
        string lastName = parts[1].Trim();
        string dateText = parts[2].Trim();
        string bloodText = parts[3].Trim();
        string phone = parts[4].Trim();

        if (firstName == "")
            throw new FormatException("Ім'я не може бути порожнім.");
        if (lastName == "")
            throw new FormatException("Прізвище не може бути порожнім.");

        DateTime dateOfBirth;
        try
        {
            dateOfBirth = DateTime.ParseExact(dateText, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
        catch (FormatException)
        {
            throw new FormatException($"Некоректна дата народження \"{dateText}\" (потрібен формат дд.мм.рррр).");
        }

        BloodType bloodType;
        try
        {
            bloodType = (BloodType)Enum.Parse(typeof(BloodType), bloodText);
            if (!Enum.IsDefined(typeof(BloodType), bloodType))  
                throw new ArgumentException();
        }
        catch (ArgumentException)
        {
            throw new FormatException($"Невідома група крові \"{bloodText}\".");
        }

        return new Patient(firstName, lastName, dateOfBirth, bloodType, phone);
    }

    private static string FieldWord(int count)
    {
        if (count == 1) 
            return "поле";
        if (count >= 2 && count <= 4) 
            return "поля";
        return "полів";
    }
}