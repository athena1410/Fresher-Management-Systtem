using Application.Core.Commands.Account.Login;
using Application.Core.Commands.Account.Register;
using Application.Core.Commands.Offers.CreateOffer;
using Application.Core.DTOs.Account;
using Application.Core.DTOs.Offers;
using AutoMapper;

namespace FresherManagement.Api.Infrastructures.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, RegisterCommand>()
                .AfterMap<SetAuditAction>();

            CreateMap<IdentityDto, LoginCommand>()
                .AfterMap<SetAuditAction<IdentityResponseDto>>();
            
            CreateMap<CreateOfferDto, CreateOfferCommand>()
                .AfterMap<SetAuditAction>();
        }
    }
}
