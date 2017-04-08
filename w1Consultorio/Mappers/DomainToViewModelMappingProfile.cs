using AutoMapper;
using w1Consultorio.Models;
using w1Consultorio.Models.ViewModel;

namespace w1Consultorio.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Prontuario, ProntuarioViewModel>();
        }
    }
}