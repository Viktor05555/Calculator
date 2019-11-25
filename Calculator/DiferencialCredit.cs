using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class DiferencialCredit : Credit
    {
        public DiferencialCredit(decimal sumCredit,double percent,short months):base(sumCredit,percent,months)
        {
                
        }
        public override List<ResultListModel> GetMontsPayments()
        {
            List<ResultListModel> resultList = new List<ResultListModel>();
                decimal principal= Math.Round(SumCredit / Months);
           
            for (int i = 1; i <= Months; i++)
            {
                decimal monthPercent = Math.Round(SumCredit * (decimal)Percent / 1200, 1);
                SumCredit = Math.Round(SumCredit - principal);
                decimal montCredit = principal + monthPercent;
                resultList.Add(new ResultListModel
                {
                    MonthCredit = montCredit,
                    MonthNumber = i,
                    MonthPercent = monthPercent,
                    Principal = principal,
                    SumCreditBalance=SumCredit,
                }) ;
            }
            return resultList;
        }
    }
}
