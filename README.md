# Event Management API  

Welcome to the **Event Management API**!  
This project is designed to help manage events, users, speakers, attendees, and related functionalities with a clean and modern architecture.  

![image](https://github.com/user-attachments/assets/d6446909-9af6-4a67-b897-9f48c626d3e3)  

## 🛠️ Technologies Used  

- **.NET 8** with **Entity Framework Core 8** for configuration and database management.  
- **SQL Server** as the primary relational database.  
- **Redis** for distributed caching to optimize performance and reduce database load.
- **Docker & Docker Compose** for seamless containerized deployment.
- **xUnit, Moq, and FluentAssertions** for a robust unit testing architecture.
- **Microsoft Identity** for user authentication and management.  
- **FluentValidation** for validating inputs.  
- **AutoMapper** for object mapping.  
- **Serilog** for logging.  
- **JWT & Refresh Token** for secure authentication.  
- **MediatR**: Use MediatR to implement CQRS (Command Query Responsibility Segregation) for clean and organized code.  
- **CQRS Pattern** to separate query and command operations.  
- **MailKit & SMTP** for sending email notifications.  
- **Repository Pattern** for data access.  
- **Clean Code & Clean Architecture** for maintainable and scalable design.  
- **XML Documentation** for service classes.  

## 🔑 Key Features  

1. **User Management**:  
   - Register, log in, and manage users with roles like Admin, Speaker, Attendee, and General User.  
   - Custom middlewares to update the last login date and handle authentication (e.g., `AuthFilter`).  

2. **Event Management**:  
   - Create, view, update, and delete events.  
   - Associate speakers, attendees, and categories with events.  

3. **Speaker Management**:  
   - Manage speaker profiles and their association with events.  
   - Add, update, or delete speaker details.  

4. **Attendee & Waitlist Management**:  
   - Users can RSVP for events.  
   - **Waitlist functionality** automatically handles event capacities and triggers email notifications when spots become available.
   - Tracks attendance and engagement.  

5. **Comments**:  
   - Add, update, and delete comments for events.  

6. **Categories**:  
   - Manage categories for events.  

7. **Custom Middleware**:  
   - `ErrorHandlerMiddleware`: Handles API errors and formats responses.  
   - `RateLimitingMiddleware`: Prevents too many requests from a single user.  
   - `UpdateLastLoginMiddleware`: Updates user login timestamps.  

8. **Custom Exception Handling**:  
   - Includes exceptions like `TooManyRequestException`.  

9. **Localization**:  
   - Uses `Localizer` for multi-language support.  

10. **Database**:  
    - Includes seed data for default users and categories (`Seeder`).  
    - Supports views (e.g., `ViewUserEventEngagementSummary`) and stored procedures (e.g., `SP_GetUserEventEngagementDetails`).  

11. **Validation & Pipeline Behavior**:  
    - Implements `ValidationBehavior<TRequest, TResponse>` for request validation.  

## 📝 How to Run  

### Using Docker (Recommended)
1. Clone this repository to your local machine.
2. Ensure Docker Desktop is running.
3. Run the application stack (API, SQL Server, Redis) using Docker Compose:
   ```bash
   docker-compose up --build -d
   ```
4. The API will be available at your configured port (e.g., `http://localhost:5026`), and the database will automatically initialize.

### Manual Setup
1. Clone this repository to your local machine.  
2. Configure the connection string in `appsettings.json` for SQL Server, and ensure you have a Redis instance running to supply the `RedisCacheUrl`.  
3. Run the following commands to apply migrations and seed data:  
   ```bash  
   dotnet ef database update  
   ```  
4. Start the API:  
   ```bash  
   dotnet run  
   ```  

## 🧪 Testing
The project includes a robust testing foundation using xUnit, Moq, and FluentAssertions.
To run the automated test suite, navigate to the solution directory and execute:
```bash
dotnet test
```
This will run comprehensive unit tests evaluating MediatR command/query handlers, service layers, and validators.  

## 🌟 API Highlights  

- **Authentication**:  
  - Secure login with JWT and refresh tokens.  
- **Event Engagement**:  
  - Get insights about event engagement using views and stored procedures.  
- **Logging**:  
  - Track application logs with Serilog.  
- **Validation**:  
  - Ensure valid inputs with FluentValidation.  

## 🛠️ Explore the API with Swagger  

You can explore the API documentation using Swagger.  
🔗 [Swagger Link](https://eventmanagementapi.runasp.net/swagger/index.html)  

### Default User for Testing  
- **Username**: `hamzalafsioui`  
- **Password**: `Mr@Lafsioui2024`  

## 🤝 Contributing  

Feel free to submit issues or pull requests to improve this project. Contributions are always welcome!  
📧 Email: hamza.lafsioui@gmail.com  

## 🔮 What's Next?  

- ✏️ **Implementing more validation rules.**  
- 🔐 **Adding more detailed authorization policies.**  
- 🌍 **Enhancing localization features.**  
- 📚 **Improving documentation.**  

## 🛡️ License  

This project is licensed under the MIT License.  

---
