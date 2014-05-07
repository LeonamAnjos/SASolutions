using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using SA.BreadCrumb.Model;
using SA.Repository.Domain;
using System.Windows.Input;

namespace SA.Financial.View.Register
{
    public interface IRegisterListPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        ObservableCollection<Cadastro> Cadastros { get; }
        Cadastro Cadastro { get; set; }

        #region Commands
        /// <summary>
        /// Create command - called when adding a Register is requeired
        /// </summary>
        ICommand CreateCommand { get; }

        /// <summary>
        /// Read command - called when reading a Register is requeired
        /// </summary>
        //ICommand ReadCommand { get; }

        /// <summary>
        /// Update command - called when updating a Register is requeired
        /// </summary>
        ICommand UpdateCommand { get; }

        /// <summary>
        /// Delete command - called when deleting a Register is requeired
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
