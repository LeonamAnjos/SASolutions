using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Infrastructure;
using Microsoft.Practices.Composite.Presentation.Commands;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

using System.Windows.Input;
using SA.Repository.Domain;

namespace SA.Address.View.Country
{
    public interface ICountryPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        Pais Pais { get; }
        string Nome     { get; set; }
        string Sigla    { get; set; }

        #region Commands
        /// <summary>
        /// Submit command - called when action is submited to be applied
        /// </summary>
        ICommand SubmitCommand { get; }
        /// <summary>
        /// Cancel command - called when action is canceld
        /// </summary>
        ICommand CancelCommand { get; }
        #endregion

    }
}
