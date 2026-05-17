using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet4_prog.Migrations
{
    /// <inheritdoc />
    public partial class produitsFull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Produits",
                columns: new[] { "Id", "Description", "Image", "NbProduitRestant", "Nom", "Prix" },
                values: new object[,]
                {
                    { 5, "Une tartinade sucrée et crémeuse faite 100% de sirop d'érable pur", "https://images.squarespace-cdn.com/content/v1/5ff4c7c50debdf5d1ff004bc/93f4f384-792c-495c-97b3-fdca6c514eb9/Beurre+d%27%C3%A9rable.png?format=1500w", 45, "Beurre d'érable", 9.9900000000000002 },
                    { 6, "Crème glacée à la vanille recouverte de sirop d'érable chaud", "https://cdn.pratico-pratiques.com/app/uploads/sites/3/2018/08/20185844/mini-cornets-a-l-erable.jpeg", 32, "Cornet à l'érable", 6.4900000000000002 },
                    { 7, "Biscuits croustillants fourrés avec une crème au sirop d'érable", "https://erableduquebec.ca/uploads/2022/01/recette-biscuits-erable-1200x900-1-600x450.jpg", 78, "Biscuits à l'érable", 5.9900000000000002 },
                    { 8, "Un milkshake froid et sucré avec du vrai sirop d'érable du Québec", "https://erableduquebec.ca/uploads/2022/07/recette-lait-frappe-erable-1200x900-1-600x450.jpg", 21, "Lait frappé à l'érable", 7.25 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
