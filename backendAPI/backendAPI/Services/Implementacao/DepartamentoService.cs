using Microsoft.EntityFrameworkCore;
using backendAPI.Models;
using backendAPI.Services.Contrato;

namespace backendAPI.Services.Implementacao
{
    public class DepartamentoService: IDepartamentoServices
    {
        private DbEmpregadoContext _dbContext;

        public DepartamentoService(DbEmpregadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Departamento>> GetList()
        {
            try
            {
                List<Departamento> listaDepartamento = new List<Departamento>();
                listaDepartamento = await _dbContext.Departamentos.ToListAsync();

                return listaDepartamento;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
