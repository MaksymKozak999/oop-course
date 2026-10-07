using System;
using ClinicApp;

using ClinicApp.Enums;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

class Program
{
    static void Main()
    {
        Clinic clinic = new Clinic("Медична Клініка");
        try
        {
            WorkSchedule w1 = new WorkSchedule(20, 6);
            Patient p1 = new Patient("Maksym", "Петренко",new DateTime(2026, 10, 8),BloodType.ANegative,"0980908307","text@gmail.com");

            Doctor d4 = new Doctor("Test", "Test", "Lic-0000123123", "0980908307", w1, Speciality.Cardiology);

            clinic.Doctors.Add(d4);
            clinic.Patients.Add(p1);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            
        }
        Patient p2 = new Patient("Oleh", "Петренко",new DateTime(2010, 10, 8),BloodType.ANegative,"0980908307","text@gmail.com");
        Doctor doctor1 = new Doctor(
            "Іван",
            "Петренко",
            Speciality.Cardiology,
            "LIC-001",
            "0501111111");

        Doctor doctor2 = new Doctor(
            "Олександр",
            "Шевченко",
            Speciality.Surgery,
            "LIC-002",
            "0502222222");

        Doctor doctor3 = new Doctor(
            "Андрій",
            "Мельник",
            Speciality.Neurology,
            "LIC-003",
            "0503333333");
        

        
        clinic.Patients.Add(p2);
        clinic.Doctors.Add(doctor1);
        clinic.Doctors.Add(doctor2);
        clinic.Doctors.Add(doctor3);

       

        Console.WriteLine("\n=== Демонстрація Перевантаження, Out, ?. та ?? ===");

        Doctor[] cardiologists = clinic.Doctors.FindBySpeciality(Speciality.Cardiology); 
        Doctor[] foundDoctorsStr = clinic.Doctors.FindBySpeciality("кардіо");           
        Console.WriteLine($"Знайдено за enum (Cardiology): {cardiologists.Length}");
        Console.WriteLine($"Знайдено за string ('кардіо'): {foundDoctorsStr.Length}");

        Appointment[] todayAppointments = clinic.Appointments.GetByDate(2026, 5, 10);
        Console.WriteLine($"Записів на 10.05.2026: {todayAppointments.Length}");

        if (clinic.Patients.TryFindById(1, out Patient patientOut))
        {
            Console.WriteLine($"[TryFindById] Знайдено пацієнта: {patientOut.FullName}");
        }
        else
        {
            Console.WriteLine("[TryFindById] Пацієнта з ID 1 не знайдено.");
        }

        if (clinic.Doctors.TryFindById(doctor1.Id, out Doctor doctorOut))
        {
            Console.WriteLine($"[TryFindById] Знайдено лікаря: {doctorOut.FullName}");
        }

        string patientNameNullSafe = clinic.Patients.FindById(99)?.FullName ?? "не знайдено";
        Console.WriteLine($"Результат безперечного пошуку (ID 99): {patientNameNullSafe}");
        Console.WriteLine("===================================================\n");

        Console.WriteLine("\n=== Демонстрація WorkSchedule (Value Type) ===");
        WorkSchedule morning = new WorkSchedule(8, 12);
        WorkSchedule copy = morning;
        copy = new WorkSchedule(9, 15);

        Console.WriteLine($"Оригінал (morning): {morning}"); 
        Console.WriteLine($"Копія (copy):       {copy}");    
        Console.WriteLine("===============================================\n");

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n=== ClinicApp: Головне меню ===");
            Console.WriteLine("1. Пацієнти");
            Console.WriteLine("2. Лікарі");
            Console.WriteLine("3. Записи");
            Console.WriteLine("4. Розклад");
            Console.WriteLine("5. Звіт");
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
                    Console.Write("Введіть дату (дд.мм.рррр): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());

                    clinic.DisplaySchedule(date);
                    break;

                case "5":
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
                    int searchId = int.Parse(Console.ReadLine());

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
                    catch (ArgumentOutOfRangeException e)
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