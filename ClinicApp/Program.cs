using System;
using System.Globalization;
using ClinicApp;

using ClinicApp.Enums;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
        
        Clinic clinic = new Clinic("Медична Клініка");
        
        Patient p1 = new Patient("Maksym", "Петренко", new DateTime(2000, 10, 8), BloodType.ANegative, "0980908307", "text@gmail.com");
        Patient p2 = new Patient("Oleh", "Петренко", new DateTime(2010, 10, 8), BloodType.ANegative, "0980908307", "text@gmail.com");
        Patient p3 = new Patient("Anna", "Коваленко", new DateTime(1995, 5, 12), BloodType.BPositive, "0501234567", "anna@gmail.com");

        WorkSchedule w1 = new WorkSchedule(8, 17);
        Doctor d1 = new Doctor("Іван", "Петренко", "LIC-001", "0501111111", w1, Speciality.Cardiology);
        Doctor d2 = new Doctor("Олександр", "Шевченко", "LIC-002", "0502222222", w1, Speciality.Surgery);
        Doctor d3 = new Doctor("Андрій", "Мельник", "LIC-003", "0503333333", w1, Speciality.Neurology);

        clinic.Patients.Add(p1);
        clinic.Patients.Add(p2);
        clinic.Patients.Add(p3);

        clinic.Doctors.Add(d1);
        clinic.Doctors.Add(d2);
        clinic.Doctors.Add(d3);

        
        clinic.MedicalRecords.Add(new Diagnosis(1, 1, DateTime.Today.AddDays(-30), "I10", "Гіпертонічна хвороба", true));
        clinic.MedicalRecords.Add(new Diagnosis(1, 1, DateTime.Today.AddDays(-5), "J06.9", "Гостра респіраторна інфекція", false));
        clinic.MedicalRecords.Add(new LabResult(1, 1, DateTime.Today.AddDays(-5), "Гемоглобін", 145, "г/л", "120-160", true));
        clinic.MedicalRecords.Add(new LabResult(1, 1, DateTime.Today.AddDays(-5), "Глюкоза", 6.2, "ммоль/л", "< 5.2", false));
        clinic.MedicalRecords.Add(new Prescription(1, 1, DateTime.Today.AddDays(-5), "Лізиноприл", "10 мг", 30, "1 раз на добу"));

       
        clinic.MedicalRecords.Add(new Prescription(2, 2, DateTime.Today.AddDays(-40), "Амоксицилін", "500 мг", 10, "3 рази на добу"));
        clinic.MedicalRecords.Add(new Diagnosis(2, 2, DateTime.Today.AddDays(-40), "J20", "Гострий бронхіт", false));

      
        clinic.MedicalRecords.Add(new LabResult(3, 3, DateTime.Today.AddDays(-2), "Загальний аналіз крові", 5.0, "10^9/л", "4.0-9.0", true));

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n=== ClinicApp: Головне меню ===");
            Console.WriteLine("1. Пацієнти");
            Console.WriteLine("2. Лікарі");
            Console.WriteLine("3. Запис на прийом");
            Console.WriteLine("4. Медична картка");
            Console.WriteLine("5. Розклад");
            Console.WriteLine("6. Звіт");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть розділ: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ManagePatients(clinic);
                    break;

                case "2":
                    ManageDoctors(clinic);
                    break;

                case "3":
                    ManageAppointments(clinic);
                    break;
                case "4":
                    ManageRecords(clinic);
                    break;

                case "5":
                    DateTime date;
                    bool isSuccess = false;
                    
                    do
                    {
                        Console.Write("Введіть дату (дд.мм.рррр): ");
                        string? inputString = Console.ReadLine();
                        isSuccess = DateTime.TryParseExact(inputString, "dd.MM.yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
                        if (!isSuccess)
                            Console.WriteLine("Помилка! Введіть дату у форматі дд.мм.рррр (наприклад, 25.10.2026).");
                    } while (!isSuccess);
    
                    clinic.DisplaySchedule(date);
                    break;

                case "6":
                    clinic.GenerateReport();
                    break;

                case "0":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }


    static void ManagePatients(Clinic clinic)
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("\n--- Підменю: Пацієнти ---");
            Console.WriteLine("1. Показати всіх");
            Console.WriteLine("2. Додати пацієнта");
            Console.WriteLine("3. Знайти за ID (TryFindById + out)");
            Console.WriteLine("4. Знайти за ім'ям/прізвищем");
            Console.WriteLine("5. Знайти за групою крові");
            Console.WriteLine("6. Видалити пацієнта");
            Console.WriteLine("7. Статистика");
            Console.WriteLine("0. Назад у головне меню");
            Console.Write("Оберіть дію: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    clinic.Patients.DisplayAll();
                    break;

                case "2":
                    try
                    {
                        Console.Write("Ім'я: ");
                        string firstName = Console.ReadLine();

                        Console.Write("Прізвище: ");
                        string lastName = Console.ReadLine();

                        Console.Write("Дата народження (дд.мм.рррр): ");
                        DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

                        Console.Write("Група крові: ");
                        string bloodTypeStr = Console.ReadLine();
                        if (!Enum.TryParse(bloodTypeStr, true, out BloodType bloodType))
                        {
                            bloodType = BloodType.Unknown;
                        }

                        Console.Write("Телефон: ");
                        string phone = Console.ReadLine();

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Patient patient = new Patient(
                            firstName,
                            lastName,
                            dateOfBirth,
                            bloodType,
                            phone,
                            email
                        );

                        clinic.Patients.Add(patient);
                        Console.WriteLine("Пацієнта додано");
                    }
                    catch (ArgumentException  e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    break;
                    

                case "3":
                    Console.Write("Введіть ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int searchId))
                    {
                        Console.WriteLine("Помилка: Некоректне значення ID.");
                        break;
                    }

                    if (clinic.Patients.TryFindById(searchId, out Patient found))
                    {
                        string status = found.IsAdult ? "дорослий" : "дитина";
                        Console.WriteLine(
                            $"Знайдено: [{found.Id}] " +
                            $"{found.FirstName} {found.LastName} | " +
                            $"Вік: {found.Age} ({status})"
                        );
                    }
                    else
                    {
                        Console.WriteLine($"Пацієнта з ID {searchId} не знайдено.");
                    }

                    break;

                case "4":
                    Console.Write("Введіть ім'я або прізвище: ");
                    string query = Console.ReadLine();

                    Patient[] results = clinic.Patients.FindByName(query);

                    if (results.Length > 0)
                    {
                        Console.WriteLine($"Знайдено пацієнтів ({results.Length}):");
                        foreach (Patient p in results)
                        {
                            Console.WriteLine($"[{p.Id}] {p.FirstName} {p.LastName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Пацієнтів за запитом '{query}' не знайдено.");
                    }

                    break;

                case "5":
                    Console.Write("Введіть групу крові (наприклад, APositive): ");
                    string btInput = Console.ReadLine();

                    if (Enum.TryParse(btInput, true, out BloodType btQuery))
                    {
                        Patient[] btResults = clinic.Patients.FindByBloodType(btQuery);
                        Console.WriteLine($"Знайдено пацієнтів з групою {btQuery} ({btResults.Length}):");
                        foreach (Patient p in btResults)
                        {
                            Console.WriteLine($"[{p.Id}] {p.FullName} | {p.BloodType}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некоректна група крові.");
                    }

                    break;

                case "6":
                    Console.Write("Введіть ID для видалення: ");
                    int removeId = int.Parse(Console.ReadLine());

                    if (clinic.Patients.Remove(removeId))
                    {
                        Console.WriteLine($"Пацієнта з ID {removeId} успішно видалено.");
                    }
                    else
                    {
                        Console.WriteLine($"Пацієнта з ID {removeId} не знайдено.");
                    }

                    break;

                case "7":
                    clinic.Patients.DisplayStats();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static void ManageRecords(Clinic clinic)
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("\n--- Підменю: Медична картка ---");
            Console.WriteLine("1. Картка пацієнта");
            Console.WriteLine("2. Усі записи пацієнта");
            Console.WriteLine("3. Додати діагноз");
            Console.WriteLine("4. Додати аналіз");
            Console.WriteLine("5. Додати рецепт");
            Console.WriteLine("6. Записи пацієнта (поліморфний список)");
            Console.WriteLine("0. Назад у головне меню");
            Console.Write("Оберіть дію: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        Console.Write("Введіть ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pId))
                        {
                            Console.WriteLine("Помилка: Некоректне значення ID.");
                            break;
                        }

                        if (clinic.Patients.FindById(pId) == null)
                        {
                            Console.WriteLine("не знайдено");
                            break;
                        }

                        clinic.MedicalRecords.DisplayPatientSummary(pId);
                        break;
                    }

                case "2":
                    {
                        Console.Write("Введіть ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pId))
                        {
                            Console.WriteLine("Помилка: Некоректне значення ID.");
                            break;
                        }

                        if (clinic.Patients.FindById(pId) == null)
                        {
                            Console.WriteLine("не знайдено");
                            break;
                        }

                        var records = clinic.MedicalRecords.GetByPatient(pId);
                        clinic.MedicalRecords.DisplayList(records);
                        break;
                    }

                case "3": 
                    {
                        Console.WriteLine("\n--- Наявні пацієнти ---");
                        clinic.Patients.DisplayAll();
                        Console.WriteLine("\n--- Наявні лікарі ---");
                        clinic.Doctors.DisplayAll();

                        Console.Write("\nВведіть ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pId))
                        {
                            Console.WriteLine("Помилка: Введено не число.");
                            break;
                        }
                        if (clinic.Patients.FindById(pId) == null)
                        {
                            Console.WriteLine("не знайдено");
                            break;
                        }

                        Console.Write("Введіть ID лікаря: ");
                        if (!int.TryParse(Console.ReadLine(), out int dId))
                        {
                            Console.WriteLine("Помилка: Введено не число.");
                            break;
                        }
                        if (clinic.Doctors.FindById(dId) == null)
                        {
                            Console.WriteLine("не знайдено");
                            break;
                        }

                        Console.Write("Код МКХ-10 (наприклад, I10): ");
                        string code = Console.ReadLine();

                        Console.Write("Опис діагнозу: ");
                        string description = Console.ReadLine();

                        Console.Write("Хронічне захворювання? (1 - Так, 0 - Ні): ");
                        bool isChronic = Console.ReadLine() == "1";

                        try
                        {
                            var diag = new Diagnosis(pId, dId, DateTime.Now, code, description, isChronic);
                            clinic.MedicalRecords.Add(diag);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        break;
                    }

                case "4": 
                    {
                        Console.WriteLine("\n--- Наявні пацієнти ---");
                        clinic.Patients.DisplayAll();
                        Console.WriteLine("\n--- Наявні лікарі ---");
                        clinic.Doctors.DisplayAll();

                        Console.Write("\nВведіть ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pId))
                        {
                            Console.WriteLine("Помилка: Введено не число.");
                            break;
                        }
                        if (clinic.Patients.FindById(pId) == null)
                        {
                            Console.WriteLine("не знайдено");
                            break;
                        }

                        Console.Write("Введіть ID лікаря: ");
                        if (!int.TryParse(Console.ReadLine(), out int dId))
                        {
                            Console.WriteLine("Помилка: Введено не число.");
                            break;
                        }
                        if (clinic.Doctors.FindById(dId) == null)
                        {
                            Console.WriteLine("не знайдено");
                            break;
                        }

                        Console.Write("Назва аналізу: ");
                        string testName = Console.ReadLine();

                        Console.Write("Значення аналізу (наприклад, 145 або 6.2): ");
                        if (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                        {
                            Console.WriteLine("Помилка: Некоректне числове значення.");
                            break;
                        }

                        Console.Write("Одиниці вимірювання (наприклад, г/л): ");
                        string unit = Console.ReadLine();

                        Console.Write("Норма (наприклад, 120-160): ");
                        string referenceRange = Console.ReadLine();

                        Console.Write("В нормі? (1 - Так, 0 - Ні): ");
                        bool isNormal = Console.ReadLine() == "1";

                        try
                        {
                            var lab = new LabResult(pId, dId, DateTime.Now, testName, value, unit, referenceRange, isNormal);
                            clinic.MedicalRecords.Add(lab);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        break;
                    }

                case "5": 
                    {
                        Console.WriteLine("\n--- Наявні пацієнти ---");
                        clinic.Patients.DisplayAll();
                        Console.WriteLine("\n--- Наявні лікарі ---");
                        clinic.Doctors.DisplayAll();

                        Console.Write("\nВведіть ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pId))
                        {
                            Console.WriteLine("Помилка: Введено не число.");
                            break;
                        }
                        if (clinic.Patients.FindById(pId) == null)
                        {
                            Console.WriteLine("не знайдено");
                            break;
                        }

                        Console.Write("Введіть ID лікаря: ");
                        if (!int.TryParse(Console.ReadLine(), out int dId))
                        {
                            Console.WriteLine("Помилка: Введено не число.");
                            break;
                        }
                        if (clinic.Doctors.FindById(dId) == null)
                        {
                            Console.WriteLine("не знайдено");
                            break;
                        }

                        Console.Write("Назва ліків: ");
                        string medication = Console.ReadLine();

                        Console.Write("Дозування: ");
                        string dosage = Console.ReadLine();

                        Console.Write("Тривалість у днях: ");
                        if (!int.TryParse(Console.ReadLine(), out int durationDays))
                        {
                            Console.WriteLine("Помилка: Введено не число.");
                            break;
                        }

                        Console.Write("Інструкція з прийому: ");
                        string instructions = Console.ReadLine();

                        try
                        {
                            var prescription = new Prescription(pId, dId, DateTime.Now, medication, dosage, durationDays, instructions);
                            clinic.MedicalRecords.Add(prescription);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        break;
                    }

                case "6": 
                    {
                        Console.Write("Введіть ID пацієнта для перегляду поліморфного списку: ");
                        if (!int.TryParse(Console.ReadLine(), out int pId))
                        {
                            Console.WriteLine("Помилка: Некоректне значення ID.");
                            break;
                        }

                        MedicalRecord[] list = clinic.MedicalRecords.GetByPatient(pId);
                        if (list.Length == 0)
                        {
                            Console.WriteLine("Записів для даного пацієнта немає.");
                            break;
                        }

                        Console.WriteLine($"\n=== Демонстрація поліморфізму для пацієнта #{pId} ===");
                        foreach (MedicalRecord record in list)
                        {
                            Console.WriteLine($"[Тип: {record.GetRecordType()}] -> {record.GetSummary()}");
                        }
                        break;
                    }

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    static void ManageDoctors(Clinic clinic)
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("\n--- Підменю: Лікарі ---");
            Console.WriteLine("1. Показати всіх");
            Console.WriteLine("2. Додати лікаря");
            Console.WriteLine("3. Знайти за ID (TryFindById + out)");
            Console.WriteLine("4. Знайти за спеціальністю");
            Console.WriteLine("5. Видалити лікаря");
            Console.WriteLine("6. Статистика");
            Console.WriteLine("0. Назад у головне меню");
            Console.Write("Оберіть дію: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    clinic.Doctors.DisplayAll();
                    break;

                case "2":
                    try
                    {
                        Console.Write("Ім'я: ");
                        string firstName = Console.ReadLine();

                        Console.Write("Прізвище: ");
                        string lastName = Console.ReadLine();

                        Console.Write("Спеціальність: ");
                        string specialityStr = Console.ReadLine();

                        if (!Enum.TryParse(specialityStr, true, out Speciality speciality))
                        {
                            speciality = Speciality.General;
                        }

                        Console.Write("Номер ліцензії: ");
                        string licenseNumber = Console.ReadLine();

                        Console.Write("Телефон: ");
                        string phone = Console.ReadLine();

                        Console.Write("Початок робочого дня (година): ");
                        if (!int.TryParse(Console.ReadLine(), out int workStartHour))
                        {
                            workStartHour = 8;
                        }

                        Console.Write("Кінець робочого дня (година): ");
                        if (!int.TryParse(Console.ReadLine(), out int workEndHour))
                        {
                            workEndHour = 17;
                        }

                        WorkSchedule schedule = new WorkSchedule(workStartHour, workEndHour);

                        Doctor doctor = new Doctor(
                            firstName,
                            lastName,
                            licenseNumber,  
                            phone,          
                            schedule,      
                            speciality      
                        );

                        clinic.Doctors.Add(doctor);
                        Console.WriteLine("Доктор додано");
                        
                    }
                    catch (ArgumentException  e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    break;
                    

                case "3":
                    Console.Write("Введіть ID: ");
                    int searchId = int.Parse(Console.ReadLine());

                    if (clinic.Doctors.TryFindById(searchId, out Doctor found))
                    {
                        Console.WriteLine(
                            $"Знайдено: [{found.Id}] " +
                            $"{found.FirstName} {found.LastName} | " +
                            $"{found.Speciality} | " +
                            $"{found.LicenseNumber} | " +
                            $"Тел: {found.Phone} | " +
                            $"{found.Schedule}"
                        );
                    }
                    else
                    {
                        Console.WriteLine($"Лікаря з ID {searchId} не знайдено.");
                    }

                    break;

                case "4":
                    Console.Write("Введіть спеціальність (або її частину): ");
                    string query = Console.ReadLine();

                    Doctor[] results;
                    if (Enum.TryParse(query, true, out Speciality specEnum))
                    {
                        results = clinic.Doctors.FindBySpeciality(specEnum);
                    }
                    else
                    {
                        results = clinic.Doctors.FindBySpeciality(query);
                    }

                    if (results.Length > 0)
                    {
                        Console.WriteLine($"Знайдено лікарів ({results.Length}):");

                        for (int i = 0; i < results.Length; i++)
                        {
                            Console.WriteLine(
                                $"[{results[i].Id}] " +
                                $"{results[i].FirstName} " +
                                $"{results[i].LastName} | " +
                                $"{results[i].Speciality} | " +
                                $"{results[i].LicenseNumber}"
                            );
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Лікарів за спеціальністю '{query}' не знайдено.");
                    }

                    break;

                case "5":
                    Console.Write("Введіть ID для видалення: ");
                    int removeId = int.Parse(Console.ReadLine());

                    if (clinic.Doctors.Remove(removeId))
                    {
                        Console.WriteLine($"Лікаря з ID {removeId} успішно видалено.");
                    }
                    else
                    {
                        Console.WriteLine($"Лікаря з ID {removeId} не знайдено.");
                    }

                    break;

                case "6":
                    clinic.Doctors.DisplayStats();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }


    static void ManageAppointments(Clinic clinic)
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("\n--- Підменю: Записи ---");
            Console.WriteLine("1. Показати всі майбутні записи");
            Console.WriteLine("2. Записати пацієнта");
            Console.WriteLine("3. Показати записи пацієнта");
            Console.WriteLine("4. Показати записи лікаря");
            Console.WriteLine("5. Показати записи на дату (рік, місяць, день)");
            Console.WriteLine("6. Скасувати запис");
            Console.WriteLine("7. Завершити запис");
            Console.WriteLine("0. Назад у головне меню");
            Console.Write("Оберіть дію: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    clinic.Appointments.DisplayList(
                        clinic.Appointments.GetUpcoming()
                    );
                    break;

                case "2":
                    try
                    {
                        Console.Write("ID пацієнта: ");
                        int patientId = int.Parse(Console.ReadLine());

                        Console.Write("ID лікаря: ");
                        int doctorId = int.Parse(Console.ReadLine());

                        Console.Write("Дата та час (дд.мм.рррр ГГ:хх): ");
                        DateTime scheduledAt = DateTime.Parse(Console.ReadLine());

                        Console.Write("Тривалість у хвилинах: ");
                        int duration = int.Parse(Console.ReadLine());

                        clinic.Appointments.Book(
                            patientId,
                            doctorId,
                            scheduledAt,
                            duration);
                        
                        Console.WriteLine("Запис успішно створено.");
                        
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Не вдалося створити запис.");
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    break;
                    
                case "3":
                    Console.Write("ID пацієнта: ");
                    int patientSearchId = int.Parse(Console.ReadLine());

                    clinic.Appointments.DisplayList(
                        clinic.Appointments.GetByPatient(patientSearchId)
                    );

                    break;

                case "4":
                    Console.Write("ID лікаря: ");
                    int doctorSearchId = int.Parse(Console.ReadLine());

                    clinic.Appointments.DisplayList(
                        clinic.Appointments.GetByDoctor(doctorSearchId)
                    );

                    break;

                case "5":
                    Console.Write("Рік (рррр): ");
                    int year = int.Parse(Console.ReadLine());

                    Console.Write("Місяць (мм): ");
                    int month = int.Parse(Console.ReadLine());

                    Console.Write("День (дд): ");
                    int day = int.Parse(Console.ReadLine());

                    Appointment[] dateAppointments = clinic.Appointments.GetByDate(year, month, day);
                    clinic.Appointments.DisplayList(dateAppointments);

                    break;

                case "6":
                    Console.Write("ID запису: ");
                    int cancelId = int.Parse(Console.ReadLine());

                    Console.Write("Причина скасування: ");
                    string reason = Console.ReadLine();

                    if (clinic.Appointments.Cancel(cancelId, reason))
                    {
                        Console.WriteLine("Запис скасовано.");
                    }
                    else
                    {
                        Console.WriteLine("Запис не знайдено.");
                    }

                    break;

                case "7":
                    Console.Write("ID запису: ");
                    int completeId = int.Parse(Console.ReadLine());

                    if (clinic.Appointments.Complete(completeId))
                    {
                        Console.WriteLine("Запис завершено.");
                    }
                    else
                    {
                        Console.WriteLine("Не вдалося завершити запис.");
                    }

                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }
}