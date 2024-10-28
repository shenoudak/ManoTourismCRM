using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Localization;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using ManoTourism.Data;
using ManoTourism.Models;
using ManoTourism.DataTables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using ManoTourism.ViewModels;
using NToastNotify;
using Microsoft.AspNetCore.Identity;

namespace ManoTourism.Report
{
    public partial class RequestReport : DevExpress.XtraReports.UI.XtraReport
    {
        //public IRequestCultureFeature locale;
        public string BrowserCulture;
        public RequestReport(string language)
        {
            BrowserCulture = language;
            
            InitializeComponent();
        }

        private void EmployeeReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RequestReport requestReport = new RequestReport(BrowserCulture);
            if (BrowserCulture == "en-US")
            {
                requestReport.RightToLeftLayout = RightToLeftLayout.No;
                requestReport.RightToLeft = RightToLeft.No;
               



            }
            else
            {
                requestReport.RightToLeftLayout = RightToLeftLayout.Yes;
                requestReport.RightToLeft = RightToLeft.Yes;
               
                tableCell2.Text = "الموظف المسئول";
                xrTableCell1.Text = "تاريخ الإسناد";
                tableCell5.Text = "تاريخ الانتهاء";
                tableCell8.Text = "الخدمه المطلوبه";
                tableCell9.Text = "إضيفت بواسطة";
                tableCell12.Text = "صاحب الطلب";
                tableCell15.Text = "حاله الطلب";
                invoiceLabel.Text = "تقرير الموظفين";
                thankYouLabel.Text = "مانو للسياحه";
               
            }

        }
            
    }
}
