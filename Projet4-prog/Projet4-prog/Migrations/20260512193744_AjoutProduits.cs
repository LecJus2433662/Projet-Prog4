using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projet4_prog.Migrations
{
    /// <inheritdoc />
    public partial class AjoutProduits : Migration
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
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbProduitRestant = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilisateurId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commandes_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCommande",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandeId = table.Column<int>(type: "int", nullable: false),
                    ProduitId = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    PrixUnitaire = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCommande", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsCommande_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsCommande_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Produits",
                columns: new[] { "Id", "Description", "NbProduitRestant", "Nom", "Prix", "image" },
                values: new object[,]
                {
                    { 1, "bacon infusé au sirop d'érable cuisiner avec l'amour pour Jad", 67, "Bacon halal au sirop d'érable", 12.67, "https://www.instagram.com/reel/DPHjzjGEixS/" },
                    { 2, "c'est tellement bon pour mathis", 67, "Pudding chomeur", 16.670000000000002, "https://www.allrecipes.com/recipe/267358/pouding-chomeur/" },
                    { 3, "Le sirop d'érable chauffé est versé dans la neige pour créer la tire. · Versez le sirop d'érable dans une neige compactée", 67, "Tire d'érable", 1.6699999999999999, "https://www.noovomoi.ca/cuisiner/trucs-et-inspirations/article.etapes-tire-erable-maison.1.618708.html" },
                    { 4, "Des pitas mais sucrée", 67, "3 Crèpes", 8.6699999999999999, "https://www.noovomoi.ca/cuisiner/trucs-et-inspirations/article.etapes-tire-erable-maison.1.618708.html" }
                });

            
            migrationBuilder.CreateIndex(
                name: "IX_Commandes_UtilisateurId",
                table: "Commandes",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCommande_CommandeId",
                table: "ItemsCommande",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCommande_ProduitId",
                table: "ItemsCommande",
                column: "ProduitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "ItemsCommande");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Produits");

            
        }
    }
}
