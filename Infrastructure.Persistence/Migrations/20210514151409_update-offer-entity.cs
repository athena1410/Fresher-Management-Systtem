using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class updateofferentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Locations_LocationId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Trainees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Trainees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Trainees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Trainees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Trainees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusInClassId",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "OfferSalary",
                schema: "dbo",
                table: "Offers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "JobRank",
                schema: "dbo",
                table: "Offers",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "ContractType",
                schema: "dbo",
                table: "Offers",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualEndDate",
                schema: "dbo",
                table: "Classes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartDate",
                schema: "dbo",
                table: "Classes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassAdminId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClassCode",
                schema: "dbo",
                table: "Classes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                schema: "dbo",
                table: "Classes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTypeId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DetailLocation",
                schema: "dbo",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "EstimateBudget",
                schema: "dbo",
                table: "Classes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedEndDate",
                schema: "dbo",
                table: "Classes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedStartDate",
                schema: "dbo",
                table: "Classes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormatId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanedTraineeNumber",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScopeId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubSubjectTypeId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectTypeId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Budgets",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    DeliveryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormatTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormatTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormatTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scopes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ScopeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scopes_Classes_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "dbo",
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubSubjectTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubSubjectTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSubjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplierPartners",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SupplierPartnerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPartners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierPartners_Classes_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "dbo",
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainerProfiles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Account = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<short>(type: "smallint", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Major = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerProfiles_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalSchema: "dbo",
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_ClassId",
                table: "Trainees",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_BudgetId",
                schema: "dbo",
                table: "Classes",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_DeliveryTypeId",
                schema: "dbo",
                table: "Classes",
                column: "DeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_FormatId",
                schema: "dbo",
                table: "Classes",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SubjectTypeId",
                schema: "dbo",
                table: "Classes",
                column: "SubjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SubSubjectTypeId",
                schema: "dbo",
                table: "Classes",
                column: "SubSubjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TrainerId",
                schema: "dbo",
                table: "Classes",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Scopes_ClassId",
                schema: "dbo",
                table: "Scopes",
                column: "ClassId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPartners_ClassId",
                schema: "dbo",
                table: "SupplierPartners",
                column: "ClassId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerProfiles_Email",
                schema: "dbo",
                table: "TrainerProfiles",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerProfiles_Phone",
                schema: "dbo",
                table: "TrainerProfiles",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerProfiles_TrainerId",
                schema: "dbo",
                table: "TrainerProfiles",
                column: "TrainerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Budgets_BudgetId",
                schema: "dbo",
                table: "Classes",
                column: "BudgetId",
                principalSchema: "dbo",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_DeliveryTypes_DeliveryTypeId",
                schema: "dbo",
                table: "Classes",
                column: "DeliveryTypeId",
                principalSchema: "dbo",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_FormatTypes_FormatId",
                schema: "dbo",
                table: "Classes",
                column: "FormatId",
                principalSchema: "dbo",
                principalTable: "FormatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Locations_LocationId",
                schema: "dbo",
                table: "Classes",
                column: "LocationId",
                principalSchema: "dbo",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_SubjectTypes_SubjectTypeId",
                schema: "dbo",
                table: "Classes",
                column: "SubjectTypeId",
                principalSchema: "dbo",
                principalTable: "SubjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_SubSubjectTypes_SubSubjectTypeId",
                schema: "dbo",
                table: "Classes",
                column: "SubSubjectTypeId",
                principalSchema: "dbo",
                principalTable: "SubSubjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Trainers_TrainerId",
                schema: "dbo",
                table: "Classes",
                column: "TrainerId",
                principalSchema: "dbo",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Classes_ClassId",
                table: "Trainees",
                column: "ClassId",
                principalSchema: "dbo",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Budgets_BudgetId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_DeliveryTypes_DeliveryTypeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_FormatTypes_FormatId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Locations_LocationId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_SubjectTypes_SubjectTypeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_SubSubjectTypes_SubSubjectTypeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Trainers_TrainerId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Classes_ClassId",
                table: "Trainees");

            migrationBuilder.DropTable(
                name: "Budgets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DeliveryTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FormatTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Scopes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SubjectTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SubSubjectTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SupplierPartners",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TrainerProfiles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Trainers",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Trainees_ClassId",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_Classes_BudgetId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_DeliveryTypeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_FormatId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SubjectTypeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SubSubjectTypeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TrainerId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "StatusInClassId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "ActualEndDate",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ActualStartDate",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ClassAdminId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ClassCode",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ClassName",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "DeliveryTypeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "DetailLocation",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "EstimateBudget",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ExpectedEndDate",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ExpectedStartDate",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "FormatId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "PlanedTraineeNumber",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ScopeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SubSubjectTypeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SubjectTypeId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                schema: "dbo",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "OfferSalary",
                schema: "dbo",
                table: "Offers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "JobRank",
                schema: "dbo",
                table: "Offers",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "ContractType",
                schema: "dbo",
                table: "Offers",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                schema: "dbo",
                table: "Classes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Locations_LocationId",
                schema: "dbo",
                table: "Classes",
                column: "LocationId",
                principalSchema: "dbo",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
