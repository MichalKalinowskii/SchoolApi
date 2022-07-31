using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolAPI.Migrations
{
    public partial class SubjectsTaughtByTeacher_Added_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "SubjectsTaughtByTeacher",
                columns: table => new
                {
                    SubjectsTaughtByTeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectsTaughtByTeacher", x => x.SubjectsTaughtByTeacherId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_SubjectsTaughtByTeacher_SubjectsTaughtByTeacherId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_SubjectsTaughtByTeacher_SubjectsTaughtByTeacherId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "SubjectsTaughtByTeacher");

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
        }
    }
}
