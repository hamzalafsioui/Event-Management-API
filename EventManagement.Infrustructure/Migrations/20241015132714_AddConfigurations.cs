using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManagement.Infrustructure.Migrations
{
	/// <inheritdoc />
	public partial class AddConfigurations : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Attendees_Events_EventId",
				table: "Attendees");

			migrationBuilder.DropForeignKey(
				name: "FK_Attendees_Users_UserId",
				table: "Attendees");

			migrationBuilder.DropForeignKey(
				name: "FK_Events_Categories_CategoryId",
				table: "Events");

			migrationBuilder.DropForeignKey(
				name: "FK_Events_Users_CreatedBy",
				table: "Events");

			migrationBuilder.DropIndex(
				name: "IX_Events_CreatedBy",
				table: "Events");

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 1);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 2);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 3);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 4);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 5);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 6);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 7);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 8);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 9);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 10);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 11);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 12);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 13);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 14);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 15);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 16);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 17);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 18);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 19);

			migrationBuilder.DeleteData(
				table: "Categories",
				keyColumn: "CategoryId",
				keyValue: 20);

			migrationBuilder.RenameColumn(
				name: "Password",
				table: "Users",
				newName: "PasswordHash");

			migrationBuilder.AddColumn<DateTime>(
				name: "DateOfBirth",
				table: "Users",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<bool>(
				name: "IsDeleted",
				table: "Users",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AddColumn<DateTime>(
				name: "LastLoginDate",
				table: "Users",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<int>(
				name: "CreatorUserId",
				table: "Events",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "Status",
				table: "Comments",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<DateTime>(
				name: "UpdatedAt",
				table: "Attendees",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.CreateIndex(
				name: "IX_Events_CreatorUserId",
				table: "Events",
				column: "CreatorUserId");

			migrationBuilder.AddForeignKey(
				name: "FK_Attendees_Events_EventId",
				table: "Attendees",
				column: "EventId",
				principalTable: "Events",
				principalColumn: "EventId",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Attendees_Users_UserId",
				table: "Attendees",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Events_Categories_CategoryId",
				table: "Events",
				column: "CategoryId",
				principalTable: "Categories",
				principalColumn: "CategoryId",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Events_Users_CreatorUserId",
				table: "Events",
				column: "CreatorUserId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Restrict);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Attendees_Events_EventId",
				table: "Attendees");

			migrationBuilder.DropForeignKey(
				name: "FK_Attendees_Users_UserId",
				table: "Attendees");

			migrationBuilder.DropForeignKey(
				name: "FK_Events_Categories_CategoryId",
				table: "Events");

			migrationBuilder.DropForeignKey(
				name: "FK_Events_Users_CreatorUserId",
				table: "Events");

			migrationBuilder.DropIndex(
				name: "IX_Events_CreatorUserId",
				table: "Events");

			migrationBuilder.DropColumn(
				name: "DateOfBirth",
				table: "Users");

			migrationBuilder.DropColumn(
				name: "IsDeleted",
				table: "Users");

			migrationBuilder.DropColumn(
				name: "LastLoginDate",
				table: "Users");

			migrationBuilder.DropColumn(
				name: "CreatorUserId",
				table: "Events");

			migrationBuilder.DropColumn(
				name: "Status",
				table: "Comments");

			migrationBuilder.DropColumn(
				name: "UpdatedAt",
				table: "Attendees");

			migrationBuilder.RenameColumn(
				name: "PasswordHash",
				table: "Users",
				newName: "Password");

			migrationBuilder.InsertData(
				table: "Categories",
				columns: new[] { "CategoryId", "Description", "Name" },
				values: new object[,]
				{
					{ 1, "Professional gatherings for networking, learning, and discussing industry topics.", "Conferences" },
					{ 2, "Interactive sessions focused on skill development and hands-on learning.", "Workshops" },
					{ 3, "Educational meetings for discussing specialized topics or subjects.", "Seminars" },
					{ 4, "Online seminars conducted over the internet.", "Webinars" },
					{ 5, "Informal gatherings for people with shared interests.", "Meetups" },
					{ 6, "Live music performances.", "Concerts" },
					{ 7, "Large public celebrations with various activities, often including music, food, and entertainment.", "Festivals" },
					{ 8, "Competitive athletic events and games.", "Sports" },
					{ 9, "Events focused on building professional connections and relationships.", "Networking" },
					{ 10, "Exhibitions where companies showcase and demonstrate their products and services.", "Trade Shows" },
					{ 11, "Events organized to raise funds or awareness for charitable causes.", "Charity" },
					{ 12, "Social gatherings for celebration and entertainment.", "Parties" },
					{ 13, "Educational sessions or courses on various topics.", "Classes" },
					{ 14, "Events where programmers and developers collaborate intensively on software projects.", "Hackathons" },
					{ 15, "Public displays of art, products, or information.", "Exhibitions" },
					{ 16, "Events related to religious practices and gatherings.", "Religious" },
					{ 17, "Events organized to raise money for specific causes or organizations.", "Fundraisers" },
					{ 18, "Local events that engage and involve the community.", "Community" },
					{ 19, "Events designed for family participation and enjoyment.", "Family" },
					{ 20, "Events that do not fit into the above categories.", "Other" }
				});

			migrationBuilder.CreateIndex(
				name: "IX_Events_CreatedBy",
				table: "Events",
				column: "CreatedBy");

			migrationBuilder.AddForeignKey(
				name: "FK_Attendees_Events_EventId",
				table: "Attendees",
				column: "EventId",
				principalTable: "Events",
				principalColumn: "EventId",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Attendees_Users_UserId",
				table: "Attendees",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Events_Categories_CategoryId",
				table: "Events",
				column: "CategoryId",
				principalTable: "Categories",
				principalColumn: "CategoryId",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Events_Users_CreatedBy",
				table: "Events",
				column: "CreatedBy",
				principalTable: "Users",
				principalColumn: "UserId",
				onDelete: ReferentialAction.Restrict);
		}
	}
}
