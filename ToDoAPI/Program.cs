using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

var app = builder.Build();

//app.UseHttpsRedirection();

app.MapGet("api/todo", async (AppDbContext db) =>
{
    var todoLists = await db.ToDoLists.ToListAsync();
    return Results.Ok(todoLists);
});

//POST Lists to database
app.MapPost("api/todo", async(AppDbContext db, ToDoList todoList) =>
{
    await db.ToDoLists.AddAsync(todoList);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todo/{todoList.Id}", todoList);
});

app.MapPut("api/todo{id}", async (AppDbContext db, int Id, ToDoList todoList) =>
    {
        var toDoListModel = await db.ToDoLists.FirstOrDefaultAsync(t => t.Id == Id);
        if (toDoListModel == null)
        {
            return Results.NotFound();
        }
        toDoListModel.Title = todoList.Title;
        toDoListModel.Description = todoList.Description;
        toDoListModel.Icon = todoList.Icon;
        toDoListModel.Items = todoList.Items;
        await db.SaveChangesAsync();
        return Results.Ok(toDoListModel);

    });

app.MapDelete("api/todo/{Id}", async (AppDbContext db, int Id) =>
{
    var toDoListModel = await db.ToDoLists.FirstOrDefaultAsync(t => t.Id == Id);
    if (toDoListModel == null)
    {
        return Results.NotFound();
    }
    db.ToDoLists.Remove(toDoListModel);
    await db.SaveChangesAsync();
    return Results.Ok(toDoListModel);
});





//app.MapPost("api/todo", async(AppDbContext db, Items items) =>
//{
//    await db.Items.AddAsync(items);
//    await db.SaveChangesAsync();
//    return Results.Created($"/api/todo/{items.Id}", items);
//});


//app.MapPost("api/todo/{listId}/items", async(AppDbContext db, Id, item) =>
//    {
//        var todoList = await db.ToDoLists.FindAsync(Id);
//    if (todoList == null)
//        {
//            return Results.NotFound();
//        }

//    if (todoList.Items == null)
//        {
//        todoList.Items = new List<Items>();
//        }

//        todoList.Items.Add(item);
//        await db.SaveChangesAsync();
//        return Results.Created($"/api/todo/{todoList.Id}/items/{item.Id}", item);
//        }); 

    

app.Run();


