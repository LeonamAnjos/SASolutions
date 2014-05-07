using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Financial.View.FinancialAccount
{
    public interface IFinancialAccountView
    {
        void SetPresenter(IFinancialAccountPresenter presenter);
    }
}
