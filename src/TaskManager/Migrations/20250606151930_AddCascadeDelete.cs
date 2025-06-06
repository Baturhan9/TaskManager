using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "attachments_task_id_fkey",
                table: "attachments");

            migrationBuilder.DropForeignKey(
                name: "taskstatuslog_task_id_fkey",
                table: "taskstatuslog");

            migrationBuilder.AddForeignKey(
                name: "attachments_task_id_fkey",
                table: "attachments",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "task_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "taskstatuslog_task_id_fkey",
                table: "taskstatuslog",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "task_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "attachments_task_id_fkey",
                table: "attachments");

            migrationBuilder.DropForeignKey(
                name: "taskstatuslog_task_id_fkey",
                table: "taskstatuslog");

            migrationBuilder.AddForeignKey(
                name: "attachments_task_id_fkey",
                table: "attachments",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "task_id");

            migrationBuilder.AddForeignKey(
                name: "taskstatuslog_task_id_fkey",
                table: "taskstatuslog",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "task_id");
        }
    }
}
