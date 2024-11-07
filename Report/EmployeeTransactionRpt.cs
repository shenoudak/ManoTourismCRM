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
    public partial class EmployeeTransactionRpt : DevExpress.XtraReports.UI.XtraReport
    {
        //public IRequestCultureFeature locale;
        public string BrowserCulture;
        public EmployeeTransactionRpt(string language)
        {
            BrowserCulture = language;
            
            InitializeComponent();
        }

        private void EmployeeReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            EmployeeReport employeeReport = new EmployeeReport(BrowserCulture);
            if (BrowserCulture == "en-US")
            {
                employeeReport.RightToLeftLayout = RightToLeftLayout.No;
                employeeReport.RightToLeft = RightToLeft.No;
                //RightToLeftLayout rightToLeftLayout = new RightToLeftLayout();


                //rightToLeftLayout(RightToLeftLayout.No);



            }
            else
            {
                employeeReport.RightToLeftLayout = RightToLeftLayout.Yes;
                employeeReport.RightToLeft = RightToLeft.Yes;
               
                tableCell2.Text = "الموظف المسئول";
                xrTableCell1.Text = "تاريخ الإسناد";
                tableCell5.Text = "تاريخ الانتهاء";
                tableCell8.Text = "الخدمه المطلوبه";
                tableCell9.Text = "إضيفت بواسطة";
                tableCell12.Text = "صاحب الطلب";
                tableCell15.Text = "حاله الطلب";
                invoiceLabel.Text = "تقرير للطلبات المسندة للموظف";
                thankYouLabel.Text = "مانو للسياحه";
                xrTableCell3.Text = "تقرير كامل لكل الطلبات";
               
            }

        }
            
    }
}
