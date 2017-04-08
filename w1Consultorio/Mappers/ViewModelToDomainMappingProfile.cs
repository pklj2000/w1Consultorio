using AutoMapper;
using w1Consultorio.Models;
using w1Consultorio.Models.ViewModel;

namespace w1Consultorio.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ProntuarioViewModel, Prontuario>();
        }
    }
}