#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaPetiteApi.Data;
using MaPetiteApi.Model;

namespace MaPetiteApi.Controllers
{
    
    public class UsersController : ControllerBase
    {
        public static IEnumerable<User> GetUsers(TodoDb db)
        {
            IEnumerable<User> items = db.Users;
            return items;
        }

        public static IResult GetUsersJson(TodoDb db)
        {
            IEnumerable<User> items = db.Users;
            return Results.Ok(items);
        }

        public static IResult GetById(int? id, TodoDb db)
        {
            if (id == null || id == 0)
            {
                return Results.NotFound();
            }

            User? user = db.Users.Find(id);

            return (user is User)
                ? Results.Ok(user)
                : Results.NotFound();
        }

        //public static IEnumerable<Todo> GetCompleted(TodoDb db)
        //{
        //    IEnumerable<Todo> items = db.Todos.Where(t => t.IsComplete);
        //    return items;
        //}

        [HttpPost]
        public static IResult PostUser(User user, TodoDb db)
        {
            db.Users.Add(user);
            db.SaveChanges();

            return Results.Created($"/user/{user.Id}", new { response = "utilisateur bien ajouté" });
        }

        [HttpPut]
        public static IResult EditUser(int? id, User newuser, TodoDb db)
        {
            if (id == null || id == 0)
            {
                return Results.NotFound("id non valide!");
            }
            User user = db.Users.Find(id);


            if (user is null)
            {
                return Results.NotFound();
            }

            user.Email = newuser.Email;
            user.Password = newuser.Password;

            db.SaveChangesAsync();

            return Results.NoContent();
        }


        [HttpDelete]
        public static IResult DeleteUser(int? id, TodoDb db)
        {
            if (id == null || id == 0)
            {
                return Results.NotFound("id non valide!");
            }

            User? user = db.Users.Find(id);

            if (user is User)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return Results.Ok("Delete Completed!");
            }

            return Results.NotFound();

        }
    }
}
