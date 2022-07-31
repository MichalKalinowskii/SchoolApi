using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolAPI.Migrations
{
    public partial class ModifyModel_SubjectsTaughtByTeacher_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectsTaughtByTeacherTeacher");

            migrationBuilder.DropTable(
                name: "SubjectSubjectsTaughtByTeacher");

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

            migrationBuilder.CreateTable(
                name: "SubjectsTaughtByTeacherTeacher",
                columns: table => new
                {
                    SubjectsTaughtByTeacherId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectsTaughtByTeacherTeacher", x => new { x.SubjectsTaughtByTeacherId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_SubjectsTaughtByTeacherTeacher_SubjectsTaughtByTeacher_SubjectsTaughtByTeacherId",
                        column: x => x.SubjectsTaughtByTeacherId,
                        principalTable: "SubjectsTaughtByTeacher",
                        principalColumn: "SubjectsTaughtByTeacherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectsTaughtByTeacherTeacher_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectSubjectsTaughtByTeacher",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    SubjectsTaughtByTeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectSubjectsTaughtByTeacher", x => new { x.SubjectId, x.SubjectsTaughtByTeacherId });
                    table.ForeignKey(
                        name: "FK_SubjectSubjectsTaughtByTeacher_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectSubjectsTaughtByTeacher_SubjectsTaughtByTeacher_SubjectsTaughtByTeacherId",
                        column: x => x.SubjectsTaughtByTeacherId,
                        principalTable: "SubjectsTaughtByTeacher",
                        principalColumn: "SubjectsTaughtByTeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsTaughtByTeacherTeacher_TeacherId",
                table: "SubjectsTaughtByTeacherTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectSubjectsTaughtByTeacher_SubjectsTaughtByTeacherId",
                table: "SubjectSubjectsTaughtByTeacher",
                column: "SubjectsTaughtByTeacherId");
        }
    }
}
