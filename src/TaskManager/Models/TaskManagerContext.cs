using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models;

public partial class TaskManagerContext : DbContext
{
    public TaskManagerContext()
    {
    }

    public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskStatusLog> TaskStatusLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccess> UserAccesses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TaskManager;User ID=postgres;Password='1qaz!QAZ'");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("attachments_pkey");

            entity.ToTable("attachments");

            entity.HasIndex(e => e.TaskId, "idx_attachments_task_id");

            entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Task).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("attachments_task_id_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.DateOfCreate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_of_create");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.ShortName)
                .HasMaxLength(50)
                .HasColumnName("short_name");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("tasks_pkey");

            entity.ToTable("tasks");

            entity.HasIndex(e => e.ProjectId, "idx_tasks_project_id");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Deadline)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ReviewerId).HasColumnName("reviewer_id");
            entity.Property(e => e.TesterId).HasColumnName("tester_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Assignment).WithMany(p => p.TaskAssignments)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("tasks_assignment_id_fkey");

            entity.HasOne(d => d.Author).WithMany(p => p.TaskAuthors)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("tasks_author_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("tasks_project_id_fkey");

            entity.HasOne(d => d.Reviewer).WithMany(p => p.TaskReviewers)
                .HasForeignKey(d => d.ReviewerId)
                .HasConstraintName("tasks_reviewer_id_fkey");

            entity.HasOne(d => d.Tester).WithMany(p => p.TaskTesters)
                .HasForeignKey(d => d.TesterId)
                .HasConstraintName("tasks_tester_id_fkey");
        });

        modelBuilder.Entity<TaskStatusLog>(entity =>
        {
            entity.HasKey(e => e.TaskStatusId).HasName("taskstatuslog_pkey");

            entity.ToTable("taskstatuslog");

            entity.HasIndex(e => e.TaskId, "idx_task_status_log_task_id");

            entity.Property(e => e.TaskStatusId).HasColumnName("task_status_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.DateUpdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_update");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskStatusLogs)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("taskstatuslog_task_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TaskStatusLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("taskstatuslog_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserAccess>(entity =>
        {
            entity.HasKey(e => e.UserAccessId).HasName("useraccesses_pkey");

            entity.ToTable("useraccesses");

            entity.HasIndex(e => e.ProjectId, "idx_user_accesses_project_id");

            entity.HasIndex(e => e.UserId, "idx_user_accesses_user_id");

            entity.HasIndex(e => new { e.UserId, e.ProjectId }, "unique_user_project").IsUnique();

            entity.Property(e => e.UserAccessId).HasColumnName("user_access_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Project).WithMany(p => p.UserAccesses)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("useraccesses_project_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserAccesses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("useraccesses_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
