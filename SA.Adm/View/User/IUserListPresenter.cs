using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

using Microsoft.Practices.Composite.Presentation.Commands;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using SA.Repository.Domain;


namespace SA.Adm.View.User
{
    public interface IUserListPresenter: ICrumbViewContent, INotifyPropertyChanged
    {
        ObservableCollection<Usuario> Usuarios { get; }
        Usuario Usuario { get; set; }

        #region Commands
        /// <summary>
        /// Create command - called when adding a user is requeired
        /// </summary>
        DelegateCommand<Usuario> CreateCommand { get; }

        /// <summary>
        /// Read command - called when reading a user is requeired
        /// </summary>
        //DelegateCommand<Usuario> ReadCommand { get; }

        /// <summary>
        /// Update command - called when updating a user is requeired
        /// </summary>
        DelegateCommand<Usuario> UpdateCommand { get; }

        /// <summary>
        /// Delete command - called when deleting a user is requeired
        /// </summary>
        DelegateCommand<Usuario> DeleteCommand { get; }

        /// <summary>
        /// Search command - called when searching is requeired
        /// </summary>
        DelegateCommand<object> SearchCommand { get; }
        #endregion
    }
}
