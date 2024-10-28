using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManoTourism.Migrations.Mano
{
    public partial class initialManoCrmMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutSections",
                columns: table => new
                {
                    AboutSectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderTLEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissionCaptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissionCaptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerFocusCaptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerFocusCaptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoBackground = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutUsImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutSections", x => x.AboutSectionId);
                });

            migrationBuilder.CreateTable(
                name: "Affiliates",
                columns: table => new
                {
                    AffiliateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AffiliatePic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AffiliateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AffiliateEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AffiliatePassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affiliates", x => x.AffiliateId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMarktings",
                columns: table => new
                {
                    CompanyMarktingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMarktings", x => x.CompanyMarktingId);
                });

            migrationBuilder.CreateTable(
                name: "CompleteTransactions",
                columns: table => new
                {
                    CompleteTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDescEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDescAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompleteTransactions", x => x.CompleteTransactionId);
                });

            migrationBuilder.CreateTable(
                name: "ContactSocials",
                columns: table => new
                {
                    ContactSocialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactUsImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatsApp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Twiter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Youtube = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telegram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpentingTimeAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpentingTimeEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactSocials", x => x.ContactSocialId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryTLEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAtive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    EmployeeRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => x.EmployeeRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeePic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeePassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EstablishingCompanies",
                columns: table => new
                {
                    EstablishingCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstablishingCompanies", x => x.EstablishingCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelMainImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Review = table.Column<double>(type: "float", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceTypes",
                columns: table => new
                {
                    InsuranceTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceTypeTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceTypeTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceTypes", x => x.InsuranceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ManoEntityTypes",
                columns: table => new
                {
                    ManoEntityTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManoEntityTypes", x => x.ManoEntityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    NationalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalityTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalityTLEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAtive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.NationalityId);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OfferId);
                });

            migrationBuilder.CreateTable(
                name: "PageContents",
                columns: table => new
                {
                    PageContentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageContents", x => x.PageContentId);
                });

            migrationBuilder.CreateTable(
                name: "PublicSliders",
                columns: table => new
                {
                    PublicSliderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsImage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicSliders", x => x.PublicSliderId);
                });

            migrationBuilder.CreateTable(
                name: "RejectReasons",
                columns: table => new
                {
                    RejectReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RejectReasonTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectReasonTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectReasons", x => x.RejectReasonId);
                });

            migrationBuilder.CreateTable(
                name: "SubscribeNewsLetters",
                columns: table => new
                {
                    SubscribeNewLetterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribeNewsLetters", x => x.SubscribeNewLetterId);
                });

            migrationBuilder.CreateTable(
                name: "Testimonials",
                columns: table => new
                {
                    TestimonialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.TestimonialId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketDescEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketDescAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.CreateTable(
                name: "TripTargets",
                columns: table => new
                {
                    TripTargetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripTargets", x => x.TripTargetId);
                });

            migrationBuilder.CreateTable(
                name: "TripTypes",
                columns: table => new
                {
                    TripTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripTypeTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripTypeTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripTypes", x => x.TripTypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    UserMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.UserMessageId);
                });

            migrationBuilder.CreateTable(
                name: "VisaRequestStatuses",
                columns: table => new
                {
                    VisaRequestStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaRequestStatuses", x => x.VisaRequestStatusId);
                });

            migrationBuilder.CreateTable(
                name: "VisaTargets",
                columns: table => new
                {
                    VisaTargetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaTargets", x => x.VisaTargetId);
                });

            migrationBuilder.CreateTable(
                name: "VisaTypes",
                columns: table => new
                {
                    VisaTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisaTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisaTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaTypes", x => x.VisaTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AssignEmployeeRoles",
                columns: table => new
                {
                    AssignEmployeeRolesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignEmployeeRoles", x => x.AssignEmployeeRolesId);
                    table.ForeignKey(
                        name: "FK_AssignEmployeeRoles_EmployeeRoles_EmployeeRoleId",
                        column: x => x.EmployeeRoleId,
                        principalTable: "EmployeeRoles",
                        principalColumn: "EmployeeRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignEmployeeRoles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    HotelImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImages", x => x.HotelImageId);
                    table.ForeignKey(
                        name: "FK_HotelImages_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelReviews",
                columns: table => new
                {
                    HotelReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewWriterImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewWriterNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewWriterNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelReviews", x => x.HotelReviewId);
                    table.ForeignKey(
                        name: "FK_HotelReviews_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationInMonth = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OldPrice = table.Column<double>(type: "float", nullable: false),
                    NewPrice = table.Column<double>(type: "float", nullable: false),
                    InsuranceTypeId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.InsuranceId);
                    table.ForeignKey(
                        name: "FK_Insurances_InsuranceTypes_InsuranceTypeId",
                        column: x => x.InsuranceTypeId,
                        principalTable: "InsuranceTypes",
                        principalColumn: "InsuranceTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferImages",
                columns: table => new
                {
                    OfferImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferImages", x => x.OfferImageId);
                    table.ForeignKey(
                        name: "FK_OfferImages_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    TripTypeId = table.Column<int>(type: "int", nullable: false),
                    ReviewRating = table.Column<double>(type: "float", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldPricePerPerson = table.Column<double>(type: "float", nullable: false),
                    NewPricePerPerson = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TripTargetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_TripTargets_TripTargetId",
                        column: x => x.TripTargetId,
                        principalTable: "TripTargets",
                        principalColumn: "TripTargetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_TripTypes_TripTypeId",
                        column: x => x.TripTypeId,
                        principalTable: "TripTypes",
                        principalColumn: "TripTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectRequestReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeRequestUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedDateToEmployee = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelArrivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HotelDepartureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OneWayDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BeturnedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OneWay = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    EntityTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManoEntityTypeId = table.Column<int>(type: "int", nullable: false),
                    CompanyMarktingId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    VisaRequestStatusId = table.Column<int>(type: "int", nullable: false),
                    RejectReasonId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Requests_CompanyMarktings_CompanyMarktingId",
                        column: x => x.CompanyMarktingId,
                        principalTable: "CompanyMarktings",
                        principalColumn: "CompanyMarktingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Requests_ManoEntityTypes_ManoEntityTypeId",
                        column: x => x.ManoEntityTypeId,
                        principalTable: "ManoEntityTypes",
                        principalColumn: "ManoEntityTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "NationalityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_RejectReasons_RejectReasonId",
                        column: x => x.RejectReasonId,
                        principalTable: "RejectReasons",
                        principalColumn: "RejectReasonId");
                    table.ForeignKey(
                        name: "FK_Requests_VisaRequestStatuses_VisaRequestStatusId",
                        column: x => x.VisaRequestStatusId,
                        principalTable: "VisaRequestStatuses",
                        principalColumn: "VisaRequestStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visas",
                columns: table => new
                {
                    VisaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisaTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisaTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    VisaTypeId = table.Column<int>(type: "int", nullable: false),
                    ValidityInDays = table.Column<int>(type: "int", nullable: false),
                    OldPricePerPerson = table.Column<double>(type: "float", nullable: false),
                    NewPricePerPerson = table.Column<double>(type: "float", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisaTargetId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visas", x => x.VisaId);
                    table.ForeignKey(
                        name: "FK_Visas_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visas_VisaTargets_VisaTargetId",
                        column: x => x.VisaTargetId,
                        principalTable: "VisaTargets",
                        principalColumn: "VisaTargetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visas_VisaTypes_VisaTypeId",
                        column: x => x.VisaTypeId,
                        principalTable: "VisaTypes",
                        principalColumn: "VisaTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripImages",
                columns: table => new
                {
                    TripImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripImages", x => x.TripImageId);
                    table.ForeignKey(
                        name: "FK_TripImages_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPrograms",
                columns: table => new
                {
                    TripProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    HeaderAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPrograms", x => x.TripProgramId);
                    table.ForeignKey(
                        name: "FK_TripPrograms_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisaCountryDocuments",
                columns: table => new
                {
                    VisaCountryDocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTLAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentTLEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaCountryDocuments", x => x.VisaCountryDocumentId);
                    table.ForeignKey(
                        name: "FK_VisaCountryDocuments_Visas_VisaId",
                        column: x => x.VisaId,
                        principalTable: "Visas",
                        principalColumn: "VisaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CompanyMarktings",
                columns: new[] { "CompanyMarktingId", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, "التسويق", "Markting" },
                    { 2, "اصدقاء", "Friends" },
                    { 3, "موظفينا", "Our Employees" },
                    { 4, "لا احد", "No One" }
                });

            migrationBuilder.InsertData(
                table: "CompleteTransactions",
                columns: new[] { "CompleteTransactionId", "Pic", "TransactionDescAr", "TransactionDescEn", "TransactionTitleAr", "TransactionTitleEn" },
                values: new object[] { 1, "p1.png", "تسهيلات", "Facilities", "تسهيلات ", "Facilities" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryTLAR", "CountryTLEN", "IsAtive" },
                values: new object[,]
                {
                    { 1, "الولايات المتحده الامريكية", "United States of America", true },
                    { 2, "كندا", "Canada", true },
                    { 3, "المكسيك", "Mexico", true },
                    { 4, "ارجنتين", "Argentina", true },
                    { 5, "المملكة المتحدة", "United Kingdom of Great Britain and Northern Ireland", true },
                    { 6, "المانيا", "Germany", true },
                    { 7, "فرنسا", "France", true },
                    { 8, "ايطاليا", "Italy", true },
                    { 9, "اسبانيا", "Spain", true },
                    { 10, "الصين", "China", true },
                    { 11, "الهند", "India", true },
                    { 12, "استراليا", "Australia", true },
                    { 13, "جنوب افريقيا", "South Africa", true },
                    { 14, "هولندا", "Netherlands", true },
                    { 15, "بلجيكا", "Belgium", true },
                    { 16, "سويسرا", "Switzerland", true },
                    { 17, "النمسا", "Austria", true },
                    { 18, "السويد", "Sweden", true },
                    { 19, "النرويج", "Norway", true },
                    { 20, "الدنمارك", "Denmark", true },
                    { 21, "فنلندا", "Finland", true },
                    { 22, "اليونان", "Greece", true },
                    { 23, "ايرلندا", "Ireland", true },
                    { 24, "البرتغال", "Portugal", true },
                    { 25, "بولندا", "Poland", true },
                    { 26, "اوكرانيا", "Ukraine", true },
                    { 27, "رومانيا", "Romania", true },
                    { 28, "بلغاريا", "Bulgaria", true },
                    { 29, "المجر", "Hungary", true },
                    { 30, "تركيا", "Turkey", true },
                    { 31, "السعودية", "Saudi Arabia", true },
                    { 32, "الإمارات العربية المتحده", "United Arab Emirates", true },
                    { 33, "ايران", "Iran", true },
                    { 34, "فلسطين", "Palestine", true },
                    { 35, "مصر", "Egypt", true },
                    { 36, "كويا الجنوبية", "South Korea", true },
                    { 37, "كوريا الشمالية", "North Korea", true }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryTLAR", "CountryTLEN", "IsAtive" },
                values: new object[,]
                {
                    { 38, "فيتنام", "Vietnam", true },
                    { 39, "تايلاند", "Thailand", true },
                    { 40, "ماليزيا", "Malaysia", true },
                    { 41, "سنغافوره", "Singapore", true },
                    { 42, "اندونيسيا", "Indonesia", true },
                    { 43, "الفلبين", "Philippines", true },
                    { 44, "باكستان", "Pakistan", true },
                    { 45, "بنجلادش", "Bangladesh", true },
                    { 46, "سيرلانكا", "Sri Lanka", true },
                    { 47, "نيبال", "Nepal", true },
                    { 48, "نيوزلاندا", "New Zealand", true },
                    { 49, "فيجي", "Fiji", true },
                    { 50, "بابو غينيا الجديدة", "Papua New Guinea", true },
                    { 51, "تشيلي", "Chile", true },
                    { 52, "بيرو", "Peru", true },
                    { 53, "كولومبيا", "Colombia", true },
                    { 54, "فنزويلا", "Venezuela", true },
                    { 55, "بوليفيا", "Bolivia", true },
                    { 56, "الاكوداور", "Ecuador", true },
                    { 57, "اوراغواي", "Uruguay", true },
                    { 58, "بارجواي", "Paraguay", true },
                    { 59, "نيجيريا", "Nigeria", true },
                    { 60, "كينيا", "Kenya", true },
                    { 61, "تنزانيا", "Tanzania", true },
                    { 62, "اوغندا", "Uganda", true },
                    { 63, "غانا", "Ghana", true },
                    { 64, "مدغشقر", "Madagascar", true },
                    { 65, "جزائر", "Algeria", true },
                    { 66, "تونس", "Tunisia", true },
                    { 67, "مغرب", "Morocco", true },
                    { 68, "افغانستان", "Afghanistan", true },
                    { 69, "اردن", "Jordan", true },
                    { 70, "لبنان", "Lebanon", true },
                    { 71, "قطر", "Qatar", true },
                    { 72, "عمان", "Oman", true },
                    { 73, "كويت", "Kuwait", true },
                    { 74, "منغوليا", "Mongolia", true },
                    { 75, "كازاخستان", "Kazakhstan", true },
                    { 76, "كمبوديا", "Cambodia", true },
                    { 77, "صربيا", "Serbia", true },
                    { 78, "كرواتيا", "Croatia", true },
                    { 79, "سولفينا", "Slovenia", true }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryTLAR", "CountryTLEN", "IsAtive" },
                values: new object[,]
                {
                    { 80, "سلوفاكيا", "Slovakia", true },
                    { 81, "ايسلندا", "Iceland", true },
                    { 82, "بيلاروسيا", "Belarus", true },
                    { 83, "قبرص", "Cyprus", true },
                    { 84, "البوسنه والهرسك", "Bosnia and Herzegovina", true },
                    { 85, "استونيا", "Estonia", true },
                    { 86, "لاتفيا", "Latvia", true },
                    { 87, "ليتوانيا", "Lithuania", true },
                    { 88, "مالطا", "Malta", true },
                    { 89, "لوكسمبورغ", "Luxembourg", true },
                    { 90, "ساموا", "Samoa", true },
                    { 91, "تونجا", "Tonga", true },
                    { 92, "غيانا", "Guyana", true },
                    { 93, "سورينام", "Suriname", true },
                    { 94, "غواتيمالا", "Guatemala", true },
                    { 95, "هندراس", "Honduras", true },
                    { 96, "كوستاريكا", "Costa Rica", true },
                    { 97, "بنما", "Panama", true },
                    { 98, "السلفادور", "El Salvador", true },
                    { 99, "نيكارغوا", "Nicaragua", true },
                    { 100, "برازيل", "Brazil", true },
                    { 101, "يابان", "Japan", true }
                });

            migrationBuilder.InsertData(
                table: "EmployeeRoles",
                columns: new[] { "EmployeeRoleId", "RoleTitleAr", "RoleTitleEn" },
                values: new object[,]
                {
                    { 1, "إداره التأشيرات", "Visas Management" },
                    { 2, "إداره الرحلات", "Trips Management" },
                    { 3, "إداره التأمينات", "Insurance Management" },
                    { 4, "إداره الفنادق", "Hotels Management " },
                    { 5, "إداره تذاكر الطيران", "Tickets Management" },
                    { 6, "إداره تأسيس الشركات", "Establishing Companies" },
                    { 7, "إداره الموقع الخارجي", "Manage Public Site" },
                    { 8, "إداره الطلبات الجديده (إسناد الطلبات للموظفين)", "New Requests Management(Assigning Request To Employees)" },
                    { 9, "إداره الطلبات التي تحت المعالجه (إعاده إسناد الطلبات للموظفين)", "Processing Requests Management(ReAssigning Request To Employees)" },
                    { 10, "إداره الطلبات الملغيه (إعاده الطلب كطلب جديد)", "Canceled Requests Management(Return Request To New Status)" },
                    { 11, "إداره الطلبات المرفوضه (إعاده الطلب كطلب جديد)", "Rejected Requests Management(Return Request To New Status)" },
                    { 12, "إداره تخليص المعاملات", "Complete Transactions Management" }
                });

            migrationBuilder.InsertData(
                table: "InsuranceTypes",
                columns: new[] { "InsuranceTypeId", "InsuranceTypeTitleAr", "InsuranceTypeTitleEn", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "تأمين سيارات", "Cars Insurance", false },
                    { 2, "تأمين صحي", "Health insurance", false },
                    { 3, "تأمين علي الحياه", "Life insurance", false },
                    { 4, "تأمين سفر", "Travel insurance", false }
                });

            migrationBuilder.InsertData(
                table: "ManoEntityTypes",
                columns: new[] { "ManoEntityTypeId", "EntityTitleAr", "EntityTitleEn" },
                values: new object[,]
                {
                    { 1, "تأشيرات", "Visa" },
                    { 2, "رحلات", "Tours" },
                    { 3, "تأمينات", "Insurance" },
                    { 4, " تأسيس الشركات", "Establishing Companies" }
                });

            migrationBuilder.InsertData(
                table: "ManoEntityTypes",
                columns: new[] { "ManoEntityTypeId", "EntityTitleAr", "EntityTitleEn" },
                values: new object[,]
                {
                    { 5, " فنادق", "Hotels" },
                    { 6, " تذاكر طيران", "Tickets" },
                    { 7, " تخليص معاملات", "Complete Transactions" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "NationalityId", "IsAtive", "NationalityTLAR", "NationalityTLEN" },
                values: new object[,]
                {
                    { 1, true, "امريكي", "American" },
                    { 2, true, "كندي", "Canadian" },
                    { 3, true, "مكسيكي", "Mexican" },
                    { 4, true, "ارجنتيني", "Argentinean" },
                    { 5, true, "بريطاني", "British" },
                    { 6, true, "الماني", "German" },
                    { 7, true, "فرنسي", "French" },
                    { 8, true, "ايطالي", "Italian" },
                    { 9, true, "اسبانيي", "Spanish" },
                    { 10, true, "صيني", "Chinese" },
                    { 11, true, "هندي", "Indian" },
                    { 12, true, "استرالي", "Australian" },
                    { 13, true, "جنوب افريقي", "South African" },
                    { 14, true, "هولندي", "Dutch" },
                    { 15, true, "بلجيكي", "Belgian" },
                    { 16, true, "سويسري", "Swiss" },
                    { 17, true, "نمساوي", "Austrian" },
                    { 18, true, "سويدي", "Swedish" },
                    { 19, true, "نرويجي", "Norwegian" },
                    { 20, true, "دنماركي", "Danish" },
                    { 21, true, "فنلندي", "Finnish" },
                    { 22, true, "يوناني", "Greek" },
                    { 23, true, "ايرلندي", "Irish" },
                    { 24, true, "برتغالي", "Portuguese" },
                    { 25, true, "بولندي", "Polish" },
                    { 26, true, "اوكراني", "Ukrainian" },
                    { 27, true, "روماني", "Romanian" },
                    { 28, true, "بلغاري", "Bulgarian" },
                    { 29, true, "مجري", "Hungarian" },
                    { 30, true, "تركي", "Turkish" },
                    { 31, true, "سعودي", "Saudi" },
                    { 32, true, "إماراتي ", "Emirati" },
                    { 33, true, "ايراني", "Iranian" },
                    { 34, true, "فلسطيني", "Palestinian" },
                    { 35, true, "مصري", "Egyptian" },
                    { 36, true, "كوري", "Korean" },
                    { 37, true, "برازيلي", "Brazilian" },
                    { 38, true, "فيتنامي", "Vietnamese" },
                    { 39, true, "تايلاندي", "Thai" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "NationalityId", "IsAtive", "NationalityTLAR", "NationalityTLEN" },
                values: new object[,]
                {
                    { 40, true, "ماليزي", "Malaysian" },
                    { 41, true, "سنغافوري", "Singaporean" },
                    { 42, true, "اندونيسي", "Indonesian" },
                    { 43, true, "فلبيني", "Filipino" },
                    { 44, true, "باكستاني", "Pakistani" },
                    { 45, true, "بنجلادشي", "Bangladeshi" },
                    { 46, true, "سيرلانكي", "Lankan" },
                    { 47, true, "نيبالي", "Nepali" },
                    { 48, true, "نيوزلاندي", "Zealander" },
                    { 49, true, "فيجي", "Fijian" },
                    { 50, true, " غينيا الجديدة", "Papua New Guinean" },
                    { 51, true, "تشيلي", "Chilean" },
                    { 52, true, "بيرو", "Peruvian" },
                    { 53, true, "كولومبيي", "Colombian" },
                    { 54, true, "فنزويلي", "Venezuelan" },
                    { 55, true, "بوليفي", "Bolivian" },
                    { 56, true, "اكوداوري", "Ecuadorian" },
                    { 57, true, "اوراغواني", "Uruguayan" },
                    { 58, true, "بارجواني", "Paraguayan" },
                    { 59, true, "نيجيري", "Nigerian" },
                    { 60, true, "كيني", "Kenyan" },
                    { 61, true, "تنزاني", "Tanzanian" },
                    { 62, true, "اوغندي", "Ugandan" },
                    { 63, true, "غانا", "Ghanaian" },
                    { 64, true, "مدغشقر", "Malagasy" },
                    { 65, true, "جزائري", "Algerian" },
                    { 66, true, "تونسي", "Tunisian" },
                    { 67, true, "مغربي", "Moroccan" },
                    { 68, true, "افغانستاني", "Afghan" },
                    { 69, true, "اردني", "Jordanian" },
                    { 70, true, "لبناني", "Lebanese" },
                    { 71, true, "قطري", "Qatari" },
                    { 72, true, "عماني", "Omani" },
                    { 73, true, "كويتي", "Kuwaiti" },
                    { 74, true, "منغولي", "Mongolian" },
                    { 75, true, "كازاخستاني", "Kazakh" },
                    { 76, true, "كمبودي", "Cambodian" },
                    { 77, true, "صربي", "Serbian" },
                    { 78, true, "كرواتي", "Croatian" },
                    { 79, true, "سولفيني", "Slovenian" },
                    { 80, true, "سلوفاكي", "Slovak" },
                    { 81, true, "ايسلندي", "Icelandic" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "NationalityId", "IsAtive", "NationalityTLAR", "NationalityTLEN" },
                values: new object[,]
                {
                    { 82, true, "بيلاروسي", "Belarusian" },
                    { 83, true, "قبرصي", "Cypriot" },
                    { 84, true, "بوسنه", "Bosnian" },
                    { 85, true, "استواني", "Estonian" },
                    { 86, true, "لاتيفي", "Latvian" },
                    { 87, true, "ليتواني", "Lithuanian" },
                    { 88, true, "مالطا", "Maltese" },
                    { 89, true, "لوكسمبورغ", "Luxembourger" },
                    { 90, true, "ساموا", "Samoa" },
                    { 91, true, "تونجا", "Tongan" },
                    { 92, true, "غيانا", "Guyanese" },
                    { 93, true, "سورينامي", "Surinamese" },
                    { 94, true, "غواتيمالا", "Guatemalan" },
                    { 95, true, "هندراسي", "Honduran" },
                    { 96, true, "كوستاريكاي", "Costa Rican" },
                    { 97, true, "بنمي", "Panamanian" },
                    { 98, true, "سلفادوري", "Salvadoran" },
                    { 99, true, "نيكارغوي", "Nicaraguan" },
                    { 100, true, "ياباني", "Japanese" }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "OfferId", "IsActive", "OfferTitleAr", "OfferTitleEn" },
                values: new object[,]
                {
                    { 1, true, "عروض التأشيرات داخل دولة الإمارات ", "Visa In UAE Offers" },
                    { 2, true, "عروض التأشيرات خارج دولة الإمارات ", "Visa Out UAE Offers" },
                    { 3, true, "عروض الرحلات داخل دولة الإمارات ", "Tours In UAE Offers" },
                    { 4, true, "عروض الرحلات خارج دولة الإمارات ", "Tours Out UAE Offers" },
                    { 5, true, "عروض  تذاكر الطيران ", "Ticket Offers" },
                    { 6, true, "عروض  التأمينات ", "Insurances Offers" },
                    { 7, true, "عروض الفنادق ", "Hotels Offers" }
                });

            migrationBuilder.InsertData(
                table: "PageContents",
                columns: new[] { "PageContentId", "ContentAr", "ContentEn", "PageTitleAr", "PageTitleEn" },
                values: new object[,]
                {
                    { 1, "سياسة الخصوصية", "Privacy Policy", "سياسة الخصوصية", "Privacy Policy" },
                    { 2, "الشروظ والاحكام", "Terms && Conditions", "الشروظ والاحكام", "Terms && Conditions" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "Pic", "TicketDescAr", "TicketDescEn", "TicketTitleAr", "TicketTitleEn" },
                values: new object[] { 1, "p1.png", "حجز تذكره طيران", "Ticket Booking", "حجز تذكره طيران ", "Ticket Booking" });

            migrationBuilder.InsertData(
                table: "TripTargets",
                columns: new[] { "TripTargetId", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, "داخل الإمارات العربية المتحده", "In UAE" },
                    { 2, "خارج الإمارات العربية المتحده", "Out UAE" }
                });

            migrationBuilder.InsertData(
                table: "TripTypes",
                columns: new[] { "TripTypeId", "IsDeleted", "TripTypeTitleAr", "TripTypeTitleEn" },
                values: new object[,]
                {
                    { 1, false, "رحلات المغامرات", "Adventure Tours" },
                    { 2, false, "رحلات ثقافية", "Cultural Tours" },
                    { 3, false, "الحياة البرية ورحلات السفاري", "Wildlife & Safari" },
                    { 4, false, "رحلات تاريخية", "Historical Tours" },
                    { 5, false, "رحلات خاصة", "Private Tours" },
                    { 6, false, "رحلات برية", "Road Trips" },
                    { 7, false, "رحلات سياحية في المدينة", "City Tours" }
                });

            migrationBuilder.InsertData(
                table: "VisaRequestStatuses",
                columns: new[] { "VisaRequestStatusId", "StatusTitleAr", "StatusTitleEn" },
                values: new object[,]
                {
                    { 1, "جديد", "New" },
                    { 2, "المعالجة", "Processing" },
                    { 3, "تمت المعاملة", "Finished" },
                    { 4, "رفض الطلب", "Reject" }
                });

            migrationBuilder.InsertData(
                table: "VisaRequestStatuses",
                columns: new[] { "VisaRequestStatusId", "StatusTitleAr", "StatusTitleEn" },
                values: new object[] { 5, "الغاء الطلب", "Cancel" });

            migrationBuilder.InsertData(
                table: "VisaTargets",
                columns: new[] { "VisaTargetId", "TitleAr", "TitleEn" },
                values: new object[] { 1, "داخل الإمارات العربية المتحده", "In UAE" });

            migrationBuilder.InsertData(
                table: "VisaTargets",
                columns: new[] { "VisaTargetId", "TitleAr", "TitleEn" },
                values: new object[] { 2, "خارج الإمارات العربية المتحده", "Out UAE" });

            migrationBuilder.CreateIndex(
                name: "IX_AssignEmployeeRoles_EmployeeId",
                table: "AssignEmployeeRoles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignEmployeeRoles_EmployeeRoleId",
                table: "AssignEmployeeRoles",
                column: "EmployeeRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelId",
                table: "HotelImages",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReviews_HotelId",
                table: "HotelReviews",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceTypeId",
                table: "Insurances",
                column: "InsuranceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferImages_OfferId",
                table: "OfferImages",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CompanyMarktingId",
                table: "Requests",
                column: "CompanyMarktingId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CountryId",
                table: "Requests",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_EmployeeId",
                table: "Requests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ManoEntityTypeId",
                table: "Requests",
                column: "ManoEntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_NationalityId",
                table: "Requests",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RejectReasonId",
                table: "Requests",
                column: "RejectReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_VisaRequestStatusId",
                table: "Requests",
                column: "VisaRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TripImages_TripId",
                table: "TripImages",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPrograms_TripId",
                table: "TripPrograms",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CountryId",
                table: "Trips",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TripTargetId",
                table: "Trips",
                column: "TripTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TripTypeId",
                table: "Trips",
                column: "TripTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VisaCountryDocuments_VisaId",
                table: "VisaCountryDocuments",
                column: "VisaId");

            migrationBuilder.CreateIndex(
                name: "IX_Visas_CountryId",
                table: "Visas",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Visas_VisaTargetId",
                table: "Visas",
                column: "VisaTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Visas_VisaTypeId",
                table: "Visas",
                column: "VisaTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutSections");

            migrationBuilder.DropTable(
                name: "Affiliates");

            migrationBuilder.DropTable(
                name: "AssignEmployeeRoles");

            migrationBuilder.DropTable(
                name: "CompleteTransactions");

            migrationBuilder.DropTable(
                name: "ContactSocials");

            migrationBuilder.DropTable(
                name: "EstablishingCompanies");

            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "HotelReviews");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "OfferImages");

            migrationBuilder.DropTable(
                name: "PageContents");

            migrationBuilder.DropTable(
                name: "PublicSliders");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "SubscribeNewsLetters");

            migrationBuilder.DropTable(
                name: "Testimonials");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TripImages");

            migrationBuilder.DropTable(
                name: "TripPrograms");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "VisaCountryDocuments");

            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "InsuranceTypes");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "CompanyMarktings");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ManoEntityTypes");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "RejectReasons");

            migrationBuilder.DropTable(
                name: "VisaRequestStatuses");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Visas");

            migrationBuilder.DropTable(
                name: "TripTargets");

            migrationBuilder.DropTable(
                name: "TripTypes");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "VisaTargets");

            migrationBuilder.DropTable(
                name: "VisaTypes");
        }
    }
}
