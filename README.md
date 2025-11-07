MVP Crm system for schools

 

The main tasks that the system solves:

1. Student Management:
• Storage of personal data, learning history and student performance (Personal data, Personal data of Parents, class).

2. Management of teachers and directorates:
• Registration and data management of teachers and management (Personal data).

3. Finance:
• Calculation of additional funding (for example, clubs/sections).
The system generates Excel and Pdf reports (on student debt, monthly income reports from clubs/sections).
 
4. Events, Activities and news:
• Planning and creation of events.
• News compilation.
• Notifications of changes or new events.
The system generates and sends email newsletters (notification of changes to an event/event, announcement of new events/events, newsletter).

5. Reports and analytics:
• Financial statements.
Generate financial reports in PDF format and automatically send them to the director/accounting department by Email directly from the system.
 
6. Online chat:
• A separate chat for the management board members
• Separate chat for teachers
• Shared Chat
 
 
 
A stack for a CRM system for a school
The backend:
1. ASP.NET Core (C#).
2. Entity Framework Core: ORM PostgreSQL.
3. ASP.NET Identity: For authentication and authorization of teachers, members of the management board.
4. SignalR: To implement real-time functions (chats between teachers and management members).
5. EPPlus: For exporting Excel reports.
6. QuestPDF: For exporting Pdf reports.
7. MailKit: For the implementation of Email newsletters on the topic of debt for a circle / section.
8. Hangfire: For managing background tasks such as notifications, email newsletters, or payment reminders.
 
 
Additional Libraries
1. AutoMapper:
• To transform data between the Domain (domain entity) and Entity (database entity) layers.
2. FluentValidation:
• To verify the data on the server side.
3. Serilog:
• For logging purposes.
4. Swagger:
• For documenting and testing the REST API.
 
 
It is planned to implement:
1. Payment integration:
• Connection of payment systems for payment (for example Yandex.Cash register).
2. Notifications (Push/Email/SMS):
• To send notifications to parents or teachers.
3. Analytics:
• Data visualization tools for analysis (for example, Power BI or Google Charts).
4. Deployment:
• Cloud deployment can be performed using Microsoft Azure, AWS, or Google Cloud for CRM accessibility from anywhere.
• Local hosting (if a small school).


Frontend:
1. NextJS with TypeScript
 
 
Desktop application:
1. .Net MAUI
 
 
Mobile application:
1. .Net MAUI
