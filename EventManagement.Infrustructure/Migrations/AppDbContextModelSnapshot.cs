﻿// <auto-generated />
using System;
using EventManagement.Infrustructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventManagement.Infrustructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventManagement.Data.Entities.Attendee", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasAttended")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RSVPDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.HasIndex("EventId", "UserId")
                        .HasDatabaseName("IX_Attendees_EventId_UserId");

                    b.ToTable("Attendees", (string)null);
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories", (string)null);

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "Professional gatherings for networking, learning, and discussing industry topics.",
                            Name = "Conferences"
                        },
                        new
                        {
                            CategoryId = 2,
                            Description = "Interactive sessions focused on skill development and hands-on learning.",
                            Name = "Workshops"
                        },
                        new
                        {
                            CategoryId = 3,
                            Description = "Educational meetings for discussing specialized topics or subjects.",
                            Name = "Seminars"
                        },
                        new
                        {
                            CategoryId = 4,
                            Description = "Online seminars conducted over the internet.",
                            Name = "Webinars"
                        },
                        new
                        {
                            CategoryId = 5,
                            Description = "Informal gatherings for people with shared interests.",
                            Name = "Meetups"
                        },
                        new
                        {
                            CategoryId = 6,
                            Description = "Live music performances.",
                            Name = "Concerts"
                        },
                        new
                        {
                            CategoryId = 7,
                            Description = "Large public celebrations with various activities, often including music, food, and entertainment.",
                            Name = "Festivals"
                        },
                        new
                        {
                            CategoryId = 8,
                            Description = "Competitive athletic events and games.",
                            Name = "Sports"
                        },
                        new
                        {
                            CategoryId = 9,
                            Description = "Events focused on building professional connections and relationships.",
                            Name = "Networking"
                        },
                        new
                        {
                            CategoryId = 10,
                            Description = "Exhibitions where companies showcase and demonstrate their products and services.",
                            Name = "Trade Shows"
                        },
                        new
                        {
                            CategoryId = 11,
                            Description = "Events organized to raise funds or awareness for charitable causes.",
                            Name = "Charity"
                        },
                        new
                        {
                            CategoryId = 12,
                            Description = "Social gatherings for celebration and entertainment.",
                            Name = "Parties"
                        },
                        new
                        {
                            CategoryId = 13,
                            Description = "Educational sessions or courses on various topics.",
                            Name = "Classes"
                        },
                        new
                        {
                            CategoryId = 14,
                            Description = "Events where programmers and developers collaborate intensively on software projects.",
                            Name = "Hackathons"
                        },
                        new
                        {
                            CategoryId = 15,
                            Description = "Public displays of art, products, or information.",
                            Name = "Exhibitions"
                        },
                        new
                        {
                            CategoryId = 16,
                            Description = "Events related to religious practices and gatherings.",
                            Name = "Religious"
                        },
                        new
                        {
                            CategoryId = 17,
                            Description = "Events organized to raise money for specific causes or organizations.",
                            Name = "Fundraisers"
                        },
                        new
                        {
                            CategoryId = 18,
                            Description = "Local events that engage and involve the community.",
                            Name = "Community"
                        },
                        new
                        {
                            CategoryId = 19,
                            Description = "Events designed for family participation and enjoyment.",
                            Name = "Family"
                        },
                        new
                        {
                            CategoryId = 20,
                            Description = "Events that do not fit into the above categories.",
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("EventId");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("IX_Events_CategoryId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("EndTime")
                        .HasDatabaseName("IX_Events_EndTime");

                    b.HasIndex("StartTime")
                        .HasDatabaseName("IX_Events_StartTime");

                    b.ToTable("Events", (string)null);
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IX_Users_Email")
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasDatabaseName("IX_Users_UserName");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Identity.UserRefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("JwtId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRefreshTokens");
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("IX_Speaker_UserId");

                    b.ToTable("Speakers", (string)null);
                });

            modelBuilder.Entity("EventManagement.Data.Entities.SpeakerEvent", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("SpeakerId")
                        .HasColumnType("int");

                    b.HasKey("EventId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SpeakerEvents", (string)null);
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Views.ViewUserEventEngagementSummary", b =>
                {
                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastCategoryAttended")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastEventAttendedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalCommentsMade")
                        .HasColumnType("int");

                    b.Property<int>("TotalEventsAttended")
                        .HasColumnType("int");

                    b.Property<int>("TotalEventsCreated")
                        .HasColumnType("int");

                    b.Property<int>("TotalEventsPhysicallyAttended")
                        .HasColumnType("int");

                    b.Property<int>("TotalEventsRelated")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("ViewUserEventEngagementSummary", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Attendee", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Event", "Event")
                        .WithMany("Attendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EventManagement.Data.Entities.Identity.User", "User")
                        .WithMany("AttendingEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Comment", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Event", "Event")
                        .WithMany("Comments")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EventManagement.Data.Entities.Identity.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Event", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Category", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EventManagement.Data.Entities.Identity.User", "Creator")
                        .WithMany("CreatedEvents")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Identity.UserRefreshToken", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Identity.User", "user")
                        .WithMany("UserRefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Speaker", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventManagement.Data.Entities.SpeakerEvent", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Event", "Event")
                        .WithMany("SpeakerEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventManagement.Data.Entities.Speaker", "Speaker")
                        .WithMany("SpeakerEvents")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Speaker");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventManagement.Data.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("EventManagement.Data.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Category", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Event", b =>
                {
                    b.Navigation("Attendees");

                    b.Navigation("Comments");

                    b.Navigation("SpeakerEvents");
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Identity.User", b =>
                {
                    b.Navigation("AttendingEvents");

                    b.Navigation("Comments");

                    b.Navigation("CreatedEvents");

                    b.Navigation("UserRefreshTokens");
                });

            modelBuilder.Entity("EventManagement.Data.Entities.Speaker", b =>
                {
                    b.Navigation("SpeakerEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
