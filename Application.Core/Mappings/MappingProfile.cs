using Application.Core.Commands.Offers.CreateOffer;
using Application.Core.Commands.Offers.UpdateOffer;
using Application.Core.DTOs.Candidates;
using Application.Core.DTOs.Offers;
using Application.Domain.Entities;
using AutoMapper;

namespace Application.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOfferCommand, Offer>()
                .ForMember(dst => dst.Id, opt => opt.Ignore());

            CreateMap<Candidate, CandidateDto>();
            CreateMap<Offer, OfferDto>();
            CreateMap<UpdateOfferDto, UpdateOfferCommand>()
                .ForMember(dst => dst.OfferId, opt => opt.MapFrom(src => src.Id));
            CreateMap<UpdateOfferCommand, Offer>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.CreatedBy, opt => opt.Ignore())
                .ForMember(dst => dst.CreatedDate, opt => opt.Ignore())
                .ForMember(dst => dst.ModifiedDate, opt => opt.Ignore())
                .ForMember(dst => dst.ModifiedBy, opt => opt.Ignore());
        }
    }
}
