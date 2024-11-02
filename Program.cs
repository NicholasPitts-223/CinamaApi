using System.Collections.Generic;
using CinemaApp.Models; // Ajuste conforme necessário
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados ao contêiner de serviços
builder.Services.AddDbContext<CinemaDbContext>(options =>
    options.UseSqlite("Data Source=CinemaDatabase.db"));

var app = builder.Build();

// Método para popular o banco de dados
void SeedDatabase(CinemaDbContext context)
{
    if (!context.Cinemas.Any())
    {
        var cinema1 = new Cinema
        {
            Nome = "Cineplex",
            Localizacao = "São Paulo",
            Filmes = new List<Filme>
            {
                new Filme { Titulo = "Vingadores: Ultimato", Genero = "Ação", Duracao = 181 },
                new Filme { Titulo = "A Grande Aposta", Genero = "Drama", Duracao = 130 }
            }
        };

        var cinema2 = new Cinema
        {
            Nome = "Cineworld",
            Localizacao = "Rio de Janeiro",
            Filmes = new List<Filme>
            {
                new Filme { Titulo = "Corra!", Genero = "Suspense", Duracao = 104 },
                new Filme { Titulo = "La La Land", Genero = "Musical", Duracao = 128 }
            }
        };

        // Adicione mais cinemas conforme necessário

        context.Cinemas.AddRange(cinema1, cinema2);
        context.SaveChanges();
    }
}

// Chama o método de inicialização
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();
    SeedDatabase(dbContext);
}

// Configuração das rotas da API
app.MapGet("/cinemas", async (CinemaDbContext db) =>
    await db.Cinemas.Include(c => c.Filmes).ToListAsync());

app.MapGet("/cinemas/{id}", async (int id, CinemaDbContext db) =>
    await db.Cinemas.Include(c => c.Filmes).FirstOrDefaultAsync(c => c.Id == id)
    is Cinema cinema ? Results.Ok(cinema) : Results.NotFound());

app.MapPost("/cinemas", async (Cinema cinema, CinemaDbContext db) =>
{
    db.Cinemas.Add(cinema);
    await db.SaveChangesAsync();
    return Results.Created($"/cinemas/{cinema.Id}", cinema);
});

// Adicione outras rotas conforme necessário

app.Run();