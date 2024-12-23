using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatAdoptionMobileApp.EntityFramework.CatAdoptionMobileApp.EntityFramework
{
    /// <inheritdoc />
    public partial class UserFavoritesTableModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavorites",
                table: "UserFavorites");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "UserFavorites",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavorites",
                table: "UserFavorites",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavorites",
                table: "UserFavorites");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserFavorites");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavorites",
                table: "UserFavorites",
                columns: new[] { "UserId", "CatId" });
        }
    }
}
