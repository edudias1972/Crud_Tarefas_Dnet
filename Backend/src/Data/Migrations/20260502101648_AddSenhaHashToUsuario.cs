using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace config.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSenhaHashToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuarios",
                newName: "SenhaHash");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Tarefas",
                newName: "CriadaEm");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConcluidaEm",
                table: "Tarefas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioId",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "ConcluidaEm",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "SenhaHash",
                table: "Usuarios",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "CriadaEm",
                table: "Tarefas",
                newName: "DataCriacao");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Tarefas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
