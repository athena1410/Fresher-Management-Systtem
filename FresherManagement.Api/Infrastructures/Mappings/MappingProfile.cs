using Application.Core.Commands.Account.Login;
using Application.Core.Commands.Account.Register;
using Application.Core.DTOs.Account;
using AutoMapper;
using MediatR;

namespace FresherManagement.Api.Infrastructures.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequestDto, RegisterCommand>()
                .AfterMap<SetAuditAction<Unit>>();

            CreateMap<IdentityRequestDto, LoginCommand>()
                .AfterMap<SetAuditAction<IdentityResponseDto>>();
        }
    }
}
