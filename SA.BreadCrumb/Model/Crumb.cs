using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.BreadCrumb.Model
{
    public class Crumb : ICrumb
    {
        #region ICrumb
        public string Title { get; private set; }
        public string Description { get; set; }
        public ICrumbViewContent Content { get; private set; }
        #endregion

        #region Constructor
        public Crumb(string title, ICrumbViewContent content)
        {
            this.Title = title;
            this.Content = content;

            //this.Description = "Unidade";
        }
        #endregion
    }
}
