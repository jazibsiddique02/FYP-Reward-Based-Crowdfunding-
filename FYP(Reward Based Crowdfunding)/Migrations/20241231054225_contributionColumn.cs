using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP_Reward_Based_Crowdfunding_.Migrations
{
    /// <inheritdoc />
    public partial class contributionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "contribution_amount",
                table: "Campaigns",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contribution_amount",
                table: "Campaigns");
        }
    }
}
