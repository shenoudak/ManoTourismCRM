using System;
using System.Numerics;
using ManoTourism.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Humanizer.In;

namespace ManoTourism.Data
{
    public class ManoContext : DbContext
    {
        public ManoContext()
        {
        }

        public ManoContext(DbContextOptions<ManoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SubscribeNewsLetter> SubscribeNewsLetters { get; set; }
        public virtual DbSet<UserMessage> UserMessages { get; set; }
        public virtual DbSet<ContactSocial> ContactSocials { get; set; }
        public virtual DbSet<PublicSlider> PublicSliders { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<VisaType> VisaTypes { get; set; }
        public virtual DbSet<Visa> Visas { get; set; }
        //public virtual DbSet<VisaRequest> VisaRequests { get; set; }
        public virtual DbSet<AboutSection> AboutSections { get; set; }
        public virtual DbSet<CompanyMarkting> CompanyMarktings { get; set; }
        public virtual DbSet<VisaTarget> VisaTargets { get; set; }
        public virtual DbSet<VisaCountryDocument> VisaCountryDocuments { get; set; }
        public virtual DbSet<PageContent> PageContents { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<TripType> TripTypes { get; set; }
        public virtual DbSet<TripTarget> TripTargets { get; set; }
        public virtual DbSet<TripImage> TripImages { get; set; }
        //public virtual DbSet<TripRequest> TripRequests { get; set; }
        public virtual DbSet<TripProgram>TripPrograms { get; set; }
        public virtual DbSet<Employee>Employees { get; set; }
        public virtual DbSet<VisaRequestStatus> VisaRequestStatuses { get; set; }
        public virtual DbSet<ManoEntityType> ManoEntityTypes { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Affiliate> Affiliates { get; set; }
        public virtual DbSet<RejectReason> RejectReasons { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<InsuranceType> InsuranceTypes { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OfferImage> OfferImages { get; set; }
        /// <summary>
        /// <summary>
        /// Employee Roles
        /// </summary>

        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<AssignEmployeeRoles> AssignEmployeeRoles { get; set; }
        public virtual DbSet<EstablishingCompany> EstablishingCompanies { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelImage> HotelImages { get; set; }
        public virtual DbSet<HotelReview> HotelReviews { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<CompleteTransaction> CompleteTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Ticket 
            modelBuilder.Entity<Ticket>().HasData(new Ticket { TicketId = 1, TicketTitleEn = "Ticket Booking", TicketTitleAr = "حجز تذكره طيران ", Pic = "p1.png",TicketDescAr= "حجز تذكره طيران", TicketDescEn= "Ticket Booking"});
            //CompleteTransaction 
            modelBuilder.Entity<CompleteTransaction>().HasData(new CompleteTransaction { CompleteTransactionId = 1, TransactionTitleEn = "Facilities", TransactionTitleAr = "تسهيلات ", Pic = "p1.png", TransactionDescAr = "تسهيلات", TransactionDescEn = "Facilities" });

            //Offer Images
            modelBuilder.Entity<Offer>().HasData(new Offer {  OfferId = 1, OfferTitleEn = "Visa In UAE Offers", OfferTitleAr = "عروض التأشيرات داخل دولة الإمارات ",IsActive=true });
            modelBuilder.Entity<Offer>().HasData(new Offer {  OfferId = 2, OfferTitleEn = "Visa Out UAE Offers", OfferTitleAr = "عروض التأشيرات خارج دولة الإمارات ",IsActive=true });
            modelBuilder.Entity<Offer>().HasData(new Offer {  OfferId = 3, OfferTitleEn = "Tours In UAE Offers", OfferTitleAr = "عروض الرحلات داخل دولة الإمارات ",IsActive=true });
            modelBuilder.Entity<Offer>().HasData(new Offer {  OfferId = 4, OfferTitleEn = "Tours Out UAE Offers", OfferTitleAr = "عروض الرحلات خارج دولة الإمارات ",IsActive=true });
            modelBuilder.Entity<Offer>().HasData(new Offer {  OfferId = 5, OfferTitleEn = "Ticket Offers", OfferTitleAr = "عروض  تذاكر الطيران ",IsActive=true });
            modelBuilder.Entity<Offer>().HasData(new Offer {  OfferId = 6, OfferTitleEn = "Insurances Offers", OfferTitleAr = "عروض  التأمينات ",IsActive=true });
            modelBuilder.Entity<Offer>().HasData(new Offer {  OfferId = 7, OfferTitleEn = "Hotels Offers", OfferTitleAr = "عروض الفنادق ",IsActive=true });


            //Insurance Types
            modelBuilder.Entity<InsuranceType>().HasData(new InsuranceType { InsuranceTypeId = 1, InsuranceTypeTitleEn = "Cars Insurance", InsuranceTypeTitleAr = "تأمين سيارات" });
            modelBuilder.Entity<InsuranceType>().HasData(new InsuranceType { InsuranceTypeId = 2, InsuranceTypeTitleEn = "Health insurance", InsuranceTypeTitleAr = "تأمين صحي" });
            modelBuilder.Entity<InsuranceType>().HasData(new InsuranceType { InsuranceTypeId = 3, InsuranceTypeTitleEn = "Life insurance", InsuranceTypeTitleAr = "تأمين علي الحياه" });
            modelBuilder.Entity<InsuranceType>().HasData(new InsuranceType { InsuranceTypeId = 4, InsuranceTypeTitleEn = "Travel insurance", InsuranceTypeTitleAr = "تأمين سفر" });
            //modelBuilder.Entity<InsuranceType>().HasData(new InsuranceType { InsuranceTypeId = 5, InsuranceTypeTitleEn = "Hotels", InsuranceTypeTitleAr = "فنادق" });

            //Seeding Employee Roles////
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 1, RoleTitleEn = "Visas Management", RoleTitleAr = "إداره التأشيرات" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 2, RoleTitleEn = "Trips Management", RoleTitleAr = "إداره الرحلات" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 3, RoleTitleEn = "Insurance Management", RoleTitleAr = "إداره التأمينات" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 4, RoleTitleEn = "Hotels Management ", RoleTitleAr = "إداره الفنادق" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 5, RoleTitleEn = "Tickets Management", RoleTitleAr = "إداره تذاكر الطيران" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 6, RoleTitleEn = "Establishing Companies", RoleTitleAr = "إداره تأسيس الشركات" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 7, RoleTitleEn = "Manage Public Site", RoleTitleAr = "إداره الموقع الخارجي" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 8, RoleTitleEn = "New Requests Management(Assigning Request To Employees)", RoleTitleAr = "إداره الطلبات الجديده (إسناد الطلبات للموظفين)" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 9, RoleTitleEn = "Processing Requests Management(ReAssigning Request To Employees)", RoleTitleAr = "إداره الطلبات التي تحت المعالجه (إعاده إسناد الطلبات للموظفين)" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 10, RoleTitleEn = "Canceled Requests Management(Return Request To New Status)", RoleTitleAr = "إداره الطلبات الملغيه (إعاده الطلب كطلب جديد)" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 11, RoleTitleEn = "Rejected Requests Management(Return Request To New Status)", RoleTitleAr = "إداره الطلبات المرفوضه (إعاده الطلب كطلب جديد)" });
            modelBuilder.Entity<EmployeeRole>().HasData(new EmployeeRole { EmployeeRoleId = 12, RoleTitleEn = "Complete Transactions Management", RoleTitleAr = "إداره تخليص المعاملات" });



            //Visa Entity Type
            modelBuilder.Entity<ManoEntityType>().HasData(new ManoEntityType { ManoEntityTypeId = 1, EntityTitleEn = "Visa", EntityTitleAr = "تأشيرات" });
            modelBuilder.Entity<ManoEntityType>().HasData(new ManoEntityType { ManoEntityTypeId = 2, EntityTitleEn = "Tours", EntityTitleAr = "رحلات" });
            modelBuilder.Entity<ManoEntityType>().HasData(new ManoEntityType { ManoEntityTypeId = 3, EntityTitleEn = "Insurance", EntityTitleAr = "تأمينات" });
            modelBuilder.Entity<ManoEntityType>().HasData(new ManoEntityType { ManoEntityTypeId = 4, EntityTitleEn = "Establishing Companies", EntityTitleAr = " تأسيس الشركات" });
            modelBuilder.Entity<ManoEntityType>().HasData(new ManoEntityType { ManoEntityTypeId = 5, EntityTitleEn = "Hotels", EntityTitleAr = " فنادق" });
            modelBuilder.Entity<ManoEntityType>().HasData(new ManoEntityType { ManoEntityTypeId = 6, EntityTitleEn = "Tickets", EntityTitleAr = " تذاكر طيران" });
            modelBuilder.Entity<ManoEntityType>().HasData(new ManoEntityType { ManoEntityTypeId = 7, EntityTitleEn = "Complete Transactions", EntityTitleAr = " تخليص معاملات" });



            //Visa Request Status
            modelBuilder.Entity<VisaRequestStatus>().HasData(new VisaRequestStatus { VisaRequestStatusId = 1, StatusTitleEn = "New", StatusTitleAr = "جديد" });
            modelBuilder.Entity<VisaRequestStatus>().HasData(new VisaRequestStatus { VisaRequestStatusId = 2, StatusTitleEn = "Processing", StatusTitleAr = "المعالجة" });
            modelBuilder.Entity<VisaRequestStatus>().HasData(new VisaRequestStatus { VisaRequestStatusId = 3, StatusTitleEn = "Finished", StatusTitleAr = "تمت المعاملة" });
            modelBuilder.Entity<VisaRequestStatus>().HasData(new VisaRequestStatus { VisaRequestStatusId = 4, StatusTitleEn = "Reject", StatusTitleAr = "رفض الطلب" });
            modelBuilder.Entity<VisaRequestStatus>().HasData(new VisaRequestStatus { VisaRequestStatusId = 5, StatusTitleEn = "Cancel", StatusTitleAr = "الغاء الطلب" });
            ////
            //Trips
            modelBuilder.Entity<TripTarget>().HasData(new TripTarget { TripTargetId = 1, TitleEn = "In UAE", TitleAr = "داخل الإمارات العربية المتحده" });
            modelBuilder.Entity<TripTarget>().HasData(new TripTarget { TripTargetId = 2, TitleEn = "Out UAE", TitleAr = "خارج الإمارات العربية المتحده" });
            ////
            modelBuilder.Entity<TripType>().HasData(new TripType { TripTypeId = 1, TripTypeTitleEn = "Adventure Tours", TripTypeTitleAr = "رحلات المغامرات" });
            modelBuilder.Entity<TripType>().HasData(new TripType { TripTypeId = 2, TripTypeTitleEn = "Cultural Tours", TripTypeTitleAr = "رحلات ثقافية" });
            modelBuilder.Entity<TripType>().HasData(new TripType { TripTypeId = 3, TripTypeTitleEn = "Wildlife & Safari", TripTypeTitleAr = "الحياة البرية ورحلات السفاري"});
            modelBuilder.Entity<TripType>().HasData(new TripType { TripTypeId = 4, TripTypeTitleEn = "Historical Tours", TripTypeTitleAr = "رحلات تاريخية" });
            modelBuilder.Entity<TripType>().HasData(new TripType { TripTypeId = 5, TripTypeTitleEn = "Private Tours", TripTypeTitleAr = "رحلات خاصة" });
            modelBuilder.Entity<TripType>().HasData(new TripType { TripTypeId = 6, TripTypeTitleEn = "Road Trips", TripTypeTitleAr = "رحلات برية" });
            modelBuilder.Entity<TripType>().HasData(new TripType { TripTypeId = 7, TripTypeTitleEn = "City Tours", TripTypeTitleAr = "رحلات سياحية في المدينة"});


            modelBuilder.Entity<CompanyMarkting>().HasData(new CompanyMarkting { CompanyMarktingId = 1, TitleEn = "Markting", TitleAr = "التسويق" });
            modelBuilder.Entity<CompanyMarkting>().HasData(new CompanyMarkting { CompanyMarktingId = 2, TitleEn = "Friends", TitleAr = "اصدقاء" });
            modelBuilder.Entity<CompanyMarkting>().HasData(new CompanyMarkting { CompanyMarktingId = 3, TitleEn = "Our Employees", TitleAr = "موظفينا" });
            modelBuilder.Entity<CompanyMarkting>().HasData(new CompanyMarkting { CompanyMarktingId = 4, TitleEn = "No One", TitleAr = "لا احد" });

            //////
            modelBuilder.Entity<PageContent>().HasData(new PageContent { PageContentId = 1, PageTitleEn = "Privacy Policy", PageTitleAr = "سياسة الخصوصية",ContentEn= "Privacy Policy",ContentAr= "سياسة الخصوصية" });
            modelBuilder.Entity<PageContent>().HasData(new PageContent { PageContentId = 2, PageTitleEn = "Terms && Conditions", PageTitleAr = "الشروظ والاحكام",ContentEn= "Terms && Conditions", ContentAr= "الشروظ والاحكام" });
           ////
            modelBuilder.Entity<VisaTarget>().HasData(new VisaTarget { VisaTargetId = 1, TitleEn = "In UAE", TitleAr = "داخل الإمارات العربية المتحده" });
            modelBuilder.Entity<VisaTarget>().HasData(new VisaTarget { VisaTargetId = 2, TitleEn = "Out UAE", TitleAr = "خارج الإمارات العربية المتحده" });
            ///
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 1, CountryTLEN = "United States of America", CountryTLAR = "الولايات المتحده الامريكية", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 2, CountryTLEN = "Canada", CountryTLAR = "كندا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 3, CountryTLEN = "Mexico", CountryTLAR = "المكسيك", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 4, CountryTLEN = "Argentina", CountryTLAR = "ارجنتين", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 5, CountryTLEN = "United Kingdom of Great Britain and Northern Ireland", CountryTLAR = "المملكة المتحدة", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 6, CountryTLEN = "Germany", CountryTLAR = "المانيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 7, CountryTLEN = "France", CountryTLAR = "فرنسا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 8, CountryTLEN = "Italy", CountryTLAR = "ايطاليا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 9, CountryTLEN = "Spain", CountryTLAR = "اسبانيا", IsAtive = true });                 
            modelBuilder.Entity<Country>().HasData(new Country { CountryId =10, CountryTLEN = "China", CountryTLAR = "الصين", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 11, CountryTLEN = "India", CountryTLAR = "الهند", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 12, CountryTLEN = "Australia", CountryTLAR = "استراليا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 13, CountryTLEN = "South Africa", CountryTLAR = "جنوب افريقيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 14, CountryTLEN = "Netherlands", CountryTLAR = "هولندا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 15, CountryTLEN = "Belgium", CountryTLAR = "بلجيكا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 16, CountryTLEN = "Switzerland", CountryTLAR = "سويسرا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 17, CountryTLEN = "Austria", CountryTLAR = "النمسا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 18, CountryTLEN = "Sweden", CountryTLAR = "السويد", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 19, CountryTLEN = "Norway", CountryTLAR = "النرويج", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 20, CountryTLEN = "Denmark", CountryTLAR = "الدنمارك", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 21, CountryTLEN = "Finland", CountryTLAR = "فنلندا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 22, CountryTLEN = "Greece", CountryTLAR = "اليونان", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 23, CountryTLEN = "Ireland", CountryTLAR = "ايرلندا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 24, CountryTLEN = "Portugal", CountryTLAR = "البرتغال", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 25, CountryTLEN = "Poland", CountryTLAR = "بولندا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 26, CountryTLEN = "Ukraine", CountryTLAR = "اوكرانيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 27, CountryTLEN = "Romania", CountryTLAR = "رومانيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 28, CountryTLEN = "Bulgaria", CountryTLAR = "بلغاريا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 29, CountryTLEN = "Hungary", CountryTLAR = "المجر", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 30, CountryTLEN = "Turkey", CountryTLAR = "تركيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 31, CountryTLEN = "Saudi Arabia", CountryTLAR = "السعودية", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 32, CountryTLEN = "United Arab Emirates", CountryTLAR = "الإمارات العربية المتحده", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 33, CountryTLEN = "Iran", CountryTLAR = "ايران", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 34, CountryTLEN = "Palestine", CountryTLAR = "فلسطين", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 35, CountryTLEN = "Egypt", CountryTLAR = "مصر", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 36, CountryTLEN = "South Korea", CountryTLAR = "كويا الجنوبية", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 37, CountryTLEN = "North Korea", CountryTLAR = "كوريا الشمالية", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 38, CountryTLEN = "Vietnam", CountryTLAR = "فيتنام", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 39, CountryTLEN = "Thailand", CountryTLAR = "تايلاند", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 40, CountryTLEN = "Malaysia", CountryTLAR = "ماليزيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 41, CountryTLEN = "Singapore", CountryTLAR = "سنغافوره", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 42, CountryTLEN = "Indonesia", CountryTLAR = "اندونيسيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 43, CountryTLEN = "Philippines", CountryTLAR = "الفلبين", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 44, CountryTLEN = "Pakistan", CountryTLAR = "باكستان", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 45, CountryTLEN = "Bangladesh", CountryTLAR = "بنجلادش", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 46, CountryTLEN = "Sri Lanka", CountryTLAR = "سيرلانكا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 47, CountryTLEN = "Nepal", CountryTLAR = "نيبال", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 48, CountryTLEN = "New Zealand", CountryTLAR = "نيوزلاندا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 49, CountryTLEN = "Fiji", CountryTLAR = "فيجي", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 50, CountryTLEN = "Papua New Guinea", CountryTLAR = "بابو غينيا الجديدة", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 51, CountryTLEN = "Chile", CountryTLAR = "تشيلي", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 52, CountryTLEN = "Peru", CountryTLAR = "بيرو", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 53, CountryTLEN = "Colombia", CountryTLAR = "كولومبيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 54, CountryTLEN = "Venezuela", CountryTLAR = "فنزويلا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 55, CountryTLEN = "Bolivia", CountryTLAR = "بوليفيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 56, CountryTLEN = "Ecuador", CountryTLAR = "الاكوداور", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 57, CountryTLEN = "Uruguay", CountryTLAR = "اوراغواي", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 58, CountryTLEN = "Paraguay", CountryTLAR = "بارجواي", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 59, CountryTLEN = "Nigeria", CountryTLAR = "نيجيريا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 60, CountryTLEN = "Kenya", CountryTLAR = "كينيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 61, CountryTLEN = "Tanzania", CountryTLAR = "تنزانيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 62, CountryTLEN = "Uganda", CountryTLAR = "اوغندا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 63, CountryTLEN = "Ghana", CountryTLAR = "غانا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 64, CountryTLEN = "Madagascar", CountryTLAR = "مدغشقر", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 65, CountryTLEN = "Algeria", CountryTLAR = "جزائر", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 66, CountryTLEN = "Tunisia", CountryTLAR = "تونس", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 67, CountryTLEN = "Morocco", CountryTLAR = "مغرب", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 68, CountryTLEN = "Afghanistan", CountryTLAR = "افغانستان", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 69, CountryTLEN = "Jordan", CountryTLAR = "اردن", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 70, CountryTLEN = "Lebanon", CountryTLAR = "لبنان", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 71, CountryTLEN = "Qatar", CountryTLAR = "قطر", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 72, CountryTLEN = "Oman", CountryTLAR = "عمان", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 73, CountryTLEN = "Kuwait", CountryTLAR = "كويت", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 74, CountryTLEN = "Mongolia", CountryTLAR = "منغوليا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 75, CountryTLEN = "Kazakhstan", CountryTLAR = "كازاخستان", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 76, CountryTLEN = "Cambodia", CountryTLAR = "كمبوديا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 77, CountryTLEN = "Serbia", CountryTLAR = "صربيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 78, CountryTLEN = "Croatia", CountryTLAR = "كرواتيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 79, CountryTLEN = "Slovenia", CountryTLAR = "سولفينا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 80, CountryTLEN = "Slovakia", CountryTLAR = "سلوفاكيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 81, CountryTLEN = "Iceland", CountryTLAR = "ايسلندا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 82, CountryTLEN = "Belarus", CountryTLAR = "بيلاروسيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 83, CountryTLEN = "Cyprus", CountryTLAR = "قبرص", IsAtive = true });                                   
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 84, CountryTLEN = "Bosnia and Herzegovina", CountryTLAR = "البوسنه والهرسك", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 85, CountryTLEN = "Estonia", CountryTLAR = "استونيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 86, CountryTLEN = "Latvia", CountryTLAR = "لاتفيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 87, CountryTLEN = "Lithuania", CountryTLAR = "ليتوانيا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 88, CountryTLEN = "Malta", CountryTLAR = "مالطا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 89, CountryTLEN = "Luxembourg", CountryTLAR = "لوكسمبورغ", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 90, CountryTLEN = "Samoa", CountryTLAR = "ساموا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 91, CountryTLEN = "Tonga", CountryTLAR = "تونجا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 92, CountryTLEN = "Guyana", CountryTLAR = "غيانا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 93, CountryTLEN = "Suriname", CountryTLAR = "سورينام", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 94, CountryTLEN = "Guatemala", CountryTLAR = "غواتيمالا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 95, CountryTLEN = "Honduras", CountryTLAR = "هندراس", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 96, CountryTLEN = "Costa Rica", CountryTLAR = "كوستاريكا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 97, CountryTLEN = "Panama", CountryTLAR = "بنما", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 98, CountryTLEN = "El Salvador", CountryTLAR = "السلفادور", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 99, CountryTLEN = "Nicaragua", CountryTLAR = "نيكارغوا", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 100, CountryTLEN = "Brazil", CountryTLAR = "برازيل", IsAtive = true });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 101, CountryTLEN = "Japan", CountryTLAR = "يابان", IsAtive = true });

            ///////////////////////////////////////////////////////////////////////Nationalites ////////////////////
                                                                                    
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 1, NationalityTLEN = "American", NationalityTLAR = "امريكي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 2, NationalityTLEN = "Canadian", NationalityTLAR = "كندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 3, NationalityTLEN = "Mexican", NationalityTLAR = "مكسيكي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 4, NationalityTLEN = "Argentinean", NationalityTLAR = "ارجنتيني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 5, NationalityTLEN = "British", NationalityTLAR = "بريطاني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 6, NationalityTLEN = "German", NationalityTLAR = "الماني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 7, NationalityTLEN = "French", NationalityTLAR = "فرنسي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 8, NationalityTLEN = "Italian", NationalityTLAR = "ايطالي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 9, NationalityTLEN = "Spanish", NationalityTLAR = "اسبانيي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 10, NationalityTLEN = "Chinese", NationalityTLAR = "صيني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 11, NationalityTLEN = "Indian", NationalityTLAR = "هندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 12, NationalityTLEN = "Australian", NationalityTLAR = "استرالي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 13, NationalityTLEN = "South African", NationalityTLAR = "جنوب افريقي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 14, NationalityTLEN = "Dutch", NationalityTLAR = "هولندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 15, NationalityTLEN = "Belgian", NationalityTLAR = "بلجيكي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 16, NationalityTLEN = "Swiss", NationalityTLAR = "سويسري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 17, NationalityTLEN = "Austrian", NationalityTLAR = "نمساوي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 18, NationalityTLEN = "Swedish", NationalityTLAR = "سويدي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 19, NationalityTLEN = "Norwegian", NationalityTLAR = "نرويجي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 20, NationalityTLEN = "Danish", NationalityTLAR = "دنماركي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 21, NationalityTLEN = "Finnish", NationalityTLAR = "فنلندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 22, NationalityTLEN = "Greek", NationalityTLAR = "يوناني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 23, NationalityTLEN = "Irish", NationalityTLAR = "ايرلندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 24, NationalityTLEN = "Portuguese", NationalityTLAR = "برتغالي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 25, NationalityTLEN = "Polish", NationalityTLAR = "بولندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 26, NationalityTLEN = "Ukrainian", NationalityTLAR = "اوكراني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 27, NationalityTLEN = "Romanian", NationalityTLAR = "روماني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 28, NationalityTLEN = "Bulgarian", NationalityTLAR = "بلغاري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 29, NationalityTLEN = "Hungarian", NationalityTLAR = "مجري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 30, NationalityTLEN = "Turkish", NationalityTLAR = "تركي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 31, NationalityTLEN = "Saudi", NationalityTLAR = "سعودي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 32, NationalityTLEN = "Emirati", NationalityTLAR = "إماراتي ", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 33, NationalityTLEN = "Iranian", NationalityTLAR = "ايراني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 34, NationalityTLEN = "Palestinian", NationalityTLAR = "فلسطيني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 35, NationalityTLEN = "Egyptian", NationalityTLAR = "مصري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 36, NationalityTLEN = "Korean", NationalityTLAR = "كوري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 37, NationalityTLEN = "Brazilian", NationalityTLAR = "برازيلي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 38, NationalityTLEN = "Vietnamese", NationalityTLAR = "فيتنامي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 39, NationalityTLEN = "Thai", NationalityTLAR = "تايلاندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 40, NationalityTLEN = "Malaysian", NationalityTLAR = "ماليزي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 41, NationalityTLEN = "Singaporean", NationalityTLAR = "سنغافوري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 42, NationalityTLEN = "Indonesian", NationalityTLAR = "اندونيسي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 43, NationalityTLEN = "Filipino", NationalityTLAR = "فلبيني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 44, NationalityTLEN = "Pakistani", NationalityTLAR = "باكستاني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 45, NationalityTLEN = "Bangladeshi", NationalityTLAR = "بنجلادشي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 46, NationalityTLEN = "Lankan", NationalityTLAR = "سيرلانكي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 47, NationalityTLEN = "Nepali", NationalityTLAR = "نيبالي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 48, NationalityTLEN = "Zealander", NationalityTLAR = "نيوزلاندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 49, NationalityTLEN = "Fijian", NationalityTLAR = "فيجي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 50, NationalityTLEN = "Papua New Guinean", NationalityTLAR = " غينيا الجديدة", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 51, NationalityTLEN = "Chilean", NationalityTLAR = "تشيلي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 52, NationalityTLEN = "Peruvian", NationalityTLAR = "بيرو", IsAtive = true });                                                                                      
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 53, NationalityTLEN = "Colombian", NationalityTLAR = "كولومبيي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 54, NationalityTLEN = "Venezuelan", NationalityTLAR = "فنزويلي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 55, NationalityTLEN = "Bolivian", NationalityTLAR = "بوليفي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 56, NationalityTLEN = "Ecuadorian", NationalityTLAR = "اكوداوري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 57, NationalityTLEN = "Uruguayan", NationalityTLAR = "اوراغواني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 58, NationalityTLEN = "Paraguayan", NationalityTLAR = "بارجواني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 59, NationalityTLEN = "Nigerian", NationalityTLAR = "نيجيري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 60, NationalityTLEN = "Kenyan", NationalityTLAR = "كيني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 61, NationalityTLEN = "Tanzanian", NationalityTLAR = "تنزاني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 62, NationalityTLEN = "Ugandan", NationalityTLAR = "اوغندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 63, NationalityTLEN = "Ghanaian", NationalityTLAR = "غانا", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 64, NationalityTLEN = "Malagasy", NationalityTLAR = "مدغشقر", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 65, NationalityTLEN = "Algerian", NationalityTLAR = "جزائري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 66, NationalityTLEN = "Tunisian", NationalityTLAR = "تونسي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 67, NationalityTLEN = "Moroccan", NationalityTLAR = "مغربي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 68, NationalityTLEN = "Afghan", NationalityTLAR = "افغانستاني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 69, NationalityTLEN = "Jordanian", NationalityTLAR = "اردني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 70, NationalityTLEN = "Lebanese", NationalityTLAR = "لبناني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 71, NationalityTLEN = "Qatari", NationalityTLAR = "قطري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 72, NationalityTLEN = "Omani", NationalityTLAR = "عماني", IsAtive = true });                                                                                          
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 73, NationalityTLEN = "Kuwaiti", NationalityTLAR = "كويتي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 74, NationalityTLEN = "Mongolian", NationalityTLAR = "منغولي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 75, NationalityTLEN = "Kazakh", NationalityTLAR = "كازاخستاني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 76, NationalityTLEN = "Cambodian", NationalityTLAR = "كمبودي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 77, NationalityTLEN = "Serbian", NationalityTLAR = "صربي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 78, NationalityTLEN = "Croatian", NationalityTLAR = "كرواتي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 79, NationalityTLEN = "Slovenian", NationalityTLAR = "سولفيني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 80, NationalityTLEN = "Slovak", NationalityTLAR = "سلوفاكي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 81, NationalityTLEN = "Icelandic", NationalityTLAR = "ايسلندي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 82, NationalityTLEN = "Belarusian", NationalityTLAR = "بيلاروسي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 83, NationalityTLEN = "Cypriot", NationalityTLAR = "قبرصي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 84, NationalityTLEN = "Bosnian", NationalityTLAR = "بوسنه", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 85, NationalityTLEN = "Estonian", NationalityTLAR = "استواني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 86, NationalityTLEN = "Latvian", NationalityTLAR = "لاتيفي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 87, NationalityTLEN = "Lithuanian", NationalityTLAR = "ليتواني", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 88, NationalityTLEN = "Maltese", NationalityTLAR = "مالطا", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 89, NationalityTLEN = "Luxembourger", NationalityTLAR = "لوكسمبورغ", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 90, NationalityTLEN = "Samoa", NationalityTLAR = "ساموا", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 91, NationalityTLEN = "Tongan", NationalityTLAR = "تونجا", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 92, NationalityTLEN = "Guyanese", NationalityTLAR = "غيانا", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 93, NationalityTLEN = "Surinamese", NationalityTLAR = "سورينامي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 94, NationalityTLEN = "Guatemalan", NationalityTLAR = "غواتيمالا", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 95, NationalityTLEN = "Honduran", NationalityTLAR = "هندراسي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 96, NationalityTLEN = "Costa Rican", NationalityTLAR = "كوستاريكاي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 97, NationalityTLEN = "Panamanian", NationalityTLAR = "بنمي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 98, NationalityTLEN = "Salvadoran", NationalityTLAR = "سلفادوري", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 99, NationalityTLEN = "Nicaraguan", NationalityTLAR = "نيكارغوي", IsAtive = true });
            modelBuilder.Entity<Nationality>().HasData(new Nationality { NationalityId = 100, NationalityTLEN = "Japanese", NationalityTLAR = "ياباني", IsAtive = true });




        }


    }

}
