using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using SA.Repository.Domain;
using System.Windows.Input;
using SA.Repository.Enums;


namespace SA.Financial.View.FinancialAccount
{
    public interface IFinancialAccountPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        FinancialAccountType Tipo { get; set; }
        ActiveInactiveType Situacao { get; set; }
        ContaFinanceira ContaFinanceira { get; }

        Cep CobrancaCep { get; set; }
        int CobrancaNumero { get; set; }
        string CobrancaComplemento { get; set; }

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
        /// Search Zip Code command - called when searching a Zip Code is requeired
        /// </summary>
        ICommand SearchZipCodeCommand { get; }
        #endregion
    }
}
