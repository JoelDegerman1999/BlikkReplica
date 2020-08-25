using Microsoft.EntityFrameworkCore.Migrations;

namespace BlikkBaiscReplica.Migrations
{
    public partial class EmbeddedAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Address_VisitingAddressId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_VisitingAddressId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "VisitingAddressId",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "VisitingAddress_City",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitingAddress_Country",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitingAddress_PostalCode",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisitingAddress_StreetAddress",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitingAddress_City",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "VisitingAddress_Country",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "VisitingAddress_PostalCode",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "VisitingAddress_StreetAddress",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "VisitingAddressId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_VisitingAddressId",
                table: "Contacts",
                column: "VisitingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Address_VisitingAddressId",
                table: "Contacts",
                column: "VisitingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
