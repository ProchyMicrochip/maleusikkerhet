using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace måleusikkerhet.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalogDev",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogDev", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "DigitalDev",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Resolution = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalDev", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PreciseDev",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreciseDev", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "DigitalAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Range = table.Column<double>(type: "double precision", nullable: false),
                    Digits = table.Column<double>(type: "double precision", nullable: false),
                    RangeError = table.Column<double>(type: "double precision", nullable: false),
                    DigitalName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalAttributes_DigitalDev_DigitalName",
                        column: x => x.DigitalName,
                        principalTable: "DigitalDev",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DigitalAttributes_DigitalName",
                table: "DigitalAttributes",
                column: "DigitalName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalogDev");

            migrationBuilder.DropTable(
                name: "DigitalAttributes");

            migrationBuilder.DropTable(
                name: "PreciseDev");

            migrationBuilder.DropTable(
                name: "DigitalDev");
        }
    }
}
