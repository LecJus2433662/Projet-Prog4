using AutoMapper;
using Projet4_prog.DTO.Commande;
using Projet4_prog.DTO.ItemCommande;
using Projet4_prog.DTO.Produit;
using Projet4_prog.DTO.Auth;
using Projet4_prog.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projet4_prog.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Produit
            CreateMap<Produit, ProduitLectureDto>();
            CreateMap<ProduitEcritureDto, Produit>();

            // Commande
            CreateMap<Commande, CommandeLectureDto>()
                .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()));

            CreateMap<CommandeCreationDto, Commande>();

            // ItemCommande
            CreateMap<ItemCommande, ItemCommandeLectureDto>()
                .ForMember(dest => dest.NomProduit, opt => opt.MapFrom(src => src.Produit!.Nom));

            CreateMap<ItemCommandeCreationDto, ItemCommande>();
        }
    }
}