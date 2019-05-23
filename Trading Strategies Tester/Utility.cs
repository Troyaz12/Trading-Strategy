using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Utility
    {
       
        int currentDay = 0;
       
        private static SecurityValues fundValues;      //history of security to be bought and sold
        private static List<DateTime> dates;

        private static double[] stdDev;
        private static double[] bmAverage;
        private static List<DateTime> dateTime;
        private static List<double> priceBm;
        private static double[] upper;
        private static double[] lower;
        private static String[] marketSig;
        private static List<double> priceFund;


        public static void calculateInvestmentValue(SecurityValues benchmark, SecurityValues fund){

            double currentValue = 10000.00;      //value of investment
            double currentShares = 0;      //shares held of investment
            stdDev = benchmark.getStandardDeviation();
            bmAverage = benchmark.getAverages();        
            priceBm = benchmark.getPrice();
            upper = benchmark.getUpperBand();
            lower = benchmark.getLowerBand();
            marketSig = benchmark.getMarketSignal();

            fundValues = fund;
            dates = fund.getDate();
            priceFund = fund.getPrice();

            Boolean holding = false;
            int lengthTotal = marketSig.Length - 20;
            for (int x = lengthTotal; x >= 0; x--)
            {
                if (marketSig[x].Equals("BUY")&&holding.Equals(false))
                {
                    holding = true;
                    Console.Write("BUY amount on this day is: " + findPrice(dates[x])+"\n");
                    currentShares = currentValue / findPrice(dates[x]);

                }else if(marketSig[x].Equals("SELL")&&holding.Equals(true)){
                     Console.Write("SELL amount on this day is: " + findPrice(dates[x])+"\n");
                     holding = false;

                     currentValue = currentShares * findPrice(dates[x]);
                }

            }

            Console.Write("Final Value: " + currentValue + "\n");
            

        }



        public static Double findPrice(DateTime date)
        {
            double securityValue=fundValues.count()-1;           //value of security to be bought and sold
            for (int x = 0; x<fundValues.count(); x++)
            {
                if (date.AddDays(1) == dates[x])
                {
                    securityValue = priceFund[x];


                }
                else if (date.AddDays(2) == dates[x])
                {
                    securityValue = priceFund[x];

                }
                else if (date.AddDays(3) == dates[x])
                {
                    securityValue = priceFund[x];

                }
                else if (date.AddDays(4) == dates[x])
                {
                    securityValue = priceFund[x];

                }
                else if (x == 0)
                {
                    securityValue = priceFund[x];
                }


            }
            return securityValue;

        }







    }
}
