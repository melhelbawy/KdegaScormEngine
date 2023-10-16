using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kdega.ScormEngine.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCmiCoreToLearnerScormPackagesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CmiData_ScormPackages_ScormPackageId",
                table: "CmiData");

            migrationBuilder.DropForeignKey(
                name: "FK_LearnerScormPackages_CmiCores_CmiCoreId",
                table: "LearnerScormPackages");

            migrationBuilder.DropIndex(
                name: "IX_LearnerScormPackages_CmiCoreId",
                table: "LearnerScormPackages");

            migrationBuilder.DropIndex(
                name: "IX_CmiData_ScormPackageId",
                table: "CmiData");

            migrationBuilder.DropColumn(
                name: "CmiCoreId",
                table: "LearnerScormPackages");

            migrationBuilder.DropColumn(
                name: "ScormContentId",
                table: "CmiData");

            migrationBuilder.DropColumn(
                name: "ScormPackageId",
                table: "CmiData");

            migrationBuilder.CreateIndex(
                name: "IX_CmiCores_LearnerScormPackageId",
                table: "CmiCores",
                column: "LearnerScormPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CmiCores_LearnerScormPackages_LearnerScormPackageId",
                table: "CmiCores",
                column: "LearnerScormPackageId",
                principalTable: "LearnerScormPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CmiCores_LearnerScormPackages_LearnerScormPackageId",
                table: "CmiCores");

            migrationBuilder.DropIndex(
                name: "IX_CmiCores_LearnerScormPackageId",
                table: "CmiCores");

            migrationBuilder.AddColumn<Guid>(
                name: "CmiCoreId",
                table: "LearnerScormPackages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ScormContentId",
                table: "CmiData",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ScormPackageId",
                table: "CmiData",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_LearnerScormPackages_CmiCoreId",
                table: "LearnerScormPackages",
                column: "CmiCoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CmiData_ScormPackageId",
                table: "CmiData",
                column: "ScormPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CmiData_ScormPackages_ScormPackageId",
                table: "CmiData",
                column: "ScormPackageId",
                principalTable: "ScormPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LearnerScormPackages_CmiCores_CmiCoreId",
                table: "LearnerScormPackages",
                column: "CmiCoreId",
                principalTable: "CmiCores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
