using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Another_URL_SHortener.Migrations
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
                    URL = table.Column<string>(type: "text", nullable: true),
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
                values: new object[] { new Guid("985e8b43-f346-440a-8b01-94921ae72b86"), new DateTime(2022, 4, 12, 12, 17, 29, 631, DateTimeKind.Local).AddTicks(3461), false, new DateTime(2022, 4, 12, 12, 17, 29, 633, DateTimeKind.Local).AddTicks(5617), "https://blog.jetbrains.com/12345", "https://blog.jetbrains.com/dotnet/2020/11/25/getting-started-with-entity-framework-core-5/" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortUrls");
        }
    }
}
