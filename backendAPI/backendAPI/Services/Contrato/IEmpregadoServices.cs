using backendAPI.Models;

namespace backendAPI.Services.Contrato
{
    public interface IEmpregadoServices
    {
        Task<List<Empregado>> GetList();

        Task<Empregado> Get(int idEmpregado);

        Task<Empregado> Add(Empregado modelo);

        Task<bool> Update(Empregado modelo);

        Task<bool> Delete(Empregado modelo);

    }
}
