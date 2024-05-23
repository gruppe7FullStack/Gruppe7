using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gruppe7.Migrations
{
    /// <inheritdoc />
    public partial class addTablesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    LevelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "RootObject",
                columns: table => new
                {
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RootObject", x => x.SourceId);
                });

            migrationBuilder.CreateTable(
                name: "Observation",
                columns: table => new
                {
                    Elementid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOffset = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeResolution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSeriesId = table.Column<int>(type: "int", nullable: false),
                    RootObjectSourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observation", x => x.Elementid);
                    table.ForeignKey(
                        name: "FK_Observation_RootObject_RootObjectSourceId",
                        column: x => x.RootObjectSourceId,
                        principalTable: "RootObject",
                        principalColumn: "SourceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Observation_RootObjectSourceId",
                table: "Observation",
                column: "RootObjectSourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "Observation");

            migrationBuilder.DropTable(
                name: "RootObject");
        }
    }
}
