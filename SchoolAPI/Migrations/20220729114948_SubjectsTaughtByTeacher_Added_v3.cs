using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolAPI.Migrations
{
    public partial class SubjectsTaughtByTeacher_Added_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_SubjectsTaughtByTeacher_SubjectsTaughtByTeacherId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_SubjectsTaughtByTeacher_SubjectsTaughtByTeacherId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SubjectsTaughtByTeacherId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SubjectsTaughtByTeacherId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjectsTaughtByTeacherId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SubjectsTaughtByTeacherId",
                table: "Subjects");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsTaughtByTeacher_SubjectId",
                table: "SubjectsTaughtByTeacher",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsTaughtByTeacher_TeacherId",
                table: "SubjectsTaughtByTeacher",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsTaughtByTeacher_Subjects_SubjectId",
                table: "SubjectsTaughtByTeacher",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsTaughtByTeacher_Teachers_TeacherId",
                table: "SubjectsTaughtByTeacher",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsTaughtByTeacher_Subjects_SubjectId",
                table: "SubjectsTaughtByTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsTaughtByTeacher_Teachers_TeacherId",
                table: "SubjectsTaughtByTeacher");

            migrationBuilder.DropIndex(
                name: "IX_SubjectsTaughtByTeacher_SubjectId",
                table: "SubjectsTaughtByTeacher");

            migrationBuilder.DropIndex(
                name: "IX_SubjectsTaughtByTeacher_TeacherId",
                table: "SubjectsTaughtByTeacher");

            migrationBuilder.AddColumn<int>(
                name: "SubjectsTaughtByTeacherId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectsTaughtByTeacherId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SubjectsTaughtByTeacherId",
                table: "Teachers",
                column: "SubjectsTaughtByTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectsTaughtByTeacherId",
                table: "Subjects",
                column: "SubjectsTaughtByTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_SubjectsTaughtByTeacher_SubjectsTaughtByTeacherId",
                table: "Subjects",
                column: "SubjectsTaughtByTeacherId",
                principalTable: "SubjectsTaughtByTeacher",
                principalColumn: "SubjectsTaughtByTeacherId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_SubjectsTaughtByTeacher_SubjectsTaughtByTeacherId",
                table: "Teachers",
                column: "SubjectsTaughtByTeacherId",
                principalTable: "SubjectsTaughtByTeacher",
                principalColumn: "SubjectsTaughtByTeacherId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
