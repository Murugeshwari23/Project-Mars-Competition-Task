using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace Competition_Task_ProjectMars.Utilities
{
    public class ExtentReportManager
    {
        private static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;

        public static ExtentReports getInstance()
        {
            if (extent == null)
            {
                extent = new ExtentReports();
                htmlReporter = new ExtentHtmlReporter("C:\\Competition Task-Project Mars\\Project-Mars-Competition-Task\\Competition Task-ProjectMars\\Competition Task-ProjectMars\\Reports\\"); // Path to the report file
                extent.AttachReporter(htmlReporter);
            }
            return extent;
        }
    }
}
