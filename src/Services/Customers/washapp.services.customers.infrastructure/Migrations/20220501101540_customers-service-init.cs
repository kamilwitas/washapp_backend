using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace washapp.services.customers.infrastructure.Migrations
{
    public partial class customersserviceinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "customers-service");

            migrationBuilder.CreateTable(
                name: "AssortmentCategories",
                schema: "customers-service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssortmentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "customers-service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "customers-service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "customers-service",
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "customers-service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "customers-service",
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assortments",
                schema: "customers-service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssortmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    MeasurementUnit = table.Column<int>(type: "int", nullable: false),
                    WeightUnit = table.Column<int>(type: "int", nullable: false),
                    AssortmentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assortments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assortments_AssortmentCategories_AssortmentCategoryId",
                        column: x => x.AssortmentCategoryId,
                        principalSchema: "customers-service",
                        principalTable: "AssortmentCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assortments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "customers-service",
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_LocationId",
                schema: "customers-service",
                table: "Addresses",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Assortments_AssortmentCategoryId",
                schema: "customers-service",
                table: "Assortments",
                column: "AssortmentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Assortments_CustomerId",
                schema: "customers-service",
                table: "Assortments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                schema: "customers-service",
                table: "Customers",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assortments",
                schema: "customers-service");

            migrationBuilder.DropTable(
                name: "AssortmentCategories",
                schema: "customers-service");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "customers-service");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "customers-service");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "customers-service");
        }
    }
}
