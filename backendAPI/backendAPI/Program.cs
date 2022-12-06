using backendAPI.Models;
using Microsoft.EntityFrameworkCore;

using backendAPI.Services.Contrato;
using backendAPI.Services.Implementacao;

using AutoMapper;
using backendAPI.DTOs;
using backendAPI.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbEmpregadoContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexaoSQL"));
    });

builder.Services.AddScoped<IDepartamentoServices, DepartamentoService>();
builder.Services.AddScoped<IEmpregadoServices, EmpregadoService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Permissao", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod(); 
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Requisicao API Rest
app.MapGet("/Departamento/lista", async(
    IDepartamentoServices _departamentoServices,
    IMapper _mapper
    ) =>
{
    var listaDepartamento = await _departamentoServices.GetList();
    var listaDepartamentoDTO = _mapper.Map<List<DepartamentoDTO>>(listaDepartamento);

    if (listaDepartamentoDTO.Count >0)
        return Results.Ok(listaDepartamentoDTO);
    else
        return Results.NotFound();

});

app.MapGet("/Empregado/lista", async (
    IEmpregadoServices _empregadoServices,
    IMapper _mapper
    ) =>
{
    var listaEmpregado = await _empregadoServices.GetList();
    var listaEmpregadoDTO = _mapper.Map<List<EmpregadoDTO>>(listaEmpregado);

    if (listaEmpregadoDTO.Count > 0)
        return Results.Ok(listaEmpregadoDTO);
    else
        return Results.NotFound();

});

app.MapPost("/Empregado/salvar", async (
    EmpregadoDTO modelo,
    IEmpregadoServices _empregadoService,
    IMapper _mapper
    ) =>{
        var _empregado = _mapper.Map<Empregado>(modelo);
        var _empregadoCriado = await _empregadoService.Add(_empregado);

        if (_empregadoCriado.IdEmpregado != 0)
            return Results.Ok(_mapper.Map<EmpregadoDTO>(_empregadoCriado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });

app.MapPut("/Empregado/Atualizar/{idEmpregado}", async (
    int idEmpregado,
    EmpregadoDTO modelo,
    IEmpregadoServices _empregadoService,
    IMapper _mapper
    ) => {
        var _encontrado = await _empregadoService.Get(idEmpregado);
        if (_encontrado is null)
            return Results.NotFound();
        var _empregado = _mapper.Map<Empregado>(modelo);

        _encontrado.Nome = _empregado.Nome;
        _encontrado.IdDepartamento = _empregado.IdDepartamento;
        _encontrado.Salario = _empregado.Salario;
        _encontrado.DataFecha = _empregado.DataFecha;

        var resposta = await _empregadoService.Update(_encontrado);

        if(resposta)
            return Results.Ok(_mapper.Map<EmpregadoDTO>(_encontrado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });

app.MapDelete("/Empregado/Excluir/{idEmpregado}", async (
    int idEmpregado,
    IEmpregadoServices _empregadoService
    ) => {
        var _encontrado = await _empregadoService.Get(idEmpregado);
        if (_encontrado is null)
            return Results.NotFound();

        var resposta = await _empregadoService.Delete(_encontrado);

        if (resposta)
            return Results.Ok();
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });


#endregion

app.UseCors("Permissao");
app.Run();

