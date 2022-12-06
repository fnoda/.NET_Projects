using System;
using System.Collections.Generic;

namespace backendAPI.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string? Nome { get; set; }

    public DateTime? DataFecha { get; set; }

    public virtual ICollection<Empregado> Empregados { get; } = new List<Empregado>();
}
