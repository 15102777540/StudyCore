using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyCore.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    ParentGuid = table.Column<Guid>(nullable: true),
                    ParentName = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    IsDefaultRouter = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedByUserGuid = table.Column<Guid>(nullable: false),
                    CreatedByUserName = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedByUserGuid = table.Column<Guid>(nullable: true),
                    ModifiedByUserName = table.Column<string>(nullable: true),
                    Component = table.Column<string>(maxLength: 255, nullable: true),
                    HideInMenu = table.Column<int>(nullable: true),
                    NotCache = table.Column<int>(nullable: true),
                    BeforeCloseFun = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedByUserGuid = table.Column<Guid>(nullable: false),
                    CreatedByUserName = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedByUserGuid = table.Column<Guid>(nullable: true),
                    ModifiedByUserName = table.Column<string>(nullable: true),
                    IsSuperAdministrator = table.Column<bool>(nullable: false),
                    IsBuiltin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LoginName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    IsLocked = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedByUserGuid = table.Column<Guid>(nullable: false),
                    CreatedByUserName = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedByUserGuid = table.Column<Guid>(nullable: true),
                    ModifiedByUserName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(800)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    MenuGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ActionCode = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CreatedByUserGuid = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedByUserName = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedByUserGuid = table.Column<Guid>(nullable: true),
                    ModifiedByUserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Permission_Menu_MenuGuid",
                        column: x => x.MenuGuid,
                        principalTable: "Menu",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleMapping",
                columns: table => new
                {
                    UserGuid = table.Column<Guid>(nullable: false),
                    RoleCode = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMapping", x => new { x.UserGuid, x.RoleCode });
                    table.ForeignKey(
                        name: "FK_UserRoleMapping_Role_RoleCode",
                        column: x => x.RoleCode,
                        principalTable: "Role",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoleMapping_User_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "User",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionMapping",
                columns: table => new
                {
                    RoleCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PermissionCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionMapping", x => new { x.RoleCode, x.PermissionCode });
                    table.ForeignKey(
                        name: "FK_RolePermissionMapping_Permission_PermissionCode",
                        column: x => x.PermissionCode,
                        principalTable: "Permission",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissionMapping_Role_RoleCode",
                        column: x => x.RoleCode,
                        principalTable: "Role",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Code",
                table: "Permission",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_MenuGuid",
                table: "Permission",
                column: "MenuGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Code",
                table: "Role",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMapping_PermissionCode",
                table: "RolePermissionMapping",
                column: "PermissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMapping_RoleCode",
                table: "UserRoleMapping",
                column: "RoleCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissionMapping");

            migrationBuilder.DropTable(
                name: "UserRoleMapping");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
