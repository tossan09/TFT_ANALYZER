# TFTracker Server

## Project Description

TFTracker is a project created to support the acquisition of complex statistics from the game **TeamFightTactics (TFT)**.  
Currently, it runs as a **self-hosted local app with IIS**, serving its purpose for a single user.  
In the future, the goal is to scale it into a **global application**.

The project is split into two repositories:

- **TFTracker_Client** → frontend (React)
- **TFTracker_Server** → backend (ASP.NET Core)

> ## Note
>
> This repository is for **demonstration and personal use only**.  
> It is not intended for production or wide distribution.

---

## Technologies Used

### Frontend

- **React** → main library for building the interface.
- **React Hooks** (`useState`, `useEffect`) → state management and side effects.
- **Material UI (MUI)** → ready-made components for tables, dropdowns, icons, and styling.
- **Styled Components (MUI)** → customization of table cells and rows.

### Backend (for reference)

- **C# ASP.NET Core** → REST API.
- **ADO.NET with Npgsql** → direct access to PostgreSQL.
- **PostgreSQL** → relational database for stats, sets, patches, comps, and matches.
- **Controllers & Repositories** → separation of concerns (controllers expose endpoints, repositories run SQL queries).
- **DataAnnotations** → basic model validations.
- **Authentication & Authorization with JWT** → secure access control.
