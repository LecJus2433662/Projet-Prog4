using Projet4_prog.DTO.Produit;

namespace Projet4_prog.Services
{
    public interface IProduitService
    {
        Task<IEnumerable<ProduitLectureDto>> ObtenirTousAsync();
        Task<ProduitLectureDto?> ObtenirParIdAsync(int id);
        Task<ProduitLectureDto> CreerAsync(ProduitEcritureDto dto);
        Task<ProduitLectureDto?> ModifierAsync(int id, ProduitEcritureDto dto);
        Task<bool> SupprimerAsync(int id);
    }
}