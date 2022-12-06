namespace backendAPI.DTOs
{
    public class EmpregadoDTO
    {
        public int IdEmpregado { get; set; }

        public string? Nome { get; set; }

        public int? IdDepartamento { get; set; }

        public string? nomeDepartamento { get; set; }

        public int? Salario { get; set; }

        public string? Contrato { get; set; }
    }
}
