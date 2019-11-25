using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public abstract class Credit
    {
        public Credit(decimal sumcredit,double percent,short months)
        {
            Months = months;
            Percent = percent;
            SumCredit = sumcredit;
        }
        public decimal SumCredit { get;  set; }
        public double Percent { get;  set; }
        public short Months { get; set; }

        public abstract List<ResultListModel> GetMontsPayments();
        
        

    }
}
