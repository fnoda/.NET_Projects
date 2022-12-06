using Microsoft.EntityFrameworkCore;
using backendAPI.Models;
using backendAPI.Services.Contrato;

namespace backendAPI.Services.Implementacao
{
    public class EmpregadoService : IEmpregadoServices
    {
        private DbEmpregadoContext _dbContext;

        public EmpregadoService(DbEmpregadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Empregado>> GetList()
        {
            try
            {
                List<Empregado> listaEmpregado = new List<Empregado>();
                listaEmpregado = await _dbContext.Empregados.Include(dpt => dpt.IdDepartamentoNavigation).ToListAsync();

                return listaEmpregado;

            }catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Empregado> Get(int idEmpregado)
        {
            try
            {
                Empregado? empregadoEncontrado = new Empregado();
                empregadoEncontrado = await _dbContext.Empregados.Include(dpt => dpt.IdDepartamentoNavigation)
                    .Where(emp => emp.IdEmpregado == idEmpregado).FirstOrDefaultAsync();

                return empregadoEncontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Empregado> Add(Empregado modelo)
        {
            try
            {
                _dbContext.Empregados.Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<bool> Update(Empregado modelo)
        {
            try
            {
                _dbContext.Empregados.Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Empregado modelo)
        {
            try
            {
                _dbContext.Empregados.Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       

        
    }

}
