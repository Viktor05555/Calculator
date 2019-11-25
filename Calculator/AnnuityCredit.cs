using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class AnnuityCredit : Credit
    {
        public AnnuityCredit(decimal sumcredit, double percent, short months) :base(sumcredit,percent,months){}
        public override List<ResultListModel> GetMontsPayments()
        {
            decimal mountPayment = ((SumCredit * (decimal)Percent) / 1200);
            double argument = Math.Pow(1 + (Percent / 1200), Months);
            decimal result = mountPayment / (1 - (decimal)(1 / argument));

            List<ResultListModel> resultList = new List<ResultListModel>();

            for (int i = 1; i <= Months; i++)
            {
                ResultListModel model = new ResultListModel();
                model.MonthNumber = i;
                model.MonthPercent = Math.Round(SumCredit * (decimal)Percent / 1200,1);
                model.Principal =Math.Round( result - model.MonthPercent,1);
                model.MonthCredit = Math.Round(result,1);
                model.SumCreditBalance = Math.Round(SumCredit - model.Principal);
                SumCredit = SumCredit - model.Principal;
                resultList.Add(model);
            }
            return resultList;
            
        }
        
    }
}
