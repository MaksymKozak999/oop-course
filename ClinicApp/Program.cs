using System;
using ClinicApp;

class Program
{
    static void Main()
    {
        PatientManager patientManager = new PatientManager();
        DoctorManager doctorManager = new DoctorManager();
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n=== ClinicApp: Головне меню ===");
            Console.WriteLine("1. Пацієнти");
            Console.WriteLine("2. Лікарі");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть розділ: ");

            string choice = Console.ReadLine();


            switch(choice)
            {
                case "1":
                    ManagePatients(patientManager);
                    break;
                case "2":
                    ManageDoctors(doctorManager); 
                    break;
                case "0":
                    exit = true;
                    break;

            }
            
        }

        static void ManagePatients(PatientManager manager)
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
                        manager.DisplayAll();
                        break;

                    case "2":
                        Console.Write("Ім'я: ");
                        string firstName = Console.ReadLine() ?? "";

                        Console.Write("Прізвище: ");
                        string lastName = Console.ReadLine() ?? "";

                        Console.Write("Дата народження (дд.мм.рррр): ");
                        DateTime dateOfBirth = DateTime.Parse(Console.ReadLine() ?? "");

                        Console.Write("Група крові: ");
                        string bloodType = Console.ReadLine() ?? "";

                        Console.Write("Телефон: ");
                        string phone = Console.ReadLine() ?? "";

                        Console.Write("Email: ");
                        string email = Console.ReadLine() ?? "";

                        Patient patient = new Patient(
                            firstName,
                            lastName,
                            dateOfBirth,
                            bloodType,
                            phone,
                            email
                        );

                        manager.Add(patient);
                        break;

                    case "3":
                        Console.Write("Введіть ID: ");
                        int searchId = int.Parse(Console.ReadLine());
                        Patient found = manager.FindById(searchId);

                        if (found != null)
                        {
                            string status = found.IsAdult ? "дорослий" : "дитина";
                            Console.WriteLine("Знайдено: [" + found.Id + "] " + found.FirstName + " " + found.LastName + " | Вік: " + found.Age + " (" + status + ")");
                        }
                        else
                        {
                            Console.WriteLine("Пацієнта з ID " + searchId + " не знайдено.");
                        }
                        break;

                    case "4":
                        Console.Write("Введіть ім'я або прізвище: ");
                        string query = Console.ReadLine();
                        Patient[] results = manager.FindByName(query);

                        if (results.Length > 0)
                        {
                            Console.WriteLine("Знайдено пацієнтів (" + results.Length + "):");
                            for (int i = 0; i < results.Length; i++)
                            {
                                Console.WriteLine("[" + results[i].Id + "] " + results[i].FirstName + " " + results[i].LastName);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Пацієнтів за запитом '" + query + "' не знайдено.");
                        }
                        break;

                    case "5":
                        Console.Write("Введіть ID для видалення: ");
                        int removeId = int.Parse(Console.ReadLine());

                        if (manager.Remove(removeId))
                        {
                            Console.WriteLine("Пацієнта з ID " + removeId + " успішно видалено.");
                        }
                        else
                        {
                            Console.WriteLine("Пацієнта з ID " + removeId + " не знайдено.");
                        }
                        break;

                    case "6":
                        manager.DisplayStats();
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

        static void ManageDoctors(DoctorManager manager)
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

                string input = Console.ReadLine() ?? "";

                switch (input)
                {
                    case "1":
                        manager.DisplayAll();
                        break;

                    case "2":
                        Console.Write("Ім'я: ");
                        string firstName = Console.ReadLine() ?? "";

                        Console.Write("Прізвище: ");
                        string lastName = Console.ReadLine() ?? "";

                        Console.Write("Спеціальність: ");
                        string speciality = Console.ReadLine() ?? "";

                        Console.Write("Номер ліцензії: ");
                        string licenseNumber = Console.ReadLine() ?? "";

                        Console.Write("Телефон: ");
                        string phone = Console.ReadLine() ?? "";

                        Console.Write("Початок робочого дня (година): ");
                        int workStartHour = int.Parse(Console.ReadLine() ?? "");

                        Console.Write("Кінець робочого дня (година): ");
                        int workEndHour = int.Parse(Console.ReadLine() ?? "");

                        Doctor doctor = new Doctor(
                            firstName,
                            lastName,
                            speciality,
                            licenseNumber,
                            phone,
                            workStartHour,
                            workEndHour
                        );

                        manager.Add(doctor);
                        break;

                    case "3":
                        Console.Write("Введіть ID: ");
                        int searchId = int.Parse(Console.ReadLine() ?? "");

                        Doctor found = manager.FindById(searchId);

                        if (found != null)
                        {
                            Console.WriteLine(
                                $"Знайдено: [{found.Id}] {found.FirstName} {found.LastName} | " +
                                $"{found.Speciality} | {found.LicenseNumber} | " +
                                $"Тел: {found.Phone} | " +
                                $"{found.WorkSchedule}"
                            );
                        }
                        else
                        {
                            Console.WriteLine(
                                "Лікаря з ID " + searchId + " не знайдено."
                            );
                        }
                        break;

                    case "4":
                        Console.Write("Введіть спеціальність: ");
                        string query = Console.ReadLine() ?? "";

                        Doctor[] results = manager.FindBySpeciality(query);

                        if (results.Length > 0)
                        {
                            Console.WriteLine(
                                "Знайдено лікарів (" + results.Length + "):"
                            );

                            for (int i = 0; i < results.Length; i++)
                            {
                                Console.WriteLine(
                                    $"[{results[i].Id}] " +
                                    $"{results[i].FirstName} {results[i].LastName} | " +
                                    $"{results[i].Speciality} | " +
                                    $"{results[i].LicenseNumber}"
                                );
                            }
                        }
                        else
                        {
                            Console.WriteLine(
                                "Лікарів за спеціальністю '" +
                                query + "' не знайдено."
                            );
                        }
                        break;

                    case "5":
                        Console.Write("Введіть ID для видалення: ");
                        int removeId = int.Parse(Console.ReadLine() ?? "");

                        if (manager.Remove(removeId))
                        {
                            Console.WriteLine(
                                "Лікаря з ID " + removeId +
                                " успішно видалено."
                            );
                        }
                        else
                        {
                            Console.WriteLine(
                                "Лікаря з ID " + removeId +
                                " не знайдено."
                            );
                        }
                        break;

                    case "6":
                        manager.DisplayStats();
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
}