using AutoMapper;
using TKILSAPRFC.Model.DataBaseModels;
using TKILSAPRFC.Model.ViewModels;

namespace TKILSAPRFC.Model.Mapper
{
    public class MappingProfile : Profile
    {
        MapperConfiguration Config;
        IMapper mapper;
    }

    public class AutoMappers : IAutomappers
    {
        MapperConfiguration Config;
        IMapper mapper;
        public AutoMappers()
        {
        }
        public D MappModel<D, S>(S mappModel)
        {
            Config = new AutoMapper.MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<S, D>();
                });
            mapper = Config.CreateMapper();

            return mapper.Map<D>(mappModel);
        }
        public List<D> MappListModel<D, S>(List<S> mappModel)
        {
            Config = new AutoMapper.MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<S, D>();
                });
            mapper = Config.CreateMapper();

            return mapper.Map<List<D>>(mappModel);
        }

        public IEnumerable<D> MappListModel<D, S>(IEnumerable<S> mappModel)
        {
            Config = new AutoMapper.MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<S, D>();
                });
            mapper = Config.CreateMapper();

            return mapper.Map<IEnumerable<D>>(mappModel);
        }
    }

}
