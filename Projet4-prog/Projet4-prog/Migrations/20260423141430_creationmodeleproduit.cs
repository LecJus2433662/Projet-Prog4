using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet4_prog.Migrations
{
    /// <inheritdoc />
    public partial class creationmodeleproduit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbProduitRestant = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Produits",
                columns: new[] { "Id", "Description", "NbProduitRestant", "Nom", "Prix", "image" },
                values: new object[,]
                {
                    { 1, "bacon infusé au sirop d'érable cuisiner avec l'amour pour Jad", 67, "Bacon halal au sirop d'érable", 12.67, "https://www.instagram.com/reel/DPHjzjGEixS/" },
                    { 2, "c'est tellement bon pour mathis", 1, "Pudding chomeur", 16.670000000000002, "https://www.allrecipes.com/recipe/267358/pouding-chomeur/" },
                    { 3, "Le sirop d'érable chauffé est versé dans la neige pour créer la tire. · Versez le sirop d'érable dans une neige compactée", 80, "Tire d'érable", 1.6699999999999999, "https://www.noovomoi.ca/cuisiner/trucs-et-inspirations/article.etapes-tire-erable-maison.1.618708.html" },
                    { 4, "Des pitas mais sucrée", 8, "3 Crèpes", 8.6699999999999999, "https://www.noovomoi.ca/cuisiner/trucs-et-inspirations/article.etapes-tire-erable-maison.1.618708.html" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produits");
        }
    }
}
