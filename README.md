<p align="center">
  <img src="https://img.shields.io/badge/C%23-.NET_4.7.2-512BD4?style=for-the-badge&logo=csharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/SQL_Server-Database-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/WinForms-Desktop_UI-0078D4?style=for-the-badge&logo=windows&logoColor=white" alt="WinForms" />
  <img src="https://img.shields.io/badge/Architecture-3--Tier-2E86C1?style=for-the-badge&logo=buffer&logoColor=white" alt="3-Tier" />
  <img src="https://img.shields.io/badge/License-MIT-green?style=for-the-badge" alt="License" />
</p>

<h1 align="center">🚗 DVLD — Driver & Vehicle Licensing Department</h1>

<p align="center">
  <strong>A full-featured, enterprise-grade desktop application for managing driver licensing workflows — from application intake and test scheduling through license issuance, renewal, replacement, and detention.</strong>
</p>

<p align="center">
  Built with a clean <strong>3-Tier Architecture</strong> in C# / .NET, backed by SQL Server, and designed for real-world government licensing operations.
</p>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Key Features](#-key-features)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Database Schema](#-database-schema)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Configuration](#-configuration)
- [Security](#-security)
- [Screenshots](#-screenshots)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)
- [License](#-license)

---

## 🔍 Overview

**DVLD** is a comprehensive Driver and Vehicle Licensing Department management system that digitizes the entire driver licensing lifecycle. It handles everything a government licensing office needs — person registration, application processing, multi-stage test scheduling, license issuance across multiple classes, and ongoing license management operations like renewals, replacements, and detentions.

The application follows a strict **3-Tier (N-Layer) Architecture** to enforce separation of concerns, making the codebase maintainable, testable, and extensible. Every operation flows through the proper layers: **Presentation → Business → Data**, with a shared **Core** layer for models and cross-cutting concerns.

---

## ✨ Key Features

### 👤 People & User Management
- Full CRUD for person records (name, national number, contact info, photo, country)
- User account creation linked to person records
- Activate / deactivate user accounts
- Change password with secure hashing (PBKDF2 + salt)
- "Remember Me" login persistence

### 📝 Application Processing
- **Local Driving License Applications** — full workflow from creation to license issuance
- **International License Applications** — issue international licenses based on active local licenses
- **Renewal Applications** — renew expired licenses with new expiration dates
- **Replacement Applications** — replace lost or damaged licenses
- **Retake Test Applications** — schedule retakes after failed test attempts
- Configurable application types with custom fee structures

### 🧪 Test Scheduling & Management
- Three-stage testing pipeline: **Vision Test → Written Test → Street Test**
- Schedule, edit, and cancel test appointments
- Record pass/fail results with automatic test locking
- Track retake history and trial counts per applicant
- Enforce test progression rules (must pass each stage sequentially)

### 🪪 License Lifecycle
- Issue first-time licenses after all tests are passed
- Support for **multiple license classes** (e.g., Class 1 – Small Motorcycle through Class 7 – Heavy Vehicle)
- Configurable validity periods per license class
- Full license history tracking per person/driver
- Active license detection to prevent duplicate issuance

### 🔒 License Detention & Release
- Detain licenses with fine fee tracking
- Release detained licenses with proper application workflow
- Manage all detained licenses from a centralized dashboard

### 🛡️ Role-Based Access Control (RBAC)
- Granular, bitwise permission system with **35+ individual operations**
- Roles: Admin (full permission), and custom roles with configurable permissions
- Real-time permission editing via a management UI
- Every operation in the system is permission-gated

### 📊 Drivers Management
- Automatic driver record creation upon first license issuance
- Driver lookup by Person ID or Driver ID
- View active license count per driver
- Centralized drivers list with search and filter

---

## 🏗️ Architecture

The application is built on a **strict 3-Tier Architecture** with a shared Core library:

```
┌──────────────────────────────────────────────────────────────┐
│                    PRESENTATION TIER                         │
│              (WinForms + Guna2 UI Framework)                 │
│                                                              │
│   Forms ─── Controls ─── Event Handlers ─── UI Validation    │
└────────────────────────────┬─────────────────────────────────┘
                             │  References
                             ▼
┌──────────────────────────────────────────────────────────────┐
│                     BUSINESS TIER                            │
│                  (Services / BLL Layer)                       │
│                                                              │
│  Business Rules ─── Validation ─── Auth Checks ─── Workflow  │
└────────────────────────────┬─────────────────────────────────┘
                             │  References
                             ▼
┌──────────────────────────────────────────────────────────────┐
│                       DATA TIER                              │
│                 (Repositories / DAL Layer)                    │
│                                                              │
│  SQL Queries ─── Parameterized Commands ─── Transactions     │
└────────────────────────────┬─────────────────────────────────┘
                             │  ADO.NET
                             ▼
┌──────────────────────────────────────────────────────────────┐
│                      SQL SERVER DB                           │
│                                                              │
│  Tables ─── Views ─── Stored Procedures ─── Constraints      │
└──────────────────────────────────────────────────────────────┘

              ┌────────────────────────┐
              │       CORE LAYER       │
              │ (Shared across all)    │
              │                        │
              │  Models ─ ViewModels   │
              │  Auth ─ AppSettings    │
              │  Global State          │
              └────────────────────────┘
```

### Why 3-Tier?

- **Separation of Concerns** — each layer has a single responsibility
- **Maintainability** — change the database without touching UI code, and vice versa
- **Testability** — business logic can be tested independently from the UI and database
- **Scalability** — layers can be independently scaled or swapped (e.g., replace WinForms with a web frontend)

---

## 🛠️ Tech Stack

| Category | Technology |
|---|---|
| **Language** | C# 7.3 |
| **Runtime** | .NET Framework 4.7.2 |
| **UI Framework** | Windows Forms + Guna2 UI |
| **Database** | Microsoft SQL Server |
| **Data Access** | ADO.NET (SqlConnection, SqlCommand, SqlDataReader) |
| **Password Security** | PBKDF2 with RFC 2898 (100,000 iterations + 16-byte salt) |
| **Configuration** | System.Configuration / App.config |
| **IDE** | Visual Studio 2022+ |

---

## 🗄️ Database Schema

The system manages the following core entities:

```
People ──────────┐
                  ├──── Users (login credentials + roles)
                  └──── Drivers (created on first license issuance)
                             │
                             ├── Licenses
                             │     ├── Local Driving Licenses
                             │     └── International Licenses
                             │
                             └── DetainedLicenses

Applications ────┬── LocalDrivingLicenseApplications
                 ├── InternationalLicenseApplications
                 ├── RenewLicenseApplications
                 └── ReplaceLicenseApplications

TestAppointments ─── Tests (Vision / Written / Street)

LicenseClasses ──── (Class 1–7 with validity periods)
Countries ───────── (Referenced by People)
Roles ───────────── (RBAC with bitwise permission codes)
ApplicationTypes ── (Configurable types with fee structures)
TestTypes ───────── (Configurable test types with fees)
```

### Key Design Decisions

- **Bitwise RBAC** — permissions are stored as a single `decimal` code per role, checked via bitwise AND operations for lightning-fast authorization
- **Transactional License Issuance** — first-time license creation wraps driver creation + license insertion in a single SQL transaction to ensure data consistency
- **Explicit FK Resolution** — all foreign key logic is handled in the repository layer (no database triggers), keeping insertion logic visible and debuggable
- **View Models for UI** — dedicated view model classes (`clsDriversView`, `clsPersonView`, etc.) project only the columns needed for list/grid displays

---

## 📁 Project Structure

```
DVLD/
├── DVLD -Core/                          # Shared Core Layer
│   ├── Models/
│   │   ├── Person.cs
│   │   ├── User.cs
│   │   ├── Driver.cs
│   │   ├── License.cs
│   │   ├── InternationalLicense.cs
│   │   ├── Application.cs
│   │   ├── ApplicationType.cs
│   │   ├── LicenseClass.cs
│   │   ├── Test.cs
│   │   ├── TestAppointment.cs
│   │   ├── TestType.cs
│   │   ├── DetainedLicense.cs
│   │   ├── Role.cs
│   │   ├── Country.cs
│   │   └── LocalDrivingLicenseApplication.cs
│   ├── View Models/
│   │   ├── clsPersonView.cs
│   │   ├── clsUserView.cs
│   │   ├── clsDriversView.cs
│   │   ├── clsAppointmentsView.cs
│   │   ├── clsLicenseHistoryView.cs
│   │   ├── clsInternationalLicenseHistory.cs
│   │   └── clsLocalDrivingLicenseApplicationView.cs
│   ├── Auth.cs                          # Bitwise permission engine
│   ├── Global.cs                        # Global state (current user)
│   └── AppSettings.cs                   # Centralized config access
│
├── DVLD -Data Tier/                     # Data Access Layer (DAL)
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
│   └── DataBaseSettings.cs              # Connection string resolver
│
├── DVLD -Business Tier/                 # Business Logic Layer (BLL)
│   ├── Services/
│   │   ├── PersonService.cs
│   │   ├── UserService.cs
│   │   ├── DriverService.cs
│   │   ├── LicenseService.cs
│   │   ├── ApplicationService.cs
│   │   ├── ApplicationsTypeService.cs
│   │   ├── AppointmentService.cs
│   │   ├── TestService.cs
│   │   ├── TestTypeService.cs
│   │   ├── LicenseClassService.cs
│   │   ├── DetainedLicenseService.cs
│   │   ├── CountryService.cs
│   │   └── RoleService.cs
│   └── clsPasswordHasher.cs            # PBKDF2 password hashing
│
├── DVLD -Presentation Tier/            # UI Layer (WinForms)
│   ├── Forms/
│   │   ├── frmLogin.cs                  # Login with Remember Me
│   │   ├── MainForm.cs                  # MDI parent with menu
│   │   ├── PersonForms/                 # People list, add/edit, details
│   │   ├── UserForms/                   # User management
│   │   ├── LocalDrivingLicenseForms/    # LDL application workflow
│   │   ├── InternationalLicenseForms/   # International license mgmt
│   │   ├── License Forms/               # License info, history, issuance
│   │   ├── DetainedLicenseForms/        # Detain & release workflows
│   │   ├── DriversForms/                # Driver management
│   │   ├── TestsAppointment/            # Vision / Written / Street tests
│   │   ├── Application Types Forms/     # App type configuration
│   │   └── Test Types Forms/            # Test type configuration
│   ├── Controls/                        # Reusable UserControls
│   │   ├── PersonControls/
│   │   ├── UserControls/
│   │   ├── LicenseControls/
│   │   ├── LocalDLApplicationsControls/
│   │   └── ScheduleTestsControls/
│   ├── App.config.example               # Configuration template
│   └── Program.cs                       # Entry point
│
└── .gitignore
```

---

## 🚀 Getting Started

### Prerequisites

- **Visual Studio 2022** (or later) with the **.NET desktop development** workload
- **SQL Server 2019+** (Express edition works fine)
- **.NET Framework 4.7.2** runtime

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/YOUR_USERNAME/DVLD.git
   cd DVLD
   ```

2. **Set up the database**

   - Open SQL Server Management Studio (SSMS)
   - Create a new database named `DVLD`
   - Execute the database creation script (if included) or create the tables matching the schema described above

3. **Configure the connection string**

   - Navigate to `DVLD -Presentation Tier/`
   - Copy `App.config.example` to `App.config`
   - Update the connection string with your SQL Server instance:

   ```xml
   <connectionStrings>
     <add name="DefaultConnection"
          connectionString="Server=YOUR_SERVER;Database=DVLD;Integrated Security=True;"
          providerName=".NET Framework Data Provider for SQL Server" />
   </connectionStrings>
   ```

4. **Configure file paths**

   In the same `App.config`, set the image storage and remember-me file paths:

   ```xml
   <appSettings>
     <add key="PersonImagesPath" value="C:\DVLD\PersonImages\" />
     <add key="RememberMeFilePath" value="C:\DVLD\RememberMe.txt" />
   </appSettings>
   ```

5. **Open and build the solution**

   - Open `DVLD -Presentation Tier/DVLD -Presentation Tier.sln` in Visual Studio
   - Restore NuGet packages (Guna2 UI)
   - Build the solution (`Ctrl + Shift + B`)
   - Run the project (`F5`)

---

## ⚙️ Configuration

The application uses `App.config` for all configurable settings. A template is provided as `App.config.example`:

| Setting | Description |
|---|---|
| `DefaultConnection` | SQL Server connection string |
| `PersonImagesPath` | Local directory for storing person profile photos |
| `RememberMeFilePath` | File path for persisting login credentials |

These settings are read at runtime via the `AppSettings` class in the Core layer using `ConfigurationManager`.

---

## 🔐 Security

### Password Hashing

All passwords are hashed using **PBKDF2** (RFC 2898) with the following parameters:

- **Salt size**: 16 bytes (randomly generated per password)
- **Hash size**: 32 bytes
- **Iterations**: 100,000
- **Output**: Base64 string combining salt + hash for database storage

Plaintext passwords are never stored or logged.

### Authorization

The system implements a **bitwise Role-Based Access Control (RBAC)** system:

- Each operation (e.g., `AddPerson`, `IssueLicense`, `DetainLicense`) is assigned a unique power-of-2 code
- A role's permission set is the bitwise OR of all its allowed operations
- Authorization checks use bitwise AND: `(roleCode & operationCode) == operationCode`
- An `Admin` role with code `-10001` bypasses all checks

This approach allows **35+ granular permissions** to be stored in a single numeric field and checked in O(1) time.

---

## 📸 Screenshots

> *Add screenshots of the Login form, Main Menu, License Application workflow, Test Scheduling, and License Information screens here.*

<!--
Suggested screenshots:
1. Login Screen (with Guna2 UI styling)
2. Main Menu (MDI parent with menu strip)
3. People List with Filters
4. New Local Driving License Application
5. Test Scheduling (Vision/Written/Street)
6. License Information Card
7. Renew / Replace License Form
8. Detained Licenses Management
9. Permission Management Grid
-->

---

## 🗺️ Roadmap

- [ ] **Repository Interfaces** — introduce `IPersonRepository`, `ILicenseRepository`, etc. for dependency inversion
- [ ] **Dependency Injection** — replace direct `new` instantiation in services with constructor injection
- [ ] **Unit Tests** — add xUnit/NUnit test coverage for the Business Tier
- [ ] **Structured Logging** — integrate Serilog for centralized, searchable logs
- [ ] **Cross-Platform Support** — abstract platform-specific concerns (file I/O, auth persistence) to enable web and mobile frontends
- [ ] **Async Everywhere** — ensure all data access is fully async/await
- [ ] **Caching & Pagination** — improve performance for large datasets
- [ ] **Azure Blob Storage** — optional cloud storage for person images

---

## 🤝 Contributing

Contributions are welcome! If you'd like to contribute:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add your feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

Please follow the existing code style and architecture patterns (layered approach, async/await, parameterized queries).

---

## 📄 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

<p align="center">
  <sub>Built with ❤️ as a full-stack portfolio project demonstrating layered architecture, secure authentication, and real-world business workflow implementation in C# / .NET.</sub>
</p>
