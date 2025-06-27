using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeiasProxy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "teias");

            migrationBuilder.CreateTable(
                name: "ProxyLogs",
                schema: "teias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FirmRequestXml = table.Column<string>(type: "text", nullable: false),
                    TeiasRequestXml = table.Column<string>(type: "text", nullable: false),
                    TeiasResponseXml = table.Column<string>(type: "text", nullable: false),
                    FirmResponseXml = table.Column<string>(type: "text", nullable: false),
                    SantralAd = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProxyLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProxyLogs",
                schema: "teias");
        }
    }
}
