using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolePermissionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_permissions_permissions_permission_id",
                table: "permissions");

            migrationBuilder.RenameColumn(
                name: "permission_id",
                table: "permissions",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "ix_permissions_permission_id",
                table: "permissions",
                newName: "ix_permissions_role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_role_role_id",
                table: "permissions",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_permissions_role_role_id",
                table: "permissions");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "permissions",
                newName: "permission_id");

            migrationBuilder.RenameIndex(
                name: "ix_permissions_role_id",
                table: "permissions",
                newName: "ix_permissions_permission_id");

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_permissions_permission_id",
                table: "permissions",
                column: "permission_id",
                principalTable: "permissions",
                principalColumn: "id");
        }
    }
}
