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

namespace SA.Address.View.ZipCode
{
    public interface IZipCodePresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        Cep Cep { get; }
        string CEP { get; set; }
        string Logradouro { get; set; }
        string Bairro { get; set; }

        int CidadeID { get; }
        Cidade Cidade { get; }
  
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
        /// Search State command - called when searching a State is requeired
        /// </summary>
        ICommand SearchCityCommand { get; }
        #endregion

    }
}
