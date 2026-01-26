# Eventmaster API

Eventmaster is an ASP.NET Core Web API for managing events, users, registrations, and administrative workflows.  
The system supports organizers, participants, and administrators with role-based authorization, real-time notifications, and a layered architecture.

---

## 🏗️ Architecture

The project follows a **Layered Architecture**:

- **API Layer**: Controllers and endpoints.
- **BLL (Business Logic Layer)**: Services, business rules, and ViewModels.
- **DAL (Data Access Layer)**: Repositories and Entity Framework Core entities.

Main layers:
- Controllers
- Services
- Repositories
- Entities
- ViewModels (VMs)
- AutoMapper for mapping

---

## 🚀 Main Features

### 👤 Authentication & Authorization
- Register
- Login with JWT Token
- Role-based access (Admin, Organizer, Registered)

### 🎫 Events Management
- Create, update, delete events (Admin, Organizer)
- Get all events with filters (location, date)
- Approve / Reject events by Admin

### 📌 Saved Events
- Save / Unsave events
- Get saved events for participant

### 📝 Registrations
- Register participant to event
- Get participant registrations
- Real-time notification using SignalR

### 🛠️ Admin Dashboard
- Approve / Reject organizers
- Approve / Reject events
- Make user Admin
- Get pending organizers
- Get pending events
- Get dashboard statistics:
  - Total users
  - Total events
  - Pending events
  - Total participants

### 🔔 Real-time Notifications
- Implemented using **SignalR**
- Notifications for:
  - New participant joined
  - Event approved
  - Event updates

---
## ▶️ How to Run the Project

Follow these steps to run the Eventmaster API locally:

### 1. Prerequisites

Make sure you have installed:

- .NET SDK (version 7.0 or later)
- SQL Server
- Visual Studio 2022 or VS Code

---

### 2. Clone the Repository
bash
git clone <repository-url>

3. Configure Database Connection

In appsettings.json, update the connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=EventmasterDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
---

4. Apply Migrations

Open Package Manager Console and run:

Update-Database

This will create the database and tables
---

5. Run the Project
Run the project using:
Visual Studio: press F5
or
CLI:
dotnet run
---

6. Open Swagger
After running, open:
https://localhost:<port>/swagger
From Swagger you can test all API endpoints.

---
7. Using Protected Endpoints

Call POST /api/Account/login

Copy the returned JWT token

In Swagger, click Authorize

Enter:
Bearer <your_token>
Now you can access secured endpoints.
---


## 🔐 Authentication & Authorization Concept

The project uses **JWT-based Authentication** with **ASP.NET Identity**.

### 1. Registration

Endpoint:
POST /api/Account/register
- Creates a new user in the system.
- The user is assigned a default role (Registered).
- User credentials are stored securely using ASP.NET Identity.

---

### 2. Login

Endpoint:
POST /api/Account/login

- Validates username and password.
- If valid, the system generates a **JWT Token**.
- The token contains claims such as:
  - UserId
  - UserName
  - Role

Example payload:

json
{
  "nameid": "user-id",
  "name": "MH",
  "role": "Registered",
  "exp": 1769269325
}

3. Using JWT Token
The client must send the token in the request header:
Authorization: Bearer <JWT_Token>
This token is required to access protected endpoints.

4. Role-Based Authorization
The system uses Role-based Authorization:
Registered → normal user
Organizer → can create and manage events
Admin → full system control

Example:
[Authorize(Roles = "Admin")]
public IActionResult ApproveEvent(int eventId)
Only users with role Admin can access this endpoint.

5. Security Flow Summary
User registers.
User logs in and receives JWT token.
Client stores the token.
Token is sent with every secured request.

The API validates:

Token signature
Expiration date
User role
Access is granted or denied accordingly.
