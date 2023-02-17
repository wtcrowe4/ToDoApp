
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

var app = builder.Build();

//app.UseHttpsRedirection();

//GET all Lists
app.MapGet("api/todo", async (AppDbContext db) =>
{
    var todoLists = await db.ToDoLists.ToListAsync();
    return Results.Ok(todoLists);
});

//POST Create List
app.MapPost("api/todo", async (AppDbContext db, ToDoList todoList) =>
{
    await db.ToDoLists.AddAsync(todoList);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todo/{todoList.Id}", todoList);
});

//PUT Edit List
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

//DELETE Delete List
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

//GET Specific List
app.MapGet("api/todo/{Id}", async (AppDbContext db, int Id) =>
    {
        var toDoList = await db.ToDoLists.FirstOrDefaultAsync(t => t.Id == Id);
        return Results.Ok(toDoList);
    });


//PUT Add Item to Specific List
app.MapPost("api/todo/{Id}/items", async (AppDbContext db, int Id, Item Item) =>
{
    var toDoList = await db.ToDoLists.FirstOrDefaultAsync(t => t.Id == Id);
    //Call method to add item
    await db.Items.AddAsync(Item);
    await db.SaveChangesAsync();
    return Results.Ok(toDoList);

});


//DELETE Delete Item from Specific List
app.MapDelete("api/todo/{Id}/items/{ItemId}", async (AppDbContext db, int Id, int ItemId) =>
{
    var toDoList = await db.ToDoLists.FirstOrDefaultAsync(t => t.Id == Id);
    var item = await db.Items.FirstOrDefaultAsync(i => i.Id == ItemId);
    if (item == null)
    {
        return Results.NotFound();
    }
    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.Ok(toDoList);
});

//PUT Edit Item from Specific List
app.MapPut("api/todo/{Id}/items/{ItemId}", async (AppDbContext db, int Id, int ItemId, Item Item) =>
{
    var toDoList = await db.ToDoLists.FirstOrDefaultAsync(t => t.Id == Id);
    var item = await db.Items.FirstOrDefaultAsync(i => i.Id == ItemId);
    if (item == null)
    {
        return Results.NotFound();
    }
    item.Name = Item.Name;
    item.IsChecked = Item.IsChecked;
    await db.SaveChangesAsync();
    return Results.Ok(toDoList);
});


app.Run();


