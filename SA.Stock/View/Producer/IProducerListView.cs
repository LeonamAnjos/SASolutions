using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Stock.View.Producer
{
    public interface IProducerListView
    {
        void SetPresenter(IProducerListPresenter presenter);
    }
}
