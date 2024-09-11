# Task Management System

## Project setup
To launch project locally you need .net 8, SQL Server Express LocalDB, ef core 8

## Api docs

- POST /users/register: To register a new user.
- POST /users/login: To authenticate a user and return a JWT.
- POST /tasks: To create a new task (authenticated).
- GET /tasks?DueDate= &Status= &Priority= &SortBy= &IsDescending= &PageNumber= &PageSize= : To retrieve a list of tasks for the authenticated user, with optional filters (e.g., status, due date, priority).
- GET /tasks/{id}: To retrieve the details of a specific task by its ID (authenticated).
- PUT /tasks/{id}: To update an existing task (authenticated).
- DELETE /tasks/{id}: To delete a specific task by its ID (authenticated).

## architecture and design choices

3 layer architecture
in data access laye used repository and unit of work patterns,
throughout in app used dependecy injection and SOLID principles
