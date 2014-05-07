using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using System.Windows.Input;
using SA.Repository.Domain;


namespace SA.Address.View.ZipCode
{
    public interface IZipCodeListPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        ObservableCollection<Cep> Ceps { get; }
        Cep Cep { get; set; }

        #region Commands
        /// <summary>
        /// Create command - called when adding a ZipCode is requeired
        /// </summary>
        ICommand CreateCommand { get; }

        /// <summary>
        /// Read command - called when reading a ZipCode is requeired
        /// </summary>
        //ICommand ReadCommand { get; }

        /// <summary>
        /// Update command - called when updating a ZipCode is requeired
        /// </summary>
        ICommand UpdateCommand { get; }

        /// <summary>
        /// Delete command - called when deleting a ZipCode is requeired
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
