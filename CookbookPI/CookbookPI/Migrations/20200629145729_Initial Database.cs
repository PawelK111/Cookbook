using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CookbookPI.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID_Category = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID_Category);
                });

            migrationBuilder.CreateTable(
                name: "Difficulty",
                columns: table => new
                {
                    ID_Difficulty = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulty", x => x.ID_Difficulty);
                });

            migrationBuilder.CreateTable(
                name: "NumberOfPeople",
                columns: table => new
                {
                    ID_NumberOfPeople = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfPeople", x => x.ID_NumberOfPeople);
                });

            migrationBuilder.CreateTable(
                name: "TimeOfPrepares",
                columns: table => new
                {
                    ID_TimeOfPrepares = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeOfPrepare = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOfPrepares", x => x.ID_TimeOfPrepares);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfKitchen",
                columns: table => new
                {
                    ID_TypeOfKitchen = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfKitchen", x => x.ID_TypeOfKitchen);
                });

            migrationBuilder.CreateTable(
                name: "User_Permissions",
                columns: table => new
                {
                    ID_Permission = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Permissions", x => x.ID_Permission);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID_User = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_Permission = table.Column<int>(nullable: false),
                    Nickname = table.Column<string>(nullable: true),
                    Passwrd = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateOfRegistration = table.Column<DateTime>(nullable: false),
                    isBanned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID_User);
                    table.ForeignKey(
                        name: "FK_Users_User_Permissions_ID_Permission",
                        column: x => x.ID_Permission,
                        principalTable: "User_Permissions",
                        principalColumn: "ID_Permission",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    ID_Recipe = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_User = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ID_TypeOfKitchen = table.Column<int>(nullable: false),
                    ID_Category = table.Column<int>(nullable: false),
                    ID_Difficulty = table.Column<int>(nullable: false),
                    ID_NumberOfPeople = table.Column<int>(nullable: false),
                    ID_TimeOfPrepare = table.Column<int>(nullable: false),
                    IsAccepted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Instruction = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.ID_Recipe);
                    table.ForeignKey(
                        name: "FK_Recipes_Categories_ID_Category",
                        column: x => x.ID_Category,
                        principalTable: "Categories",
                        principalColumn: "ID_Category",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Difficulty_ID_Difficulty",
                        column: x => x.ID_Difficulty,
                        principalTable: "Difficulty",
                        principalColumn: "ID_Difficulty",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_NumberOfPeople_ID_NumberOfPeople",
                        column: x => x.ID_NumberOfPeople,
                        principalTable: "NumberOfPeople",
                        principalColumn: "ID_NumberOfPeople",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_TimeOfPrepares_ID_TimeOfPrepare",
                        column: x => x.ID_TimeOfPrepare,
                        principalTable: "TimeOfPrepares",
                        principalColumn: "ID_TimeOfPrepares",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_TypeOfKitchen_ID_TypeOfKitchen",
                        column: x => x.ID_TypeOfKitchen,
                        principalTable: "TypeOfKitchen",
                        principalColumn: "ID_TypeOfKitchen",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Users_ID_User",
                        column: x => x.ID_User,
                        principalTable: "Users",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ID_Component = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ID_Recipe = table.Column<int>(nullable: false),
                    NameOfComponent = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ID_Component);
                    table.ForeignKey(
                        name: "FK_Components_Recipes_ID_Recipe",
                        column: x => x.ID_Recipe,
                        principalTable: "Recipes",
                        principalColumn: "ID_Recipe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ID_Recipe",
                table: "Components",
                column: "ID_Recipe");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ID_Category",
                table: "Recipes",
                column: "ID_Category");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ID_Difficulty",
                table: "Recipes",
                column: "ID_Difficulty");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ID_NumberOfPeople",
                table: "Recipes",
                column: "ID_NumberOfPeople");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ID_TimeOfPrepare",
                table: "Recipes",
                column: "ID_TimeOfPrepare");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ID_TypeOfKitchen",
                table: "Recipes",
                column: "ID_TypeOfKitchen");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ID_User",
                table: "Recipes",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ID_Permission",
                table: "Users",
                column: "ID_Permission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Difficulty");

            migrationBuilder.DropTable(
                name: "NumberOfPeople");

            migrationBuilder.DropTable(
                name: "TimeOfPrepares");

            migrationBuilder.DropTable(
                name: "TypeOfKitchen");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "User_Permissions");
        }
    }
}
