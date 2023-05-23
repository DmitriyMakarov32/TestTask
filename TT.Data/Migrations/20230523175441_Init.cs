using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TT.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "test_task");

            migrationBuilder.CreateTable(
                name: "Searches",
                schema: "test_task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SearchState = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Searches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Queries",
                schema: "test_task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<string>(type: "jsonb", nullable: false),
                    SearchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Queries_Searches_SearchId",
                        column: x => x.SearchId,
                        principalSchema: "test_task",
                        principalTable: "Searches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchResults",
                schema: "test_task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SearchResultState = table.Column<string>(type: "text", nullable: false),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    SearchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchResults_Searches_SearchId",
                        column: x => x.SearchId,
                        principalSchema: "test_task",
                        principalTable: "Searches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                schema: "test_task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Origin = table.Column<string>(type: "text", nullable: false),
                    Destination = table.Column<string>(type: "text", nullable: false),
                    OriginDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DestinationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    TimeLimit = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SearchResultId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_SearchResults_SearchResultId",
                        column: x => x.SearchResultId,
                        principalSchema: "test_task",
                        principalTable: "SearchResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Queries_SearchId",
                schema: "test_task",
                table: "Queries",
                column: "SearchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_Origin_Destination_OriginDateTime",
                schema: "test_task",
                table: "Routes",
                columns: new[] { "Origin", "Destination", "OriginDateTime" })
                .Annotation("Npgsql:IndexInclude", new[] { "DestinationDateTime", "Price", "TimeLimit" });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_SearchResultId",
                schema: "test_task",
                table: "Routes",
                column: "SearchResultId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_SearchId",
                schema: "test_task",
                table: "SearchResults",
                column: "SearchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queries",
                schema: "test_task");

            migrationBuilder.DropTable(
                name: "Routes",
                schema: "test_task");

            migrationBuilder.DropTable(
                name: "SearchResults",
                schema: "test_task");

            migrationBuilder.DropTable(
                name: "Searches",
                schema: "test_task");
        }
    }
}
