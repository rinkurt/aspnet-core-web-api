using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class NewCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Employees_Companies_CompanyId",
            //     table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"));

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("080d684e-21aa-46d8-8eb8-8c6709879f60"), "Don't be evil", "Google1" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("080d684e-21aa-46d8-8eb8-8c6709879f61"), "Don't be evil", "Google2" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("080d684e-21aa-46d8-8eb8-8c6709879f62"), "Don't be evil", "Google3" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("080d684e-21aa-46d8-8eb8-8c6709879f63"), "Don't be evil", "Google4" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c3b"), new Guid("080d684e-21aa-46d8-8eb8-8c6709879f60"), new DateTime(1977, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "G097", "Kevin", 1, "Richardson" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c40"), new Guid("080d684e-21aa-46d8-8eb8-8c6709879f61"), new DateTime(1977, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "G097", "Kevin", 1, "Richardson" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c41"), new Guid("080d684e-21aa-46d8-8eb8-8c6709879f62"), new DateTime(1977, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "G097", "Kevin", 1, "Richardson" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c42"), new Guid("080d684e-21aa-46d8-8eb8-8c6709879f63"), new DateTime(1977, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "G097", "Kevin", 1, "Richardson" });

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Employees_Companies_CompanyId",
            //     table: "Employees",
            //     column: "CompanyId",
            //     principalTable: "Companies",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Employees_Companies_CompanyId",
            //     table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c3b"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c40"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c41"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c42"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("080d684e-21aa-46d8-8eb8-8c6709879f60"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("080d684e-21aa-46d8-8eb8-8c6709879f61"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("080d684e-21aa-46d8-8eb8-8c6709879f62"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("080d684e-21aa-46d8-8eb8-8c6709879f63"));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"), new Guid("080d684e-21aa-46d8-8eb8-8c6709879f59"), new DateTime(1977, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "G097", "Kevin", 1, "Richardson" });

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Employees_Companies_CompanyId",
            //     table: "Employees",
            //     column: "CompanyId",
            //     principalTable: "Companies",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Restrict);
        }
    }
}
