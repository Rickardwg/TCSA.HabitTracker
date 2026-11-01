using HabitTracker.Data;

string connectionString = "Data Source=habittracker.db;Foreign Keys=True";
var databaseInitializer = new DatabaseInitializer(connectionString);
databaseInitializer.Initialize();
