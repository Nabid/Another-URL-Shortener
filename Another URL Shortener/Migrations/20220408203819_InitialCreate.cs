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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortedURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrls", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ShortUrls",
                columns: new[] { "Id", "CreatedOn", "IsExpired", "ModifiedOn", "ShortedURL", "URL" },
                values: new object[] { new Guid("f46aec3c-3b6b-4217-82ae-3566c9986416"), new DateTime(2022, 4, 8, 22, 38, 18, 850, DateTimeKind.Local).AddTicks(5538), false, new DateTime(2022, 4, 8, 22, 38, 18, 855, DateTimeKind.Local).AddTicks(6739), "https://blog.jetbrains.com/12345", "https://blog.jetbrains.com/dotnet/2020/11/25/getting-started-with-entity-framework-core-5/" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortUrls");
        }
    }
}
