using AutoMapper;
using backendAPI.DTOs;
using backendAPI.Models;
using System.Globalization;
using System.Net.Mime;

namespace backendAPI.Util
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Departamento
            CreateMap<Departamento, DepartamentoDTO>().ReverseMap();
            #endregion

            #region Empregado
            CreateMap<Empregado, EmpregadoDTO>()
                .ForMember(dtn => dtn.nomeDepartamento,
                opt => opt.MapFrom(origem => origem.IdDepartamentoNavigation.Nome)
                )
                .ForMember(dtn => dtn.Contrato,
                opt => opt.MapFrom(origem => origem.Contrato.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<EmpregadoDTO, Empregado>()
                .ForMember(dtn =>
                dtn.IdDepartamentoNavigation,
                opt => opt.Ignore()
                )
                .ForMember(dtn =>
                dtn.Contrato,
                opt => opt.MapFrom(origem => DateTime.ParseExact(origem.Contrato, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                );

            #endregion

        }
    }
}
