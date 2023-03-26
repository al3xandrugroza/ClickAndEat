using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickAndEatApi.Db.Migrations
{
    public partial class AddUsersAndAdditionalConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodTypeEntities_OrganizationEntities_OrganizationEntityIdentifier",
                table: "FoodTypeEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuEntities_OrganizationEntities_OrganizationEntityIdentifier",
                table: "MenuEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntities_OrganizationEntities_OrganizationEntityIdentifier",
                table: "OrderEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartEntities_OrganizationEntities_OrganizationEntityIdentifier",
                table: "ShoppingCartEntities");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartEntities_OrganizationEntityIdentifier",
                table: "ShoppingCartEntities");

            migrationBuilder.DropIndex(
                name: "IX_OrderEntities_OrganizationEntityIdentifier",
                table: "OrderEntities");

            migrationBuilder.DropIndex(
                name: "IX_MenuEntities_OrganizationEntityIdentifier",
                table: "MenuEntities");

            migrationBuilder.RenameColumn(
                name: "OrganizationEntityIdentifier",
                table: "ShoppingCartEntities",
                newName: "UserEntityId");

            migrationBuilder.RenameColumn(
                name: "OrganizationEntityIdentifier",
                table: "OrderEntities",
                newName: "UserEntityId");

            migrationBuilder.RenameColumn(
                name: "OrganizationEntityIdentifier",
                table: "MenuEntities",
                newName: "OrganizationEntityId");

            migrationBuilder.RenameColumn(
                name: "OrganizationEntityIdentifier",
                table: "FoodTypeEntities",
                newName: "OrganizationEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodTypeEntities_OrganizationEntityIdentifier",
                table: "FoodTypeEntities",
                newName: "IX_FoodTypeEntities_OrganizationEntityId");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationEntityId",
                table: "ShoppingCartEntities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "OrderDeliverState",
                table: "OrderEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationEntityId",
                table: "OrderEntities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "OrderLockState",
                table: "MenuEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingLimit",
                table: "MenuEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FoodTypeEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "FoodTypeEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_UserEntity_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartEntities_OrganizationEntityId",
                table: "ShoppingCartEntities",
                column: "OrganizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartEntities_UserEntityId",
                table: "ShoppingCartEntities",
                column: "UserEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntities_OrganizationEntityId",
                table: "OrderEntities",
                column: "OrganizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntities_UserEntityId",
                table: "OrderEntities",
                column: "UserEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuEntities_OrganizationEntityId",
                table: "MenuEntities",
                column: "OrganizationEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_OrganizationEntityId",
                table: "UserEntity",
                column: "OrganizationEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodTypeEntities_OrganizationEntities_OrganizationEntityId",
                table: "FoodTypeEntities",
                column: "OrganizationEntityId",
                principalTable: "OrganizationEntities",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuEntities_OrganizationEntities_OrganizationEntityId",
                table: "MenuEntities",
                column: "OrganizationEntityId",
                principalTable: "OrganizationEntities",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntities_OrganizationEntities_OrganizationEntityId",
                table: "OrderEntities",
                column: "OrganizationEntityId",
                principalTable: "OrganizationEntities",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntities_UserEntity_UserEntityId",
                table: "OrderEntities",
                column: "UserEntityId",
                principalTable: "UserEntity",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartEntities_OrganizationEntities_OrganizationEntityId",
                table: "ShoppingCartEntities",
                column: "OrganizationEntityId",
                principalTable: "OrganizationEntities",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartEntities_UserEntity_UserEntityId",
                table: "ShoppingCartEntities",
                column: "UserEntityId",
                principalTable: "UserEntity",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodTypeEntities_OrganizationEntities_OrganizationEntityId",
                table: "FoodTypeEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuEntities_OrganizationEntities_OrganizationEntityId",
                table: "MenuEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntities_OrganizationEntities_OrganizationEntityId",
                table: "OrderEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntities_UserEntity_UserEntityId",
                table: "OrderEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartEntities_OrganizationEntities_OrganizationEntityId",
                table: "ShoppingCartEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartEntities_UserEntity_UserEntityId",
                table: "ShoppingCartEntities");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartEntities_OrganizationEntityId",
                table: "ShoppingCartEntities");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartEntities_UserEntityId",
                table: "ShoppingCartEntities");

            migrationBuilder.DropIndex(
                name: "IX_OrderEntities_OrganizationEntityId",
                table: "OrderEntities");

            migrationBuilder.DropIndex(
                name: "IX_OrderEntities_UserEntityId",
                table: "OrderEntities");

            migrationBuilder.DropIndex(
                name: "IX_MenuEntities_OrganizationEntityId",
                table: "MenuEntities");

            migrationBuilder.DropColumn(
                name: "OrganizationEntityId",
                table: "ShoppingCartEntities");

            migrationBuilder.DropColumn(
                name: "OrderDeliverState",
                table: "OrderEntities");

            migrationBuilder.DropColumn(
                name: "OrganizationEntityId",
                table: "OrderEntities");

            migrationBuilder.DropColumn(
                name: "OrderLockState",
                table: "MenuEntities");

            migrationBuilder.DropColumn(
                name: "ShoppingLimit",
                table: "MenuEntities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FoodTypeEntities");

            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "FoodTypeEntities");

            migrationBuilder.RenameColumn(
                name: "UserEntityId",
                table: "ShoppingCartEntities",
                newName: "OrganizationEntityIdentifier");

            migrationBuilder.RenameColumn(
                name: "UserEntityId",
                table: "OrderEntities",
                newName: "OrganizationEntityIdentifier");

            migrationBuilder.RenameColumn(
                name: "OrganizationEntityId",
                table: "MenuEntities",
                newName: "OrganizationEntityIdentifier");

            migrationBuilder.RenameColumn(
                name: "OrganizationEntityId",
                table: "FoodTypeEntities",
                newName: "OrganizationEntityIdentifier");

            migrationBuilder.RenameIndex(
                name: "IX_FoodTypeEntities_OrganizationEntityId",
                table: "FoodTypeEntities",
                newName: "IX_FoodTypeEntities_OrganizationEntityIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartEntities_OrganizationEntityIdentifier",
                table: "ShoppingCartEntities",
                column: "OrganizationEntityIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntities_OrganizationEntityIdentifier",
                table: "OrderEntities",
                column: "OrganizationEntityIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_MenuEntities_OrganizationEntityIdentifier",
                table: "MenuEntities",
                column: "OrganizationEntityIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodTypeEntities_OrganizationEntities_OrganizationEntityIdentifier",
                table: "FoodTypeEntities",
                column: "OrganizationEntityIdentifier",
                principalTable: "OrganizationEntities",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuEntities_OrganizationEntities_OrganizationEntityIdentifier",
                table: "MenuEntities",
                column: "OrganizationEntityIdentifier",
                principalTable: "OrganizationEntities",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntities_OrganizationEntities_OrganizationEntityIdentifier",
                table: "OrderEntities",
                column: "OrganizationEntityIdentifier",
                principalTable: "OrganizationEntities",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartEntities_OrganizationEntities_OrganizationEntityIdentifier",
                table: "ShoppingCartEntities",
                column: "OrganizationEntityIdentifier",
                principalTable: "OrganizationEntities",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
