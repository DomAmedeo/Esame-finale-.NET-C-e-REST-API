using Its_31_Esame_RestApi_Sablone.Models;

var builder = WebApplication.CreateBuilder(args);

// learn more about https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

//RICERCA AUTORE PER NOME 
app.MapGet("api/nome_autori/{varNome}", (string varNome) =>
{
    Autore? a = autori.FirstOrDefault(aut => aut.Nome == varNome);
    if (a is not null)
        return Results.Ok(a);
    return Results.NotFound();
});

//altro possibile modo per fare una ricerca con una stringa
app.MapGet("api/nomeautori/{varNome}", (string varNome) =>
{
    Autore? a = autori.Find(a => a.Nome == varNome);
    return Results.Ok(a);
});


//aggiungere un nuovo autore
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

//modifica autore
app.MapPut("api/autori/{varID}", (int varID,Autore autoreNuovo) =>
{
    Autore? autoreVecchio = autori.FirstOrDefault(aut => aut.Id == varID);
    if (autoreNuovo.Nome == "")
        return Results.BadRequest();

    if (autoreVecchio is null)
        return Results.NotFound();
    
    if (autoreNuovo.Nome is not null)
        autoreVecchio.Nome = autoreNuovo.Nome;

    if (autoreNuovo.Libri is not null)
        autoreVecchio.Libri = autoreNuovo.Libri;

        return Results.Ok();
});


//Visualizza Libro in base all'ID(DA IMPLEMENTARE)
app.MapGet("api/libri/{varID}", (int varID) =>
{

});

//inserire un libro se l'autore è gia esistente, oppure inserire un nuovo autore con nuovi libri
app.MapPost("api/libri/{autID}", (int autID, Autore autoreNuovo ) =>
{
    Autore? autoreVecchio = autori.FirstOrDefault(a => a.Id == autID);
    if (autoreNuovo.Nome == "")
        return Results.BadRequest();

    if (autoreVecchio is null)
    {
        autoreNuovo.Id = autori.Count + 1;
        autori.Add(autoreNuovo);
        return Results.Created("api/libri" + autoreNuovo.Nome, null);
    }
    else
    {
        autoreNuovo.Id = autID;

        autori.Remove(autoreVecchio);
        autori.Add(autoreNuovo);
        return Results.Created("api/libri" + autoreNuovo.Nome, null);
    }
});






app.Run();
