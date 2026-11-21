using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;
using ClinicApp.Events;

namespace ClinicApp;
internal class Clinic
{
    private string _name;
    private PatientManager _patients;
    private DoctorManager _doctors;
    private AppointmentManager _appointments;
    private MedicalRecordManager _medicalRecords;
	private WaitingQueue<Patient> _waitingRoom;
    private TreatmentPlanManager _treatmentPlans;
    private ClinicLogger _logger;
    private ClinicExporter _exporter;
    private SessionManager _session;
    public BillingManager Billing { get; }

    public string Name => _name;
    public PatientManager Patients => _patients;
    public DoctorManager Doctors => _doctors;
    public AppointmentManager Appointments => _appointments;
    public MedicalRecordManager MedicalRecords => _medicalRecords;
	public WaitingQueue<Patient> WaitingRoom => _waitingRoom;
    public AnalyticsManager Analytics { get; }
    public TreatmentPlanManager TreatmentPlans => _treatmentPlans;
    public ClinicLogger Logger => _logger;
    public ClinicExporter  Exporter=> _exporter;
    public SessionManager Session => _session;
    public PatientPassportWriter Passport { get; }
    public SessionEventTracker Tracker { get; }
    public ReportManager Reports { get; }
    

    public Clinic(string name)
    {
        _name = name;
        _patients = new PatientManager();
        _doctors = new DoctorManager();
        _appointments = new AppointmentManager(_patients, _doctors);
        _medicalRecords = new MedicalRecordManager();
		_waitingRoom = new WaitingQueue<Patient>();
        Billing = new BillingManager(Appointments);
        Analytics = new AnalyticsManager(Appointments, Doctors, Patients);
        _treatmentPlans = new TreatmentPlanManager();
        _logger = new ClinicLogger();
        _exporter = new ClinicExporter(this);
        _session = new SessionManager();
        Passport = new PatientPassportWriter(this);
        Tracker = new SessionEventTracker(this);
        Reports = new ReportManager(_appointments, _doctors,_patients);
        SubscribeEvents();
        
        
    }
    private void SubscribeEvents()
    {
        _patients.PatientAdded += _logger.OnPatientAdded;

        _appointments.AppointmentBooked += _logger.OnAppointmentBooked;
        _appointments.AppointmentCancelled += _logger.OnAppointmentCancelled;
        _appointments.AppointmentCompleted += _logger.OnAppointmentCompleted;
        _appointments.UrgentAppointmentBooked += _logger.OnUrgentAppointmentBooked;

        Billing.PaymentReceived += _logger.OnPaymentReceived;
        _treatmentPlans.PlanCompleted += _logger.OnPlanCompleted;

        Patients.PatientAdded += Passport.PatientAdded;
        Appointments.AppointmentCompleted += Passport.AppointmentCompleted;
        TreatmentPlans.PlanCompleted += Passport.PlanCompleted;

        Patients.PatientAdded += Tracker.PatientAdded;
        Appointments.AppointmentBooked += Tracker.AppointmentBooked;
        Appointments.UrgentAppointmentBooked += Tracker.UrgentBook;
        Appointments.AppointmentCancelled += Tracker.AppointmentCancelled;
        Appointments.AppointmentCompleted += Tracker.AppointmentCompleted;
        Billing.PaymentReceived += Tracker.PaymentReceived;
        TreatmentPlans.PlanCompleted += Tracker.PlanCompleted;

    }
    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine($"=== Розклад на {date:dd.MM.yyyy} ===");

        Appointment[] appointmentsForDate =
            Appointments.GetByDate(date);

        Appointments.DisplayList(appointmentsForDate);
    }
    public void GenerateReport()
    {
        int upcomingAppointments =
            Appointments.GetUpcoming().Length;

        string output =
            "╔══════════════════════════════════════════════╗\n" +
           $"║  Звіт — {Name,-39}║\n" +
            "╠══════════════════════════════════════════════╣\n" +
           $"║  Пацієнтів:          {Patients.Count,-20}║\n" +
           $"║  Лікарів:            {Doctors.Count,-20}║\n" +
           $"║  Майбутніх записів:  {upcomingAppointments,-20}║\n" +
            "╠══════════════════════════════════════════════╣\n" +
            "║  Навантаження лікарів (майбутні записи):     ║\n";

        for (int i = 1; i <= Doctors.Count; i++)
        {
            Appointment[] doctorAppointments =
                Appointments.GetByDoctor(i);

            int upcomingCount = 0;

            foreach (Appointment appointment in doctorAppointments)
            {
                if (appointment.IsUpcoming)
                    upcomingCount++;
            }

            Doctor? doctor = Doctors.FindById(i);

            if (doctor != null)
            {
                output += $"║  {doctor.FullName,-20}: {upcomingCount,3} записів        ║\n";
            }
        }

        output +=
            "╚══════════════════════════════════════════════╝";

        Console.WriteLine(output);
    }
}