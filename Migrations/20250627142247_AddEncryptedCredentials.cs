using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeiasProxy.Migrations
{
    /// <inheritdoc />
    public partial class AddEncryptedCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LagosCredentials",
                schema: "teias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EncryptedUsername = table.Column<string>(type: "text", nullable: false),
                    EncryptedPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LagosCredentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantCredentials",
                schema: "teias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantName = table.Column<string>(type: "text", nullable: false),
                    EncryptedUsername = table.Column<string>(type: "text", nullable: false),
                    EncryptedPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantCredentials", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LagosCredentials",
                schema: "teias");

            migrationBuilder.DropTable(
                name: "PlantCredentials",
                schema: "teias");
        }
    }
}
