using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices; 

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        

        }
       
        //creat com objects
        Excel.Application xlApp = new Excel.Application();
        SecurityValues securityPrice;
        SecurityValues benchmark;

        private void button1_Click(object sender, EventArgs e)
        {
            var lineOfData = new List<string>();
            int count = 0;
           var date = new List<DateTime>();
            var price = new List<double>();
            Excel.Workbook xlBenchmark=null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            
            openFileDialog1.Title = "Select a File";  
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                xlBenchmark = xlApp.Workbooks.Open(openFileDialog1.FileName);
              
            }

            Excel.Worksheet xlWorksheet = xlBenchmark.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;


            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {

                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        //get first value in the row, date
                        if (j == 1)
                        {
                        
                            DateTime conv = DateTime.FromOADate(xlRange.Cells[i, j].Value2);

                            date.Add(conv);
                        }
                        //gets second value which is the actual value of the security
                        if (j == 2)
                        {
                            price.Add(xlRange.Cells[i, j].Value2);
                      
                        }
                    }

                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

   
            //close and release
            xlBenchmark.Close();

            //quit and release
            xlApp.Quit();
  
       
            benchmark = new SecurityValues(date,price);
            benchmark.calculateValues();


            Console.Write("index count: " + benchmark.count());
       


        }

    
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var lineOfData = new List<string>();
            int count = 0;
            var date = new List<DateTime>();
            var price = new List<double>();
            Excel.Workbook xlValueList = null;
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            Console.Write("click: ");
            openFileDialog2.Title = "Select a File";
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                xlValueList = xlApp.Workbooks.Open(openFileDialog2.FileName);

            }

            Excel.Worksheet xlWorksheet = xlValueList.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;


            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {

                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        //get first value in the row, date
                        if (j == 1)
                        {

                            DateTime conv = DateTime.FromOADate(xlRange.Cells[i, j].Value2);

                            date.Add(conv);
                        }
                        //gets second value which is the actual value of the security
                        if (j == 2)
                        {
                            price.Add(xlRange.Cells[i, j].Value2);

                        }
                    }

                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //close and release
            xlValueList.Close();


            //quit and release
            xlApp.Quit();
 

            securityPrice = new SecurityValues(date, price);
         

            Console.Write("security count: " + securityPrice.count());
      


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Utility.calculateInvestmentValue(benchmark, securityPrice);
        }

       


    }
}
