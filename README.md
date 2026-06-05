# DVLD — Driver & Vehicle Licensing Department

<p align="center">
  <img src="https://img.shields.io/badge/.NET_Framework-4.7.2-512BD4?style=flat-square&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-7.3-239120?style=flat-square&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/WinForms-0078D4?style=flat-square&logo=windows&logoColor=white" />
  <img src="https://img.shields.io/badge/Architecture-3--Tier-333?style=flat-square" />
</p>

A desktop application that manages the complete driver licensing lifecycle — from citizen registration and multi-stage testing, through license issuance, renewal, replacement, and detention enforcement.

Built with a layered architecture across four C# projects, backed by SQL Server, and designed to mirror real government licensing workflows.

---

## What This System Does

DVLD covers seven application types that a licensing office would process daily:

| # | Application Type | What Happens |
|---|---|---|
| 1 | **New Local License** | Person registers → selects license class → passes 3 tests (vision, written, street) → license issued |
| 2 | **International License** | Holder of an active local license applies → international license issued for 1 year |
| 3 | **Renew License** | Expired license holder applies → new license issued with fresh expiration |
| 4 | **Replace (Damaged)** | Active license replaced → old license deactivated, new one issued |
| 5 | **Replace (Lost)** | Same flow as damaged, different fee structure |
| 6 | **Retake Test** | Failed applicant pays retake fee → new test appointment created |
| 7 | **Release Detained License** | Fine paid → detention lifted, license reactivated |

Each type has its own fee, creates its own application record, and is permission-gated via the RBAC system.

---

## Architecture

Four projects, one clear rule: **dependencies only flow downward**.

```
DVLD -Presentation Tier          →  What the user sees and clicks
    │
    ▼
DVLD -Business Tier              →  What the rules say is allowed
    │
    ▼
DVLD -Data Tier                  →  How data is read and written
    │
    ▼
SQL Server                       →  Where data lives


DVLD -Core                       →  Shared by all layers (models, auth, config)
```

**Presentation Tier** — WinForms UI with Guna2 controls. Handles user interaction, form validation, and display. Never talks to the database directly.

**Business Tier** — Service classes that enforce business rules, permission checks, and data validation before any database call. Every public method starts with an authorization check via `Auth.IsAuth()`.

**Data Tier** — Repository classes that own all SQL. Parameterized queries only, no string concatenation. Transactional operations for multi-table inserts (like first-time license issuance, which creates a Driver record + License record atomically).

**Core** — Models, view models, the `Auth` engine, `AppSettings` config reader, and `Global` state. Referenced by all three tiers but owns no logic beyond authorization checks.

---

## Project Structure

```
DVLD/
│
├── DVLD -Core/
│   ├── Models/
│   │   ├── Person.cs              # citizen profile (name, national no, DOB, photo, contact)
│   │   ├── User.cs                # system user (username, hashed password, role, active flag)
│   │   ├── Driver.cs              # created automatically when a person gets their first license
│   │   ├── License.cs             # local driving license
│   │   ├── InternationalLicense.cs
│   │   ├── Application.cs         # base application record (type, status, fees, dates)
│   │   ├── ApplicationType.cs     # configurable type with fee structure
│   │   ├── LocalDrivingLicenseApplication.cs
│   │   ├── LicenseClass.cs        # class 1-7 with validity periods
│   │   ├── Test.cs                # test result (pass/fail, notes)
│   │   ├── TestAppointment.cs     # scheduled test with date, fee, lock status
│   │   ├── TestType.cs            # vision / written / street, each with fees
│   │   ├── DetainedLicense.cs     # detention record with fine tracking
│   │   ├── Role.cs                # Admin, Manager, Employee
│   │   └── Country.cs
│   ├── View Models/               # flat projections for DataGridView binding
│   │   ├── clsPersonView.cs
│   │   ├── clsUserView.cs
│   │   ├── clsDriversView.cs
│   │   ├── clsAppointmentsView.cs
│   │   ├── clsLicenseHistoryView.cs
│   │   ├── clsInternationalLicenseHistory.cs
│   │   └── clsLocalDrivingLicenseApplicationView.cs
│   ├── Auth.cs                    # bitwise permission engine (35+ operations)
│   ├── Global.cs                  # current logged-in user
│   └── AppSettings.cs             # reads from App.config
│
├── DVLD -Data Tier/
│   ├── Repositories/
│   │   ├── PersonRepository.cs
│   │   ├── UserRepository.cs
│   │   ├── DriverRepository.cs
│   │   ├── LicenseRepository.cs
│   │   ├── ApplicationRepository.cs
│   │   ├── ApplicationsTypesRepository.cs
│   │   ├── AppointmentRepository.cs
│   │   ├── TestRepository.cs
│   │   ├── TestTypesRepository.cs
│   │   ├── LicenseClassRepository.cs
│   │   ├── DetainedLicenseRepository.cs
│   │   ├── CountryRepository.cs
│   │   └── RoleRepository.cs
│   └── DataBaseSettings.cs
│
├── DVLD -Business Tier/
│   ├── Services/
│   │   ├── PersonService.cs
│   │   ├── UserService.cs
│   │   ├── DriverService.cs
│   │   ├── LicenseService.cs       # license issuance, renewal, replacement, international
│   │   ├── ApplicationService.cs
│   │   ├── ApplicationsTypeService.cs
│   │   ├── AppointmentService.cs    # test scheduling, retake logic
│   │   ├── TestService.cs           # pass/fail recording, lock management
│   │   ├── TestTypeService.cs
│   │   ├── LicenseClassService.cs
│   │   ├── DetainedLicenseService.cs
│   │   ├── CountryService.cs
│   │   └── RoleService.cs
│   └── clsPasswordHasher.cs        # PBKDF2 hashing utility
│
├── DVLD -Presentation Tier/
│   ├── Forms/
│   │   ├── frmLogin.cs                          # entry point with remember-me
│   │   ├── MainForm.cs                          # MDI parent, permission-gated menu
│   │   ├── PersonForms/                         # people list, add, edit, details
│   │   ├── UserForms/                           # user CRUD, status toggle, password change
│   │   ├── LocalDrivingLicenseForms/            # new application, application list
│   │   ├── InternationalLicenseForms/           # international license management
│   │   ├── License Forms/                       # issuance, license info, history
│   │   ├── DetainedLicenseForms/                # detain, release, manage detained
│   │   ├── DriversForms/                        # driver lookup and listing
│   │   ├── TestsAppointment/                    # vision, written, street test scheduling
│   │   │   ├── frmVisionTestAppointment.cs
│   │   │   ├── WrittenTestFroms/
│   │   │   └── StreetTestForm/
│   │   ├── TestsForms/                          # take test, set result
│   │   ├── Application Types Forms/             # configure app types and fees
│   │   ├── Test Types Forms/                    # configure test types and fees
│   │   ├── frmRenewLicenseApplication.cs
│   │   ├── frmReplaceLicense.cs
│   │   ├── frmInternationalLicenseApplication.cs
│   │   └── frmManagePermissions.cs              # role permission editor
│   ├── Controls/                                # reusable UserControls
│   │   ├── PersonControls/
│   │   ├── UserControls/
│   │   ├── LicenseControls/
│   │   ├── LocalDLApplicationsControls/
│   │   └── SechduleTestsControls/
│   ├── App.config.example
│   └── Program.cs
│
└── .gitignore
```

---

## Technical Highlights

### Bitwise Permission System

Authorization uses a bitwise approach. Each of the 35+ operations is a power of two:

```csharp
public enum enOperations : long
{
    FullPermission      = -10001,
    PeopleList          = 1,
    AddPerson           = 2,
    ShowPersonDetails   = 4,
    DeletePerson        = 16,
    AddUser             = 1024,
    IssueLicense        = 268435456,
    DetainLicense       = 8589934592,
    // ... 35+ operations total
}
```

A role's permission code is the bitwise OR of its allowed operations. Checking authorization is a single AND:

```csharp
private static bool _checkPermission(decimal userRoleCode, decimal OperationCode)
{
    if (userRoleCode == (long)enOperations.FullPermission)
        return true;

    return (Convert.ToInt64(OperationCode) & Convert.ToInt64(userRoleCode))
            == Convert.ToInt64(OperationCode);
}
```

Roles are editable at runtime through the permissions management form — toggle checkboxes per operation, save, and the new permission code takes effect immediately.

### Password Security

Passwords are hashed with PBKDF2 (RFC 2898):

- 16-byte random salt per password
- 32-byte hash output
- 100,000 iterations
- Salt + hash stored as a single Base64 string in SQL Server

Verification re-derives the hash from the entered password using the stored salt, then does a constant-time byte comparison.

### Transactional License Issuance

When someone gets their first license, two things must happen atomically: a `Driver` record is created, and a `License` record is inserted. Both are wrapped in a single `SqlTransaction` — if either fails, everything rolls back.

```csharp
// Simplified flow in LicenseRepository
using (SqlTransaction transaction = connection.BeginTransaction())
{
    int driverID = await _insertNewDriverForTransactionalAsync(driver, transaction, connection);
    int licenseID = await _insertNewLicenseForTransactionalAsync(license, transaction, connection);

    transaction.Commit();
}
```

For subsequent licenses (same person, different class), only the license is inserted — the existing driver record is reused.

### Async Data Access

All database operations are async throughout the stack. The UI stays responsive during database calls:

```csharp
// Presentation → Business → Data, all async
bool isLoginSuccessful = await _userService.Login(username, password, isRememberMeChecked);
```

Repository methods use `OpenAsync()`, `ExecuteReaderAsync()`, `ExecuteScalarAsync()`, and `ReadAsync()` consistently.

---

## Database Entities

```
People
 ├── PersonID (PK)
 ├── FirstName, MiddleName, ThirdName, LastName
 ├── NationalNO (unique)
 ├── Gender, DateOfBirth, Email, Phone, Address
 ├── Country_ID (FK → Countries)
 └── ImageName

Users
 ├── UserID (PK)
 ├── Person_ID (FK → People)
 ├── Username, HashedPassword
 ├── isActive
 └── Role (FK → Roles)

Drivers
 ├── DriverID (PK)
 ├── Person_ID (FK → People)
 ├── CreatedByUser_ID (FK → Users)
 └── CreationDate

Licenses
 ├── LicenseID (PK)
 ├── Driver_ID (FK → Drivers)
 ├── LicenseClass_ID (FK → LicenseClasses)
 ├── IssueDate, ExpirationDate
 ├── IssueReason, Note, isActive
 ├── CreateByUser_ID (FK → Users)
 └── LocalDrivingLicenseApplication_ID (FK)

Applications
 ├── ApplicationID (PK)
 ├── Person_ID (FK → People)
 ├── ApplicationType_ID (FK → ApplicationTypes)
 ├── ApplicationDate, LastStatusDate
 ├── ApplicationStatus (New / Completed / Cancelled)
 ├── PaidFees
 └── CreatedByUser_ID (FK → Users)

LocalDrivingLicenseApplications
 ├── LocalDrivingLicenseApplicationID (PK)
 ├── Application_ID (FK → Applications)
 └── LicenseClass_ID (FK → LicenseClasses)

TestAppointments
 ├── TestAppointmentID (PK)
 ├── TestType_ID (FK → TestTypes)
 ├── LocalDrivingLicenseApplication_ID (FK)
 ├── AppointmentDate, PaidFees
 ├── isLocked
 └── CreatedByUser_ID (FK → Users)

Tests
 ├── TestID (PK)
 ├── TestAppointment_ID (FK → TestAppointments)
 ├── TestResult (pass/fail)
 ├── Notes
 └── CreatedByUser_ID (FK → Users)

DetainedLicenses
 ├── DetainID (PK)
 ├── License_ID (FK → Licenses)
 ├── DetainDate, ReleaseDate
 ├── FineFees
 ├── isReleased
 └── CreatedByUser_ID, ReleasedByUser_ID

InternationalLicenses
 ├── InternationalLicenseID (PK)
 ├── Application_ID (FK → Applications)
 ├── LocalLicense_ID (FK → Licenses)
 ├── IssueDate, ExpirationDate
 ├── IsActive
 └── CreatedBy_ID (FK → Users)

Roles
 ├── RoleID (PK)  →  Admin(1), Manager(2), Employee(3)
 ├── RoleName
 └── RoleCode (decimal — bitwise permission encoding)

LicenseClasses
 ├── LicenseClassID (PK)
 ├── ClassName (e.g., "Small Motorcycle", "Heavy Vehicle")
 └── DefaultValidityLength (years)

ApplicationTypes — fee configuration per application type
TestTypes — vision(1), written(2), street(3) with configurable fees
Countries — nationality reference table
```

---

## Getting Started

### Prerequisites

- Visual Studio 2022+ with **.NET desktop development** workload
- SQL Server 2019+ (Express works)
- .NET Framework 4.7.2

### Setup

**1. Clone**

```bash
git clone https://github.com/YOUR_USERNAME/DVLD.git
```

**2. Database**

Create a database named `DVLD` in SQL Server and run the schema creation script.

**3. Configure**

Copy `App.config.example` → `App.config` in the Presentation Tier project, then fill in:

```xml
<connectionStrings>
  <add name="DefaultConnection"
       connectionString="Server=YOUR_SERVER;Database=DVLD;Integrated Security=True;"
       providerName=".NET Framework Data Provider for SQL Server" />
</connectionStrings>

<appSettings>
  <add key="PersonImagesPath" value="C:\DVLD\PersonImages\" />
  <add key="RememberMeFilePath" value="C:\DVLD\RememberMe.txt" />
</appSettings>
```

**4. Build & Run**

Open `DVLD -Presentation Tier.sln` → Restore NuGet packages → Build → Run.

The login form appears first. After authentication, the main MDI form opens with a permission-gated menu.

---

## Application Flow

The most complex workflow — getting a first-time local license — goes through these steps:

```
1. Register Person           → PersonRepository.AddNewPersonAsync()
2. Create Application        → ApplicationRepository.InsertNewApplication()
3. Schedule Vision Test      → AppointmentRepository.AddNewAppointmentAsync()
4. Take & Pass Vision Test   → TestRepository.AddNewTestAsync()
5. Schedule Written Test     → (same flow, TestType = 2)
6. Take & Pass Written Test
7. Schedule Street Test      → (same flow, TestType = 3)
8. Take & Pass Street Test
9. Issue License             → LicenseRepository.InsertNewLicenseForFirstTimeAsync()
                               (creates Driver + License in a single transaction)
```

At each step, the system checks: does the user have permission? Has the applicant already passed this stage? Is there a duplicate active application? Is there an existing active license for this class?

---

## Screenshots

> Add screenshots here. Recommended captures:
> 1. Login form
> 2. Main menu (MDI parent)
> 3. New Local Driving License Application
> 4. Test scheduling and results
> 5. License information card
> 6. Renew / Replace license form
> 7. Detained licenses management
> 8. Permission management grid

---

## Planned Improvements

- Repository interfaces (`IPersonRepository`, `ILicenseRepository`) for dependency inversion
- Constructor injection to replace `new` instantiation in services
- Unit test coverage with xUnit
- Structured logging with Serilog
- Platform abstraction for file I/O and auth persistence (enabling web/mobile frontends)
- Caching layer and query pagination for large datasets

---

## Tech Stack

| | |
|---|---|
| **Language** | C# 7.3 on .NET Framework 4.7.2 |
| **Database** | SQL Server with ADO.NET |
| **UI** | WinForms + Guna2 UI Framework |
| **Security** | PBKDF2-SHA1 (100k iterations), bitwise RBAC |
| **Config** | System.Configuration / App.config |
| **IDE** | Visual Studio 2022 |

---

## License

MIT — see [LICENSE](LICENSE) for details.
