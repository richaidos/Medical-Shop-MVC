using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Medical_Shop_MVC.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medical_Enterprise",
                columns: table => new
                {
                    MedID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MedName = table.Column<string>(nullable: true),
                    MedDescription = table.Column<string>(nullable: true),
                    MedAddress = table.Column<string>(nullable: true),
                    Time_at = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medical_Enterprise", x => x.MedID);
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    MedicineCode = table.Column<string>(nullable: true),
                    Img = table.Column<string>(nullable: true),
                    Use_in_case = table.Column<string>(nullable: true),
                    Contradication = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineID);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    PharmID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PharmName = table.Column<string>(nullable: true),
                    PharmAddress = table.Column<string>(nullable: true),
                    PharmPhone = table.Column<string>(nullable: true),
                    Time_at = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.PharmID);
                });

            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    SpecID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SpecName = table.Column<string>(nullable: true),
                    SpecDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.SpecID);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DoctorName = table.Column<string>(nullable: true),
                    DoctorSurname = table.Column<string>(nullable: true),
                    DoctorDescription = table.Column<string>(nullable: true),
                    doctorTypeSpecID = table.Column<int>(nullable: true),
                    DoctorPhone = table.Column<string>(nullable: true),
                    DoctorEnterpriseMedID = table.Column<int>(nullable: true),
                    Price = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorID);
                    table.ForeignKey(
                        name: "FK_Doctors_Medical_Enterprise_DoctorEnterpriseMedID",
                        column: x => x.DoctorEnterpriseMedID,
                        principalTable: "Medical_Enterprise",
                        principalColumn: "MedID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_Specialization_doctorTypeSpecID",
                        column: x => x.doctorTypeSpecID,
                        principalTable: "Specialization",
                        principalColumn: "SpecID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DoctorEnterpriseMedID",
                table: "Doctors",
                column: "DoctorEnterpriseMedID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_doctorTypeSpecID",
                table: "Doctors",
                column: "doctorTypeSpecID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "Pharmacy");

            migrationBuilder.DropTable(
                name: "Medical_Enterprise");

            migrationBuilder.DropTable(
                name: "Specialization");
        }
    }
}
