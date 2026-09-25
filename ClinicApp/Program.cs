using System;
using ClinicApp;

class Program
{
    static void Main()
    {
        Clinic clinic = new Clinic("Медична Клініка");

        Doctor doctor1 = new Doctor(
            "Іван",
            "Петренко",
            "Кардіолог",
            "LIC-001",
            "0501111111");

        Doctor doctor2 = new Doctor(
            "Олександр",
            "Шевченко",
            "Хірург",
            "LIC-002",
            "0502222222");

        Doctor doctor3 = new Doctor(
            "Андрій",
            "Мельник",
            "Терапевт",
            "LIC-003",
            "0503333333");

        clinic.Doctors.Add(doctor1);
        clinic.Doctors.Add(doctor2);
        clinic.Doctors.Add(doctor3);

        Console.WriteLine(doctor1);
        Console.WriteLine(doctor2);
        Console.WriteLine(doctor3);

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
            Console.WriteLine("3. Знайти за ID");
            Console.WriteLine("4. Знайти за ім'ям/прізвищем");
            Console.WriteLine("5. Видалити пацієнта");
            Console.WriteLine("6. Статистика");
            Console.WriteLine("0. Назад у головне меню");
            Console.Write("Оберіть дію: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    clinic.Patients.DisplayAll();
                    break;

                case "2":
                    Console.Write("Ім'я: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Прізвище: ");
                    string lastName = Console.ReadLine();

                    Console.Write("Дата народження (дд.мм.рррр): ");
                    DateTime dateOfBirth =
                        DateTime.Parse(Console.ReadLine());

                    Console.Write("Група крові: ");
                    string bloodType = Console.ReadLine();

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
                    break;

                case "3":
                    Console.Write("Введіть ID: ");
                    int searchId = int.Parse(Console.ReadLine());

                    Patient? found =
                        clinic.Patients.FindById(searchId);

                    if (found != null)
                    {
                        string status;

                        if (found.IsAdult)
                        {
                            status = "дорослий";
                        }
                        else
                        {
                            status = "дитина";
                        }

                        Console.WriteLine(
                            $"Знайдено: [{found.Id}] " +
                            $"{found.FirstName} {found.LastName} | " +
                            $"Вік: {found.Age} ({status})"
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Пацієнта з ID {searchId} не знайдено."
                        );
                    }

                    break;

                case "4":
                    Console.Write("Введіть ім'я або прізвище: ");
                    string query = Console.ReadLine();

                    Patient[] results =
                        clinic.Patients.FindByName(query);

                    if (results.Length > 0)
                    {
                        Console.WriteLine(
                            $"Знайдено пацієнтів ({results.Length}):"
                        );

                        foreach (Patient p in results)
                        {
                            Console.WriteLine(
                                $"[{p.Id}] {p.FirstName} {p.LastName}"
                            );
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Пацієнтів за запитом '{query}' не знайдено."
                        );
                    }

                    break;

                case "5":
                    Console.Write("Введіть ID для видалення: ");
                    int removeId = int.Parse(Console.ReadLine());

                    if (clinic.Patients.Remove(removeId))
                    {
                        Console.WriteLine(
                            $"Пацієнта з ID {removeId} успішно видалено."
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Пацієнта з ID {removeId} не знайдено."
                        );
                    }

                    break;

                case "6":
                    clinic.Patients.DisplayStats();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine(
                        "Некоректний вибір. Спробуйте ще раз."
                    );
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
            Console.WriteLine("3. Знайти за ID");
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
                    Console.Write("Ім'я: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Прізвище: ");
                    string lastName = Console.ReadLine();

                    Console.Write("Спеціальність: ");
                    string speciality = Console.ReadLine();

                    Console.Write("Номер ліцензії: ");
                    string licenseNumber = Console.ReadLine();

                    Console.Write("Телефон: ");
                    string phone = Console.ReadLine();

                    Console.Write("Початок робочого дня (година): ");
                    int workStartHour =
                        int.Parse(Console.ReadLine());

                    Console.Write("Кінець робочого дня (година): ");
                    int workEndHour =
                        int.Parse(Console.ReadLine());

                    Doctor doctor = new Doctor(
                        firstName,
                        lastName,
                        speciality,
                        licenseNumber,
                        phone,
                        workStartHour,
                        workEndHour
                    );

                    clinic.Doctors.Add(doctor);
                    break;

                case "3":
                    Console.Write("Введіть ID: ");
                    int searchId = int.Parse(Console.ReadLine());

                    Doctor? found =
                        clinic.Doctors.FindById(searchId);

                    if (found != null)
                    {
                        Console.WriteLine(
                            $"Знайдено: [{found.Id}] " +
                            $"{found.FirstName} {found.LastName} | " +
                            $"{found.Speciality} | " +
                            $"{found.LicenseNumber} | " +
                            $"Тел: {found.Phone} | " +
                            $"{found.WorkSchedule}"
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            "Лікаря з ID " +
                            searchId +
                            " не знайдено."
                        );
                    }

                    break;

                case "4":
                    Console.Write("Введіть спеціальність: ");
                    string query = Console.ReadLine();

                    Doctor[] results =
                        clinic.Doctors.FindBySpeciality(query);

                    if (results.Length > 0)
                    {
                        Console.WriteLine(
                            "Знайдено лікарів (" +
                            results.Length +
                            "):"
                        );

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
                        Console.WriteLine(
                            "Лікарів за спеціальністю '" +
                            query +
                            "' не знайдено."
                        );
                    }

                    break;

                case "5":
                    Console.Write("Введіть ID для видалення: ");
                    int removeId =
                        int.Parse(Console.ReadLine());

                    if (clinic.Doctors.Remove(removeId))
                    {
                        Console.WriteLine(
                            "Лікаря з ID " +
                            removeId +
                            " успішно видалено."
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            "Лікаря з ID " +
                            removeId +
                            " не знайдено."
                        );
                    }

                    break;

                case "6":
                    clinic.Doctors.DisplayStats();
                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine(
                        "Некоректний вибір. Спробуйте ще раз."
                    );
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
            Console.WriteLine("5. Показати записи на дату");
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
                    Console.Write("ID пацієнта: ");
                    int patientId =
                        int.Parse(Console.ReadLine());

                    Console.Write("ID лікаря: ");
                    int doctorId =
                        int.Parse(Console.ReadLine());

                    Console.Write(
                        "Дата та час (дд.мм.рррр ГГ:хх): "
                    );

                    DateTime scheduledAt =
                        DateTime.Parse(Console.ReadLine());

                    Console.Write("Тривалість у хвилинах: ");
                    int duration =
                        int.Parse(Console.ReadLine());

                    if (clinic.Appointments.Book(
                        patientId,
                        doctorId,
                        scheduledAt,
                        duration))
                    {
                        Console.WriteLine(
                            "Запис успішно створено."
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            "Не вдалося створити запис."
                        );
                    }

                    break;

                case "3":
                    Console.Write("ID пацієнта: ");
                    int patientSearchId =
                        int.Parse(Console.ReadLine());

                    clinic.Appointments.DisplayList(
                        clinic.Appointments.GetByPatient(
                            patientSearchId
                        )
                    );

                    break;

                case "4":
                    Console.Write("ID лікаря: ");
                    int doctorSearchId =
                        int.Parse(Console.ReadLine());

                    clinic.Appointments.DisplayList(
                        clinic.Appointments.GetByDoctor(
                            doctorSearchId
                        )
                    );

                    break;

                case "5":
                    Console.Write("Дата (дд.мм.рррр): ");
                    DateTime date =
                        DateTime.Parse(Console.ReadLine());

                    clinic.DisplaySchedule(date);
                    break;

                case "6":
                    Console.Write("ID запису: ");
                    int cancelId =
                        int.Parse(Console.ReadLine());

                    Console.Write("Причина скасування: ");
                    string reason = Console.ReadLine();

                    if (clinic.Appointments.Cancel(
                        cancelId,
                        reason))
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
                    int completeId =
                        int.Parse(Console.ReadLine());

                    if (clinic.Appointments.Complete(completeId))
                    {
                        Console.WriteLine("Запис завершено.");
                    }
                    else
                    {
                        Console.WriteLine(
                            "Не вдалося завершити запис."
                        );
                    }

                    break;

                case "0":
                    back = true;
                    break;

                default:
                    Console.WriteLine(
                        "Некоректний вибір. Спробуйте ще раз."
                    );
                    break;
            }
        }
    }
}