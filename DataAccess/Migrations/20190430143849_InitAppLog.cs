using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitAppLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastUpdateTime = table.Column<DateTimeOffset>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    User = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Source = table.Column<string>(maxLength: 50, nullable: false),
                    Action = table.Column<string>(maxLength: 50, nullable: false),
                    Message = table.Column<string>(maxLength: 400, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
