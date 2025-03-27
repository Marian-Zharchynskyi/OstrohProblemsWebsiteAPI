using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changedCommentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comments_problems_problem_id",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "fk_problems_problem_categories_problem_category_id",
                table: "problems");

            migrationBuilder.DropIndex(
                name: "ix_problems_problem_category_id",
                table: "problems");

            migrationBuilder.DropIndex(
                name: "ix_comments_problem_id",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "problem_category_id",
                table: "problems");

            migrationBuilder.DropColumn(
                name: "problem_id",
                table: "comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "problem_category_id",
                table: "problems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "problem_id",
                table: "comments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_problems_problem_category_id",
                table: "problems",
                column: "problem_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_problem_id",
                table: "comments",
                column: "problem_id");

            migrationBuilder.AddForeignKey(
                name: "fk_comments_problems_problem_id",
                table: "comments",
                column: "problem_id",
                principalTable: "problems",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_problems_problem_categories_problem_category_id",
                table: "problems",
                column: "problem_category_id",
                principalTable: "problem_categories",
                principalColumn: "id");
        }
    }
}
