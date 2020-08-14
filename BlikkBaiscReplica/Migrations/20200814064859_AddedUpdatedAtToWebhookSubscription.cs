using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlikkBaiscReplica.Migrations
{
    public partial class AddedUpdatedAtToWebhookSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "WebhookSubscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WebhookSubscriptions");
        }
    }
}
