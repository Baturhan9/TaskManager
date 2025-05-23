using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    short_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    date_of_create = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("projects_pkey", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    author_id = table.Column<int>(type: "integer", nullable: true),
                    reviewer_id = table.Column<int>(type: "integer", nullable: true),
                    tester_id = table.Column<int>(type: "integer", nullable: true),
                    assignment_id = table.Column<int>(type: "integer", nullable: true),
                    project_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tasks_pkey", x => x.task_id);
                    table.ForeignKey(
                        name: "tasks_assignment_id_fkey",
                        column: x => x.assignment_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "tasks_author_id_fkey",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "tasks_project_id_fkey",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "tasks_reviewer_id_fkey",
                        column: x => x.reviewer_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "tasks_tester_id_fkey",
                        column: x => x.tester_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "useraccesses",
                columns: table => new
                {
                    user_access_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    project_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("useraccesses_pkey", x => x.user_access_id);
                    table.ForeignKey(
                        name: "useraccesses_project_id_fkey",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "useraccesses_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    attachment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    task_id = table.Column<int>(type: "integer", nullable: true),
                    file_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("attachments_pkey", x => x.attachment_id);
                    table.ForeignKey(
                        name: "attachments_task_id_fkey",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateTable(
                name: "taskstatuslog",
                columns: table => new
                {
                    task_status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    task_id = table.Column<int>(type: "integer", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    date_update = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("taskstatuslog_pkey", x => x.task_status_id);
                    table.ForeignKey(
                        name: "taskstatuslog_task_id_fkey",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id");
                    table.ForeignKey(
                        name: "taskstatuslog_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "idx_attachments_task_id",
                table: "attachments",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_project_id",
                table: "tasks",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_assignment_id",
                table: "tasks",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_author_id",
                table: "tasks",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_reviewer_id",
                table: "tasks",
                column: "reviewer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_tester_id",
                table: "tasks",
                column: "tester_id");

            migrationBuilder.CreateIndex(
                name: "idx_task_status_log_task_id",
                table: "taskstatuslog",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_taskstatuslog_user_id",
                table: "taskstatuslog",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "idx_user_accesses_project_id",
                table: "useraccesses",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "idx_user_accesses_user_id",
                table: "useraccesses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "unique_user_project",
                table: "useraccesses",
                columns: new[] { "user_id", "project_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_email_key",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "taskstatuslog");

            migrationBuilder.DropTable(
                name: "useraccesses");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
