    using System;
    using System.Collections.Generic;
    using System.Linq;



namespace Calculator
{
    class Program
    {

        
        static void Main(string[] args)
        {
            Start();

        }

        public static List<ResultListModel> GetResul(Credit credit)
        {

            List<ResultListModel> resultLists = credit.GetMontsPayments();
            foreach (var list in resultLists)
            {
                Console.WriteLine($"{list.MonthNumber}    {list.SumCreditBalance}     {list.Principal}    {list.MonthPercent}   {list.MonthCredit}");
            }
            return resultLists;
        }
        public static void Start()
        {
            Console.WriteLine("Write type of your credirt:Annuity->1  Diferency->2");
            int type = int.Parse(Console.ReadLine());
            Console.WriteLine("Credit:");
           decimal Credit = decimal.Parse(Console.ReadLine());
           Console.WriteLine("Annual percentage:");
           double Percent = double.Parse(Console.ReadLine());
            Console.WriteLine("Count of monts:");
           short Months = short.Parse(Console.ReadLine());

            switch (type)
            {
                case (int)CreditEnums.Annity:
                    Credit creditA = new AnnuityCredit(Credit,Percent,Months);
                   List<ResultListModel> resultListA= GetResul(creditA);
                    double PTA=Math.Round(Xirr(creditA,resultListA),3);
                    Console.WriteLine();
                    Console.WriteLine(PTA*100);
                    break;

                case (int)CreditEnums.Differency:
                    Credit creditD = new DiferencialCredit(Credit, Percent, Months);
                    List<ResultListModel> resultListB=GetResul(creditD);
                    double PTD=Math.Round(Xirr(creditD, resultListB),3);
                    Console.WriteLine();
                    Console.WriteLine(PTD*100);
                    break;

                default:
                    Console.WriteLine("Please write 1 or 2!");
                    Start();
                    break;

            }
        }
        
        public static double Xirr(Credit credit,List<ResultListModel> list)
        {

            List<double> payment=new List<double>();
            payment.Add(-(double)(list.FirstOrDefault().SumCreditBalance + list.FirstOrDefault().Principal));

            foreach(var lis in list)
            {
                payment.Add((double)lis.MonthCredit);
                
            };

            var days = new double[credit.Months + 1];
            int j = 0;
            for (int i = 1; i <= Math.Ceiling((decimal)credit.Months / 12); i++)
            {
                if (j == 0)
                {
                    days[j] = i;
                    j++;
                }
                for (int k = 1; j <= i * 12; j++, k++)
                {
                    if (j == credit.Months + 1)
                    {
                        break;
                    }
                    days[j] = days[j - 1] + DateTime.DaysInMonth(DateTime.Now.Year, k);

                }
            }
            double[] payments = payment.ToArray();
            double result= XIRR.Newtons_method(0.1, XIRR.total_f_xirr(payments, days), XIRR.total_df_xirr(payments, days));
            return result;
            
        }
    }
}
