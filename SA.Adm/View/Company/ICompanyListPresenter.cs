using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using SA.Repository.Domain;
using System.Windows.Input;


namespace SA.Adm.View.Company
{
    public interface ICompanyListPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        ObservableCollection<Empresa> Empresas { get; }
        Empresa Empresa { get; set; }

        #region Commands
        /// <summary>
        /// Create command - called when adding a Company is requeired
        /// </summary>
        ICommand CreateCommand { get; }

        /// <summary>
        /// Read command - called when reading a Company is requeired
        /// </summary>
        //ICommand ReadCommand { get; }

        /// <summary>
        /// Update command - called when updating a Company is requeired
        /// </summary>
        ICommand UpdateCommand { get; }

        /// <summary>
        /// Delete command - called when deleting a Company is requeired
        /// </summary>
        ICommand DeleteCommand { get; }

        /// <summary>
        /// Search command - called when searching is requeired
        /// </summary>
        ICommand SearchCommand { get; }

        /// <summary>
        /// Close command - called when closing is requeired
        /// </summary>
        ICommand CloseCommand { get; }
        #endregion
    }
}
