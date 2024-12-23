using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatAdoptionMobileApp.EntityFramework.CatAdoptionMobileApp.EntityFramework
{
    /// <inheritdoc />
    public partial class UserAdoptionModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAdoptions_Cats_CatId1",
                table: "UserAdoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAdoptions_Users_UserId1",
                table: "UserAdoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Cats_CatId1",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Users_UserId1",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorites_CatId1",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorites_UserId1",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserAdoptions_CatId1",
                table: "UserAdoptions");

            migrationBuilder.DropIndex(
                name: "IX_UserAdoptions_UserId1",
                table: "UserAdoptions");

            migrationBuilder.DropColumn(
                name: "CatId1",
                table: "UserFavorites");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserFavorites");

            migrationBuilder.DropColumn(
                name: "CatId1",
                table: "UserAdoptions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserAdoptions");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserFavorites",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "CatId",
                table: "UserFavorites",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserAdoptions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "CatId",
                table: "UserAdoptions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_CatId",
                table: "UserFavorites",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_UserId",
                table: "UserFavorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdoptions_CatId",
                table: "UserAdoptions",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdoptions_UserId",
                table: "UserAdoptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdoptions_Cats_CatId",
                table: "UserAdoptions",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdoptions_Users_UserId",
                table: "UserAdoptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Cats_CatId",
                table: "UserFavorites",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Users_UserId",
                table: "UserFavorites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAdoptions_Cats_CatId",
                table: "UserAdoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAdoptions_Users_UserId",
                table: "UserAdoptions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Cats_CatId",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Users_UserId",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorites_CatId",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorites_UserId",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserAdoptions_CatId",
                table: "UserAdoptions");

            migrationBuilder.DropIndex(
                name: "IX_UserAdoptions_UserId",
                table: "UserAdoptions");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserFavorites",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CatId",
                table: "UserFavorites",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CatId1",
                table: "UserFavorites",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "UserFavorites",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserAdoptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CatId",
                table: "UserAdoptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CatId1",
                table: "UserAdoptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "UserAdoptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_CatId1",
                table: "UserFavorites",
                column: "CatId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_UserId1",
                table: "UserFavorites",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdoptions_CatId1",
                table: "UserAdoptions",
                column: "CatId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdoptions_UserId1",
                table: "UserAdoptions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdoptions_Cats_CatId1",
                table: "UserAdoptions",
                column: "CatId1",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdoptions_Users_UserId1",
                table: "UserAdoptions",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Cats_CatId1",
                table: "UserFavorites",
                column: "CatId1",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Users_UserId1",
                table: "UserFavorites",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
