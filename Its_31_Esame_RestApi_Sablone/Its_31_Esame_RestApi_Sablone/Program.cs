using Its_31_Esame_RestApi_Sablone.Models;

var builder = WebApplication.CreateBuilder(args);

// learn more about https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();


var autori = new List<Autore>
{
    new Autore { Id = 1, Nome = "Italo Calvino", Libri = { new Libro { Id = 1, Nome_libro = "Le città invisibili", Genere = "Narrativa", Data_publicazione = "1972" } } },
    new Autore { Id = 2, Nome = "Elena Ferrante", Libri = { new Libro { Id = 2, Nome_libro = "L'amica geniale", Genere = "Romanzo", Data_publicazione = "2011" } } },
};

//visualizza tutti gli autori
app.MapGet("api/autori", () =>
{
    return Results.Ok(autori);
});

//visualizza autore in base all'id
app.MapGet("api/autori/{varID}", (int varID) =>
{
    Autore? a = autori.FirstOrDefault(aut => aut.Id == varID);
    if (a is not null)
        return Results.Ok(a);
    return Results.NotFound();
});

//RICERCA AUTORE PER NOME (DA COMPLETARE)
//app.MapGet("api/nomeautori/{varNome}", (string varNome) =>
//{
//    Autore? a = autori.FirstOrDefault(aut => aut.Nome == varNome);
//    if (a is not null)
//        return Results.Ok(a);
//    else Results.NotFound();
//});

// aggiungere un nuovo autore
app.MapPost("api/autori", (Autore autore) =>
{
    autore.Id = autori.Count + 1;
    autori.Add(autore);
    return Results.Created("api/autori" + autore.Nome, null);

});

//eliminare un autore
app.MapDelete("api/autori/{varID}", (int varID) =>
{
    Autore? a = autori.FirstOrDefault(aut => aut.Id == varID);
    if (a is not null)
    {
        autori.Remove(a);
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapPut("api/autori/{varID}", (int varID, Autore autoreNuovo) =>
{
    Autore ? autoreVecchio = autori.FirstOrDefault(aut => aut.Id == varID);
    if (autoreVecchio is not null)
    {
        if (autoreNuovo.Nome is not null)
            autoreVecchio.Nome = autoreNuovo.Nome;
        if (autoreNuovo.Libri is not null)
            autoreVecchio.Libri = autoreNuovo.Libri;

    }
    else Results.NotFound();

});

app.Run();
