using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;
using ConsoleApp1.FileAccessor.Database.Context;
namespace ConsoleApp1.FileAccessor.Database.Migrations
{
    public partial class InsertUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "SQL", @"4-InsertUsers.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from users");
        }
    }
}
