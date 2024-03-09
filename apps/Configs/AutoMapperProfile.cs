
using apps.Models.Entities;
using apps.Models.Request;
using AutoMapper;

namespace apps.Configs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddMasterRequest, PromoMaster>()
                .ForMember(x => x.Code, opt => opt.MapFrom(q => q.Code!.ToUpper()))
                .ForMember(x => x.QtyRemaining, opt => opt.MapFrom(q => q.Qty))
                .ForMember(x => x.BalanceRemaining, opt => opt.MapFrom(q => q.Balance));
        }
    }
}
