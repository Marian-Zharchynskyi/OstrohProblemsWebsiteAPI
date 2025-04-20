using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_problem_rating_problems_problem_id",
                table: "problem_rating");

            migrationBuilder.DropPrimaryKey(
                name: "pk_problem_rating",
                table: "problem_rating");

            migrationBuilder.RenameTable(
                name: "problem_rating",
                newName: "problem_ratings");

            migrationBuilder.RenameIndex(
                name: "ix_problem_rating_problem_id_user_id",
                table: "problem_ratings",
                newName: "ix_problem_ratings_problem_id_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_problem_ratings",
                table: "problem_ratings",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_problem_ratings_problems_problem_id",
                table: "problem_ratings",
                column: "problem_id",
                principalTable: "problems",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_problem_ratings_problems_problem_id",
                table: "problem_ratings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_problem_ratings",
                table: "problem_ratings");

            migrationBuilder.RenameTable(
                name: "problem_ratings",
                newName: "problem_rating");

            migrationBuilder.RenameIndex(
                name: "ix_problem_ratings_problem_id_user_id",
                table: "problem_rating",
                newName: "ix_problem_rating_problem_id_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_problem_rating",
                table: "problem_rating",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_problem_rating_problems_problem_id",
                table: "problem_rating",
                column: "problem_id",
                principalTable: "problems",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
