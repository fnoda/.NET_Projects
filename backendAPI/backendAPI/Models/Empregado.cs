using System;
using System.Collections.Generic;

namespace backendAPI.Models;

public partial class Empregado
{
    public int IdEmpregado { get; set; }

    public string? Nome { get; set; }

    public int? IdDepartamento { get; set; }

    public int? Salario { get; set; }

    public DateTime? Contrato { get; set; }

    public DateTime? DataFecha { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }
}
