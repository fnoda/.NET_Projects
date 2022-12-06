using backendAPI.Models;

namespace backendAPI.Services.Contrato
{
    public interface IDepartamentoServices
    {
        Task<List<Departamento>> GetList();
    }
}
