using Microsoft.AspNetCore.Mvc;
using MaPetiteApi.Data;
using MaPetiteApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MaPetiteApi.Controllers
{
    public class TodoItemController : Controller
    {
        public static IEnumerable<Todo> GetTodos(TodoDb db)
        {
            IEnumerable<Todo> items = db.Todos;
            return items;
        }

        public static IResult GetTodosJson(TodoDb db)
        {
            IEnumerable<Todo> items = db.Todos;
            return Results.Ok(items);
        }

        public static IResult GetById(int? id, TodoDb db)
        {
            if (id == null || id == 0)
            {
                return Results.NotFound();
            }

            Todo? todo = db.Todos.Find(id);

            return (todo is Todo) 
                ? Results.Ok(todo)
                : Results.NotFound();

        }

        public static IEnumerable<Todo> GetCompleted(TodoDb db)
        {
            IEnumerable<Todo> items = db.Todos.Where(t => t.IsComplete);
            return items;
        }

        [HttpPost]
        public static IResult PostTodo(Todo todo, TodoDb db)
        {
            db.Todos.Add(todo);
            db.SaveChanges();

            return Results.Created($"/task/{todo.Id}", new {response = "Tache bien ajouté"});
        }

        [HttpPut]
        public static IResult EditTodo(int? id, Todo newtodo, TodoDb db)
        {
            if (id == null || id == 0)
            {
                return Results.NotFound("id non valide!");
            }
            Todo todo = db.Todos.Find(id);


            if (todo is null)
            {
                return Results.NotFound();
            }

            todo.Name = newtodo.Name;
            todo.IsComplete = newtodo.IsComplete;

            db.SaveChangesAsync();

            return Results.NoContent();
        }


        [HttpDelete]
        public static IResult DeletePost(int? id, TodoDb db)
        {
            if (id == null || id == 0)
            {
                return Results.NotFound("id non valide!");
            }

            Todo? todo = db.Todos.Find(id);

            if (todo is Todo)
            {
                db.Todos.Remove(todo);
                db.SaveChanges();
                return Results.Ok("Delete Completed!");
            }

            return Results.NotFound();

        }


    }
}
