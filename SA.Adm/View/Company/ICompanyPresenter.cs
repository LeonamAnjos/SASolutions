using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Infrastructure;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SA.Repository.Domain;
using SA.Repository.Enums;
using System.Windows.Input;

namespace SA.Adm.View.Company
{
    public interface ICompanyPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        Empresa Empresa { get; }
        string Nome { get; set; }
        string CNPJ { get; set; }
        string InscricaoEstadual { get; set; }
        string Complemento { get; set; }
        int Numero { get; set; }
        Cep Cep { get; set; }

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
        /// Search State command - called when searching a Zip Code is requeired
        /// </summary>
        ICommand SearchZipCodeCommand { get; }
        #endregion
    }
}
