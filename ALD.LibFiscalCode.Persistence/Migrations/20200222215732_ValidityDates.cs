using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ALD.LibFiscalCode.Persistence.Migrations
{
    public partial class ValidityDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime?>("start_date", "Places", "TEXT", nullable: true, defaultValue: null);
            migrationBuilder.AddColumn<DateTime?>("end_date", "Places", "TEXT", nullable: true, defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
