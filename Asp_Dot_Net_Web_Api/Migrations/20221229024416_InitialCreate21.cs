﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspDotNetWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Book");
        }
    }
}
