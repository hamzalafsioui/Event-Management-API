using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManagement.Infrustructure.Migrations
{
	/// <inheritdoc />
	public partial class InitialMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
					Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Role = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false),
					DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
					UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
					PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
					TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
					AccessFailedCount = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Categories",
				columns: table => new
				{
					CategoryId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Categories", x => x.CategoryId);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<int>(type: "int", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_AspNetUserClaims_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_AspNetUserLogins_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserId = table.Column<int>(type: "int", nullable: false),
					RoleId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
						column: x => x.RoleId,
						principalTable: "AspNetRoles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AspNetUserRoles_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserId = table.Column<int>(type: "int", nullable: false),
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_AspNetUserTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Speakers",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					Bio = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Speakers", x => x.Id);
					table.ForeignKey(
						name: "FK_Speakers_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UserRefreshTokens",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
					RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
					JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
					IsUsed = table.Column<bool>(type: "bit", nullable: false),
					IsRevoked = table.Column<bool>(type: "bit", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
					table.ForeignKey(
						name: "FK_UserRefreshTokens_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Events",
				columns: table => new
				{
					EventId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
					EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
					CategoryId = table.Column<int>(type: "int", nullable: false),
					CreatorId = table.Column<int>(type: "int", nullable: false),
					Capacity = table.Column<int>(type: "int", nullable: false),
					Status = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Events", x => x.EventId);
					table.ForeignKey(
						name: "FK_Events_AspNetUsers_CreatorId",
						column: x => x.CreatorId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Events_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "CategoryId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Attendees",
				columns: table => new
				{
					EventId = table.Column<int>(type: "int", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: false),
					Status = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					RSVPDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					HasAttended = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Attendees", x => new { x.EventId, x.UserId });
					table.ForeignKey(
						name: "FK_Attendees_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Attendees_Events_EventId",
						column: x => x.EventId,
						principalTable: "Events",
						principalColumn: "EventId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Comments",
				columns: table => new
				{
					CommentId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					EventId = table.Column<int>(type: "int", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: false),
					Content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
					Status = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Comments", x => x.CommentId);
					table.ForeignKey(
						name: "FK_Comments_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Comments_Events_EventId",
						column: x => x.EventId,
						principalTable: "Events",
						principalColumn: "EventId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "SpeakerEvents",
				columns: table => new
				{
					EventId = table.Column<int>(type: "int", nullable: false),
					SpeakerId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SpeakerEvents", x => new { x.EventId, x.SpeakerId });
					table.ForeignKey(
						name: "FK_SpeakerEvents_Events_EventId",
						column: x => x.EventId,
						principalTable: "Events",
						principalColumn: "EventId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_SpeakerEvents_Speakers_SpeakerId",
						column: x => x.SpeakerId,
						principalTable: "Speakers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

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
				name: "IX_AspNetRoleClaims_RoleId",
				table: "AspNetRoleClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserId",
				table: "AspNetUserClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserLogins_UserId",
				table: "AspNetUserLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserRoles_RoleId",
				table: "AspNetUserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "AspNetUsers",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "IX_Users_Email",
				table: "AspNetUsers",
				column: "Email",
				unique: true,
				filter: "[Email] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Users_UserName",
				table: "AspNetUsers",
				column: "UserName",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Attendees_EventId_UserId",
				table: "Attendees",
				columns: new[] { "EventId", "UserId" });

			migrationBuilder.CreateIndex(
				name: "IX_Attendees_UserId",
				table: "Attendees",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Comments_EventId",
				table: "Comments",
				column: "EventId");

			migrationBuilder.CreateIndex(
				name: "IX_Comments_UserId",
				table: "Comments",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Events_CategoryId",
				table: "Events",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Events_CreatorId",
				table: "Events",
				column: "CreatorId");

			migrationBuilder.CreateIndex(
				name: "IX_Events_EndTime",
				table: "Events",
				column: "EndTime");

			migrationBuilder.CreateIndex(
				name: "IX_Events_StartTime",
				table: "Events",
				column: "StartTime");

			migrationBuilder.CreateIndex(
				name: "IX_SpeakerEvents_SpeakerId",
				table: "SpeakerEvents",
				column: "SpeakerId");

			migrationBuilder.CreateIndex(
				name: "IX_Speaker_UserId",
				table: "Speakers",
				column: "UserId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_UserRefreshTokens_UserId",
				table: "UserRefreshTokens",
				column: "UserId");

			// Create the stored procedure
			migrationBuilder.Sql(@"
    CREATE PROCEDURE dbo.SP_GetUserEventEngagementDetails
        @UserId INT
    AS
    BEGIN
        SET NOCOUNT ON;

        SELECT 
            u.Id AS UserId,
            u.UserName,
            CASE 
                WHEN u.Role = 1 THEN 'Admin'
                WHEN u.Role = 2 THEN 'Speaker'
                WHEN u.Role = 3 THEN 'Attendee'
                WHEN u.Role = 4 THEN 'User'
                ELSE 'Unknown'
            END AS Role,
            u.Email,
            COUNT(DISTINCT att.EventId) AS TotalEventsAttended,
            COUNT(DISTINCT c.CommentId) AS TotalCommentsMade,
            COUNT(DISTINCT CASE WHEN e.CreatorId = u.Id THEN e.EventId END) AS TotalEventsCreated,
            SUM(CASE WHEN att.HasAttended = 1 THEN 1 ELSE 0 END) AS TotalEventsPhysicallyAttended,
            SUM(CASE WHEN e.EventId IS NOT NULL THEN 1 ELSE 0 END) AS TotalEventsRelated,
            MAX(cat.Name) AS LastCategoryAttended,
            MAX(e.StartTime) AS LastEventAttendedDate
        FROM 
            dbo.AspNetUsers u
        LEFT JOIN 
            dbo.Attendees att ON u.Id = att.UserId
        LEFT JOIN 
            dbo.Events e ON att.EventId = e.EventId
        LEFT JOIN 
            dbo.Categories cat ON e.CategoryId = cat.CategoryId
        LEFT JOIN 
            dbo.Comments c ON u.Id = c.UserId AND c.EventId = e.EventId
        WHERE 
            u.Id = @UserId
            AND u.IsDeleted = 0
        GROUP BY 
            u.Id, u.UserName, u.Email, u.Role;
    END
");

			// Create the view
			migrationBuilder.Sql(@"
    CREATE VIEW dbo.ViewUserEventEngagementSummary
    AS
    SELECT 
        u.Id AS UserId,
        u.FirstName,
        u.LastName,
        CASE 
            WHEN u.Role = 1 THEN 'Admin'
            WHEN u.Role = 2 THEN 'Speaker'
            WHEN u.Role = 3 THEN 'Attendee'
            WHEN u.Role = 4 THEN 'User'
            ELSE 'Unknown'
        END AS Role,
        u.Email,
        COUNT(DISTINCT att.EventId) AS TotalEventsAttended,
        COUNT(DISTINCT c.CommentId) AS TotalCommentsMade,
        COUNT(DISTINCT CASE WHEN e.CreatorId = u.Id THEN e.EventId END) AS TotalEventsCreated,
        SUM(CASE WHEN att.HasAttended = 1 THEN 1 ELSE 0 END) AS TotalEventsPhysicallyAttended,
        SUM(CASE WHEN e.EventId IS NOT NULL THEN 1 ELSE 0 END) AS TotalEventsRelated,
        MAX(cat.Name) AS LastCategoryAttended,
        MAX(e.StartTime) AS LastEventAttendedDate
    FROM 
        dbo.AspNetUsers u
    LEFT JOIN 
        dbo.Attendees att ON u.Id = att.UserId
    LEFT JOIN 
        dbo.Events e ON att.EventId = e.EventId
    LEFT JOIN 
        dbo.Categories cat ON e.CategoryId = cat.CategoryId
    LEFT JOIN 
        dbo.Comments c ON u.Id = c.UserId AND c.EventId = e.EventId
    WHERE 
        u.IsDeleted = 0
    GROUP BY 
        u.Id, u.FirstName, u.LastName, u.Email, u.Role;
");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserClaims");

			migrationBuilder.DropTable(
				name: "AspNetUserLogins");

			migrationBuilder.DropTable(
				name: "AspNetUserRoles");

			migrationBuilder.DropTable(
				name: "AspNetUserTokens");

			migrationBuilder.DropTable(
				name: "Attendees");

			migrationBuilder.DropTable(
				name: "Comments");

			migrationBuilder.DropTable(
				name: "SpeakerEvents");

			migrationBuilder.DropTable(
				name: "UserRefreshTokens");

			migrationBuilder.DropTable(
				name: "AspNetRoles");

			migrationBuilder.DropTable(
				name: "Events");

			migrationBuilder.DropTable(
				name: "Speakers");

			migrationBuilder.DropTable(
				name: "Categories");

			migrationBuilder.DropTable(
				name: "AspNetUsers");
		}
	}
}
