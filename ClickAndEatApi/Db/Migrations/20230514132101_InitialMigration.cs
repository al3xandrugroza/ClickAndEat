using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickAndEatApi.Db.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationEntities",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationEntities", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "FoodTypeEntities",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypeEntities", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_FoodTypeEntities_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.Sql("exec('UPDATE pt SET pt.Identifier = NEWID() FROM dbo.FoodTypeEntities pt')");

            migrationBuilder.CreateTable(
                name: "MenuEntities",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingLimit = table.Column<int>(type: "int", nullable: false),
                    OrderLockState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuEntities", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_MenuEntities_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.Sql("exec('UPDATE pt SET pt.Identifier = NEWID() FROM dbo.MenuEntities pt')");

            migrationBuilder.CreateTable(
                name: "UserEntities",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_UserEntities_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FoodTypeEntityMenuEntity",
                columns: table => new
                {
                    FoodTypeEntitiesIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuEntitiesIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypeEntityMenuEntity", x => new { x.FoodTypeEntitiesIdentifier, x.MenuEntitiesIdentifier });
                    table.ForeignKey(
                        name: "FK_FoodTypeEntityMenuEntity_FoodTypeEntities_FoodTypeEntitiesIdentifier",
                        column: x => x.FoodTypeEntitiesIdentifier,
                        principalTable: "FoodTypeEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodTypeEntityMenuEntity_MenuEntities_MenuEntitiesIdentifier",
                        column: x => x.MenuEntitiesIdentifier,
                        principalTable: "MenuEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntities",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDeliverState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntities", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_OrderEntities_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderEntities_UserEntities_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "UserEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.Sql("exec('UPDATE pt SET pt.Identifier = NEWID() FROM dbo.OrderEntities pt')");

            migrationBuilder.CreateTable(
                name: "ShoppingCartEntities",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartEntities", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_ShoppingCartEntities_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ShoppingCartEntities_UserEntities_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "UserEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.Sql("exec('UPDATE pt SET pt.Identifier = NEWID() FROM dbo.ShoppingCartEntities pt')");

            migrationBuilder.CreateTable(
                name: "FoodTypeEntityOrderEntity",
                columns: table => new
                {
                    FoodTypeEntitiesIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderEntitiesIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypeEntityOrderEntity", x => new { x.FoodTypeEntitiesIdentifier, x.OrderEntitiesIdentifier });
                    table.ForeignKey(
                        name: "FK_FoodTypeEntityOrderEntity_FoodTypeEntities_FoodTypeEntitiesIdentifier",
                        column: x => x.FoodTypeEntitiesIdentifier,
                        principalTable: "FoodTypeEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodTypeEntityOrderEntity_OrderEntities_OrderEntitiesIdentifier",
                        column: x => x.OrderEntitiesIdentifier,
                        principalTable: "OrderEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodTypeEntityShoppingCartEntity",
                columns: table => new
                {
                    FoodTypeEntitiesIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingCartEntitiesIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypeEntityShoppingCartEntity", x => new { x.FoodTypeEntitiesIdentifier, x.ShoppingCartEntitiesIdentifier });
                    table.ForeignKey(
                        name: "FK_FoodTypeEntityShoppingCartEntity_FoodTypeEntities_FoodTypeEntitiesIdentifier",
                        column: x => x.FoodTypeEntitiesIdentifier,
                        principalTable: "FoodTypeEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodTypeEntityShoppingCartEntity_ShoppingCartEntities_ShoppingCartEntitiesIdentifier",
                        column: x => x.ShoppingCartEntitiesIdentifier,
                        principalTable: "ShoppingCartEntities",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodTypeEntities_OrganizationEntityId",
                table: "FoodTypeEntities",
                column: "OrganizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTypeEntities_Type_OrganizationEntityId",
                table: "FoodTypeEntities",
                columns: new[] { "Type", "OrganizationEntityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodTypeEntityMenuEntity_MenuEntitiesIdentifier",
                table: "FoodTypeEntityMenuEntity",
                column: "MenuEntitiesIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTypeEntityOrderEntity_OrderEntitiesIdentifier",
                table: "FoodTypeEntityOrderEntity",
                column: "OrderEntitiesIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTypeEntityShoppingCartEntity_ShoppingCartEntitiesIdentifier",
                table: "FoodTypeEntityShoppingCartEntity",
                column: "ShoppingCartEntitiesIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_MenuEntities_OrganizationEntityId",
                table: "MenuEntities",
                column: "OrganizationEntityId",
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
                name: "IX_ShoppingCartEntities_OrganizationEntityId",
                table: "ShoppingCartEntities",
                column: "OrganizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartEntities_UserEntityId",
                table: "ShoppingCartEntities",
                column: "UserEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserEntities_OrganizationEntityId",
                table: "UserEntities",
                column: "OrganizationEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodTypeEntityMenuEntity");

            migrationBuilder.DropTable(
                name: "FoodTypeEntityOrderEntity");

            migrationBuilder.DropTable(
                name: "FoodTypeEntityShoppingCartEntity");

            migrationBuilder.DropTable(
                name: "MenuEntities");

            migrationBuilder.DropTable(
                name: "OrderEntities");

            migrationBuilder.DropTable(
                name: "FoodTypeEntities");

            migrationBuilder.DropTable(
                name: "ShoppingCartEntities");

            migrationBuilder.DropTable(
                name: "UserEntities");

            migrationBuilder.DropTable(
                name: "OrganizationEntities");
        }
    }
}
