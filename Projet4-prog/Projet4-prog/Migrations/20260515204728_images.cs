using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet4_prog.Migrations
{
    /// <inheritdoc />
    public partial class images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://images.radio-canada.ca/q_auto,w_844/v1/alimentation/recette/16x9/2892-tire-erable.jpg");

            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://img.fourchette-et-bikini.fr/1200x900/2025/03/07/i50846-crepes-sucrees-sans-gluten-et-faciles.webp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://www.noovomoi.ca/cuisiner/trucs-et-inspirations/article.etapes-tire-erable-maison.1.618708.html");

            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://www.noovomoi.ca/cuisiner/trucs-et-inspirations/article.etapes-tire-erable-maison.1.618708.html");
        }
    }
}
