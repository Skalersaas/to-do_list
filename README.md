# To-Do List Application

A simple and powerful to-do list app built with *Vue.js* on the front end and *C#* for back-end services. This application provides an intuitive way to manage tasks, with additional features that ensure task tracking is easy and efficient.

## Features
- *Add Tasks*: Users can add new tasks to the to-do list.
- *Edit Tasks*: Ability to modify existing tasks.
- *Delete Tasks*: Users can remove tasks from the list.
- *Mark as Completed*: Tasks can be marked as completed, helping users track their progress.
- *Task Filtering*: View tasks based on their status (completed, pending).
- *Responsive Design*: The app works seamlessly across devices.
- *Authentication*: Secure user login functionality, allowing only authorized users to access their personalized to-do lists.
- *Authorization*: Role-based access control ensures users only interact with tasks assigned to them.
    - Users are issued *JWT (JSON Web Tokens)* upon successful login, which are used to validate requests to the API.
    - These tokens expire after a specified time, improving security. Expired tokens prompt users to log in again.
- *Persistent Storage*: The back-end ensures all tasks are stored securely and persist across user sessions using a relational database.

## Tech Stack
- *Front End*: 
    - Vue.js
    - JavaScript
    - HTML
    - CSS
    - Tailwind CSS (for styling)
    - FontAwesome (for icons)
- *Back End*: 
    - C# (API)
    - Entity Framework (EF) Core: For object-relational mapping (ORM) with the database.
    - PostgreSQL: For relational data storage and retrieval.
    - JWT (for secure token-based authentication)

## Setup and Installation

### Prerequisites
- Node.js
- .NET SDK
- PostgreSQL (set up your database and configure connection strings)

### Installation Steps
1. Clone the repository:
   bash
   git clone https://github.com/Skalersaas/to-do_list.git
   

2. Navigate to the FrontEnd folder:
   bash
   cd FrontEnd
   

3. Install front-end dependencies:
   bash
   npm install
   

4. Start the front-end server:
   bash
   npm run serve
   

5. Navigate to the BackEnd folder, set up your database and update the connection strings in the configuration files:
   bash
   cd BackEnd
   

6. Run the C# server:
   bash
   dotnet run
   

## Usage
- Once both servers are running, open your browser and go to the specified address.
- Add tasks, mark them as completed, or delete them as you wish.
- Filter through tasks to see only completed or pending ones.
- *Authentication and Authorization*: Log in with your credentials to access your personal task list. The app uses JWT tokens to authorize requests and ensure data security.

## External Libraries and Tools
- *Tailwind CSS*: For fast and flexible styling across the app.
- *FontAwesome*: Provides icons used within the user interface.
- *Vue.js*: A progressive JavaScript framework for building user interfaces.
- *C#/.NET*: Backend services providing the API for data storage and retrieval.
- *Entity Framework Core*: Provides an ORM for seamless database interactions.
- *PostgreSQL*: The relational database used to store task data.
- *JWT*: Used for token-based authentication, ensuring secure access to user-specific data.

## Contributing
Feel free to open a pull request or an issue to improve the app.
