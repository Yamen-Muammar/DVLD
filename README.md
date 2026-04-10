# Driving Vehicle License Department System (DVLD)
A comprehensive desktop application designed to streamline the processes of the Driving & Vehicle License Department. This system manages the full lifecycle of driving licenses, from initial application and testing to issuance, renewal, and replacement.
  
🛠 Tech Stack  


• Language: C#


• Framework: .NET WinForms

• Database: Microsoft SQL Server

• Data Access: ADO.NET

• Architecture: 3-Tier Architecture (Presentation, Business, and Data Access Layers)

🚀 Key Features


1. Application Management
   
• New Local Driving Licenses: Full workflow for first-time applicants.

• International Licenses: Issuance of international permits based on valid local licenses.

• Renewals & Replacements: Handle license expirations and replacements for lost or damaged cards.

• Detain & Release: Manage the detention and release of licenses with associated fine tracking.

2. People & User Management
   
• Global People Registry: Centralized management of personal details (National ID, contact info, etc.) linked across the system.

• Role-Based Access: Secure user management with specific permissions for different administrative levels.

• Account Controls: Enable/disable user access and manage login credentials.

3. Examination & Testing
   
• Three-Stage Testing: Integration of Vision, Written, and Practical Street tests.

• Test Appointments: Schedule and manage re-takes for failed attempts.

• Result Tracking: Historical logging of all test attempts and outcomes.

4. License Classes
   
• Support for multiple license categories (e.g., Small Motorcycles, Regular Driving, Commercial, Heavy Vehicles) with distinct requirements for age and fees.


🏗 System Architecture

The project follows a strict 3-Tier Architecture to ensure scalability and maintainability:

• Presentation Layer: Handles the UI/UX using Windows Forms.

• Business Layer: Implements core business logic, validation rules, and application workflows.

• Data Access Layer: Manages all direct interactions with the SQL Server database via ADO.NET.
