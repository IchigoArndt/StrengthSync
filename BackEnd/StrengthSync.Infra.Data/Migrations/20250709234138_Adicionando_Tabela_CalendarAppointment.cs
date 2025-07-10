using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrengthSync.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adicionando_Tabela_CalendarAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "dbo",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "dbo",
                newName: "Users",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "dbo",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Calendars",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trained = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_UserId",
                schema: "dbo",
                table: "Calendars",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendars",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "dbo",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "users",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "dbo",
                table: "users",
                column: "Id");
        }
    }
}
