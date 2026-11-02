using HabitTracker.Controllers;
using HabitTracker.Data;
using HabitTracker.Repositories;
using HabitTracker.Services;
using HabitTracker.Views;

string connectionString = "Data Source=habittracker.db;Foreign Keys=True";
var databaseInitializer = new DatabaseInitializer(connectionString);
databaseInitializer.Initialize();

IHabitRepository habitRepository = new HabitRepository(connectionString);
var service = new HabitTrackingService(habitRepository);
var view = new ConsoleView();
var controller = new ApplicationController(service, view);

controller.Run();
