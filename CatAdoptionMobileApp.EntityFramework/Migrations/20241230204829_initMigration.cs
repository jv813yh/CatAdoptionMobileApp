using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatAdoptionMobileApp.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false),
                    AdoptionStatus = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAdoptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CatId = table.Column<int>(type: "int", nullable: false),
                    AdoptedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdoptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAdoptions_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAdoptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavorites_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "AdoptionStatus", "Breed", "DateOfBirth", "Description", "Gender", "ImageUrl", "IsActive", "Name", "Price", "Views" },
                values: new object[,]
                {
                    { 1, 0, "Siamese", new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fluffy is a very friendly cat. She loves to play with kids and other cats. She is very active and loves to be around people.", 0, "https://cdn.pixabay.com/photo/2014/11/30/14/11/cat-551554_1280.jpg", false, "Fluffy", 250.0, 0 },
                    { 2, 0, "Persian", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Whiskers is a very calm and quiet cat. He loves to sleep and relax. He is very friendly and loves to be pet", 0, "https://cdn.pixabay.com/photo/2022/01/18/07/38/cat-6946505_1280.jpg", false, "Whiskers", 150.0, 0 },
                    { 3, 0, "Tabby", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mittens is a very playful cat. She loves to run around and play with toys. She is very friendly and loves to be around people.", 0, "https://cdn.pixabay.com/photo/2021/10/27/19/09/cat-6748193_1280.jpg", false, "Mittens", 100.0, 0 },
                    { 4, 0, "British Shorthair", new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Socks is a quiet and dignified cat. She enjoys spending time with her family and has a soft, round face.", 0, "https://cdn.pixabay.com/photo/2022/01/18/07/36/cat-6946498_1280.jpg", false, "Socks", 110.0, 0 },
                    { 5, 0, "Bengal", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bella is an energetic and curious cat. She loves to explore and is very intelligent, often figuring out how to open doors.", 1, "https://cdn.pixabay.com/photo/2022/06/20/16/54/cat-7274205_1280.jpg", false, "Bella", 140.0, 0 },
                    { 6, 0, "Orange Tabby", new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ginger is a sweet, calm cat who loves to be pampered. She enjoys napping in the sun and curling up on soft blankets.", 1, "https://cdn.pixabay.com/photo/2023/09/21/17/05/european-shorthair-8267220_1280.jpg", false, "Ginger", 95.0, 0 },
                    { 7, 0, "Russian Blue", new DateTime(2023, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smokey is a quiet, reserved cat. He is known for his beautiful silvery coat and enjoys being petted when he’s in the mood.", 1, "https://cdn.pixabay.com/photo/2021/12/01/18/17/cat-6838844_1280.jpg", false, "Smokey", 125.0, 0 },
                    { 8, 0, "Ragdoll", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oliver is a gentle giant with a calm and loving nature. He’s very affectionate and loves to be held like a baby.", 0, "https://cdn.pixabay.com/photo/2023/04/07/07/14/cat-7905702_1280.jpg", false, "Oliver", 150.0, 0 },
                    { 9, 0, "Abyssinian", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luna is a playful and active cat. She loves to climb and jump, and she is always full of energy.", 1, "https://cdn.pixabay.com/photo/2022/11/11/09/26/cat-7584624_1280.jpg", false, "Luna", 105.0, 0 },
                    { 10, 0, "Sphynx", new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Milo is an outgoing and social cat. Despite having no fur, he is very warm and enjoys being around people.", 0, "https://cdn.pixabay.com/photo/2015/11/16/22/14/cat-1046544_1280.jpg", false, "Milo", 95.0, 0 },
                    { 11, 0, "Scottish Fold", new DateTime(2023, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toby is a calm and loving cat with distinctive folded ears. He enjoys lounging around and being spoiled with treats.", 0, "https://cdn.pixabay.com/photo/2017/08/07/12/27/cat-2603300_1280.jpg", false, "Toby", 115.0, 0 },
                    { 12, 0, "Birman", new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chloe is an elegant cat with a luxurious coat. She loves being pampered and enjoys a good scratch behind the ears.", 0, "https://cdn.pixabay.com/photo/2020/11/22/17/28/cat-5767334_1280.jpg", false, "Chloe", 130.0, 0 },
                    { 13, 0, "Burmese", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leo is an affectionate and playful cat. He loves attention and follows his owner around the house.", 0, "https://cdn.pixabay.com/photo/2022/07/25/15/18/cat-7344042_1280.jpg", false, "Leo", 140.0, 0 },
                    { 14, 0, "Egyptian Mau", new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Penny is a lively and graceful cat with a striking spotted coat. She’s very active and loves to play with interactive toys.", 1, "https://cdn.pixabay.com/photo/2019/06/08/17/02/cat-4260536_1280.jpg", false, "Penny", 120.0, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAdoptions_CatId",
                table: "UserAdoptions",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdoptions_UserId",
                table: "UserAdoptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_CatId",
                table: "UserFavorites",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_UserId",
                table: "UserFavorites",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAdoptions");

            migrationBuilder.DropTable(
                name: "UserFavorites");

            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
