using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ensek.Data.Migrations
{
    public partial class Add_InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "MeterReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    MeterReadingDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeterReadValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeterReadings_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeterReadings_AccountId",
                table: "MeterReadings",
                column: "AccountId");

            migrationBuilder.Sql(@"
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1234, N'Freya', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1239, N'Noddy', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1240, N'Archie', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1241, N'Lara', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1242, N'Tim', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1243, N'Graham', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1244, N'Tony', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1245, N'Neville', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1246, N'Jo', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1247, N'Jim', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (1248, N'Pam', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2233, N'Barry', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2344, N'Tommy', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2345, N'Jerry', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2346, N'Ollie', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2347, N'Tara', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2348, N'Tammy', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2349, N'Simon', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2350, N'Colin', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2351, N'Gladys', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2352, N'Greg', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2353, N'Tony', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2355, N'Arthur', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (2356, N'Craig', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (4534, N'JOSH', N'TEST')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (6776, N'Laura', N'Test')
            INSERT [dbo].[Accounts] ([AccountId], [FirstName], [LastName]) VALUES (8766, N'Sally', N'Test')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeterReadings");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
