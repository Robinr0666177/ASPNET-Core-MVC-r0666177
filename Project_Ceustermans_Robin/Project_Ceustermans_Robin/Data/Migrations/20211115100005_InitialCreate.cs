using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Ceustermans_Robin.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    CategorieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beschrijving = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.CategorieID);
                });

            migrationBuilder.CreateTable(
                name: "Land",
                columns: table => new
                {
                    LandID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beschrijving = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Land", x => x.LandID);
                });

            migrationBuilder.CreateTable(
                name: "Mede_Eigenaar",
                columns: table => new
                {
                    MedeEigenaarID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(nullable: false),
                    Familienaam = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mede_Eigenaar", x => x.MedeEigenaarID);
                });

            migrationBuilder.CreateTable(
                name: "Merk",
                columns: table => new
                {
                    MerkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: false),
                    LandID = table.Column<int>(nullable: false),
                    Beschrijving = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merk", x => x.MerkID);
                    table.ForeignKey(
                        name: "FK_Merk_Land_LandID",
                        column: x => x.LandID,
                        principalTable: "Land",
                        principalColumn: "LandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerzamelObject",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: false),
                    Beschrijving = table.Column<string>(nullable: true),
                    AankoopPrijs = table.Column<decimal>(nullable: true),
                    Waarde = table.Column<decimal>(nullable: true),
                    CreatieJaar = table.Column<int>(nullable: true),
                    MerkID = table.Column<int>(nullable: true),
                    CategorieID = table.Column<int>(nullable: false),
                    Breedte_Cm = table.Column<int>(nullable: true),
                    Hoogte_Cm = table.Column<int>(nullable: true),
                    Lengte_Cm = table.Column<int>(nullable: true),
                    Afbeelding = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerzamelObject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VerzamelObject_Categorie_CategorieID",
                        column: x => x.CategorieID,
                        principalTable: "Categorie",
                        principalColumn: "CategorieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VerzamelObject_Merk_MerkID",
                        column: x => x.MerkID,
                        principalTable: "Merk",
                        principalColumn: "MerkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedeEigenaarObjecten",
                columns: table => new
                {
                    MedeEigenaarID = table.Column<int>(nullable: false),
                    ObjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedeEigenaarObjecten", x => new { x.MedeEigenaarID, x.ObjectID });
                    table.ForeignKey(
                        name: "FK_MedeEigenaarObjecten_Mede_Eigenaar_MedeEigenaarID",
                        column: x => x.MedeEigenaarID,
                        principalTable: "Mede_Eigenaar",
                        principalColumn: "MedeEigenaarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedeEigenaarObjecten_VerzamelObject_ObjectID",
                        column: x => x.ObjectID,
                        principalTable: "VerzamelObject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedeEigenaarObjecten_ObjectID",
                table: "MedeEigenaarObjecten",
                column: "ObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Merk_LandID",
                table: "Merk",
                column: "LandID");

            migrationBuilder.CreateIndex(
                name: "IX_VerzamelObject_CategorieID",
                table: "VerzamelObject",
                column: "CategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_VerzamelObject_MerkID",
                table: "VerzamelObject",
                column: "MerkID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedeEigenaarObjecten");

            migrationBuilder.DropTable(
                name: "Mede_Eigenaar");

            migrationBuilder.DropTable(
                name: "VerzamelObject");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropTable(
                name: "Merk");

            migrationBuilder.DropTable(
                name: "Land");
        }
    }
}
