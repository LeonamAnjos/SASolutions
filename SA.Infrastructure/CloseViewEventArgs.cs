using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA.Infrastructure
{
    public enum CloseViewType
    {
        Submit, Cancel, Ok, Close
    }

    public class CloseViewEventArgs : EventArgs
    {
        #region Constructors
        public CloseViewEventArgs(CloseViewType closeViewOption)
        {
            this.CloseViewOption = closeViewOption;
        }
        #endregion

        #region Getters and Setters
        public CloseViewType CloseViewOption { get; private set; }
        #endregion


    }
}
