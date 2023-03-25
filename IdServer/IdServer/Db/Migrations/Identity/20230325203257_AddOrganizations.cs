using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdServer.Db.Migrations.Identity
{
    public partial class AddOrganizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationEntityIdentifier",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "OrganizationEntities",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationEntities", x => x.Identifier);
                });
            
            migrationBuilder.Sql("exec('UPDATE pt SET pt.Identifier = NEWID() FROM dbo.OrganizationEntities pt')");

            migrationBuilder.CreateTable(
                name: "InvitationEntities",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationEntityIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitationEntities", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_InvitationEntities_OrganizationEntities_OrganizationEntityIdentifier",
                        column: x => x.OrganizationEntityIdentifier,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.Sql("exec('UPDATE pt SET pt.Identifier = NEWID() FROM dbo.InvitationEntities pt')");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrganizationEntityIdentifier",
                table: "AspNetUsers",
                column: "OrganizationEntityIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationEntities_OrganizationEntityIdentifier",
                table: "InvitationEntities",
                column: "OrganizationEntityIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_OrganizationEntities_OrganizationEntityIdentifier",
                table: "AspNetUsers",
                column: "OrganizationEntityIdentifier",
                principalTable: "OrganizationEntities",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_OrganizationEntities_OrganizationEntityIdentifier",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "InvitationEntities");

            migrationBuilder.DropTable(
                name: "OrganizationEntities");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OrganizationEntityIdentifier",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OrganizationEntityIdentifier",
                table: "AspNetUsers");
        }
    }
}
