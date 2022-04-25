using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Another_URL_Shortener.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortUrls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    URL = table.Column<string>(type: "text", nullable: false),
                    ShortedURL = table.Column<string>(type: "text", nullable: true),
                    IsExpired = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrls", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ShortUrls",
                columns: new[] { "Id", "CreatedOn", "IsExpired", "ModifiedOn", "ShortedURL", "URL" },
                values: new object[] { new Guid("975a28e0-ad89-45e4-a906-e282a6c63e62"), new DateTime(2022, 4, 12, 12, 45, 0, 451, DateTimeKind.Local).AddTicks(66), false, new DateTime(2022, 4, 12, 12, 45, 0, 453, DateTimeKind.Local).AddTicks(1669), "https://blog.jetbrains.com/12345", "https://blog.jetbrains.com/dotnet/2020/11/25/getting-started-with-entity-framework-core-5/" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortUrls");
        }
    }
}
