using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Address.View.State
{
    public interface IStateListView
    {
        void SetPresenter(IStateListPresenter presenter);
    }
}
