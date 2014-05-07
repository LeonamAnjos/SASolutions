using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Infrastructure;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

using System.Windows.Input;
using SA.Repository.Domain;

namespace SA.Address.View.State
{
    public interface IStatePresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        Estado Estado { get; }
        string Nome     { get; set; }
        string Sigla { get; set; }

        int PaisID { get; }
        Pais Pais { get; }

        

        #region Commands
        /// <summary>
        /// Submit command - called when action is submited to be applied
        /// </summary>
        ICommand SubmitCommand { get; }
        /// <summary>
        /// Cancel command - called when action is canceld
        /// </summary>
        ICommand CancelCommand { get; }
        /// <summary>
        /// Search country command - called when searching a country is requeired
        /// </summary>
        ICommand SearchCountryCommand { get; }
        #endregion

    }
}
