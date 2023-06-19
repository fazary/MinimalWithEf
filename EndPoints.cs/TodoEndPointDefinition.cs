using Microsoft.AspNetCore.Http.HttpResults;

namespace MinimalWithEf.EndPoints;

public class TodoEndPointDefinition : IEndPointDefinition
{
    public void RegisterEndPoints(WebApplication app)
    {
        CancellationToken ct = new();
        var todo = app.MapGroup("/api/Todo");
        todo.MapGet("/", async (IRepository<Todo> db) =>
        {
            return await db.GetAllAsync();
        });
        todo.MapGet("/{id}", async (int id, IRepository<Todo> db) =>
        {
            return await db.GetByIdAsync(id);
        });
        todo.MapPost("/", async (Todo entity, IRepository<Todo> db) =>
        {
            await db.AddAsync(entity, ct);
        });
        todo.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, IRepository<Todo> db) =>
        {
            var affected = await db.DeletAsync(id, ct);
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        });
    }
}