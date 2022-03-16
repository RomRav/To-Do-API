using Microsoft.EntityFrameworkCore;
using MaPetiteApi.Data;
using MaPetiteApi.Model;
using MaPetiteApi.Controllers;
using MaPetiteApi.configuration;
using System.Configuration;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<TodoDb>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("maConnection")));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    

    builder.Services.AddCors();


var app = builder.Build();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});


//app.UseCors();


app.MapGet("", () => "Hello World!");

app.MapPost("NewUser", (User newUser, TodoDb db) =>
{
    return UsersController.PostUser(newUser, db);
});

app.MapGet("/TodoList", (TodoDb db) => {
    return TodoItemController.GetTodosJson(db);
});



app.MapGet("Task", (int id, TodoDb db) =>
{
    return TodoItemController.GetById(id, db);
});


app.MapPost("NewTask", (Todo todo, TodoDb db) =>
{
    return TodoItemController.PostTodo(todo, db);
});

app.MapPut("UpdateTask", (int id, Todo inputTodo, TodoDb db) =>
{
    return TodoItemController.EditTodo(id,inputTodo,db);
});

app.MapDelete("DeleteTask", (int id, TodoDb db) =>
{
    return TodoItemController.DeletePost(id, db);
});


app.Run();
