using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class SecurityValues
    {
        private List<double> priceNum;
        private List<DateTime> dateNum;
        private double[] dayAverage;
        private double[] lowerBand;
        private double[] upperBand;
        private double[] standardDev;
        private String[] marketSignalVar;
        private Boolean myBool = false;
        public SecurityValues(List<DateTime> date, List<double> price)
        {
            priceNum = new List<double>(price);
            dateNum = new List<DateTime>(date);
            dayAverage = new double[date.Count];
            upperBand = new double[date.Count];
            lowerBand = new double[date.Count];
            standardDev = new double[date.Count];
            marketSignalVar = new String[date.Count];
        }
        public void calculateValues()
        {
          
            standardDeviation();
            calcMarketSignal();
        }
        public double AverageCalc(double[] values){
            double newAverage=values.Average();
            double sumValuesAverage = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sumValuesAverage = sumValuesAverage + values[i];

            
            }

        

            return newAverage;
        }

        public double[] standardDeviation()
        {
       
           
            for (int i=0; i < priceNum.Count; i++)
            {
                double[] subSet = new double[20];
                
                double firstAverage = 0;
                double secondAverage = 0;
                if (i < priceNum.Count - 20)
                {
                    for (int x = 0; x < 20; x++)        //get 20 values
                    {
                        subSet[x] = priceNum[i+x];
                      

                    }
                  
                    firstAverage = AverageCalc(subSet);     //find average

                    for (int x = 0; x < 20; x++)        //subtract average or mean from each of the original 20 values
                    {
                        Console.Write(subSet[x]- firstAverage+"\n");
                        subSet[x] = Math.Pow(Math.Abs(subSet[x] - firstAverage),2);
                        Console.Write(subSet[x]+"\n");
                        
                       
                    }
                    secondAverage = AverageCalc(subSet);
                }
                standardDev[i] = Math.Sqrt(secondAverage);
                dayAverage[i] = firstAverage;
                upperBand[i] = firstAverage + Math.Sqrt(secondAverage);
                lowerBand[i] = firstAverage - Math.Sqrt(secondAverage);
            }

            return standardDev;
        }

        public String[] calcMarketSignal()
        {

            for (int i = 0; i < priceNum.Count; i++)
            {

                if (priceNum[i] < lowerBand[i])
                    myBool = true;

                if (priceNum[i] > upperBand[i])
                {
                    marketSignalVar[i] = "BUY";
                    myBool=false;
                }
                else if(priceNum[i] > dayAverage[i]&&myBool==true){
                    marketSignalVar[i] = "BUY";
                }
                else
                {
                    marketSignalVar[i] = "SELL";
                 
                }



            }



            return marketSignalVar;
        }
        public String[] getMarketSignal()
        {
            return marketSignalVar;
        }

        public double[] getAverages()
        {
            return dayAverage;
        }
        public double[] getStandardDeviation()
        {
            return standardDev;
        }
        public List<double> getPrice()
        {
            return priceNum;
        }
        public double[] getUpperBand()
        {
            return upperBand;
        }
        public double[] getLowerBand()
        {
            return lowerBand;
        }
        public List<DateTime> getDate()
        {

            return dateNum;

        }

        public int count()
        {
            return priceNum.Count;
        }

    }
}
