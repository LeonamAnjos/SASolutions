﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SA.BreadCrumb.Model;
using System.ComponentModel;

using System.Windows.Input;
using SA.Repository.Domain;


namespace SA.Address.View.State
{
    public interface IStateListPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        ObservableCollection<Estado> Estados { get; }
        Estado Estado { get; set; }

        #region Commands
        /// <summary>
        /// Create command - called when adding a state is requeired
        /// </summary>
        ICommand CreateCommand { get; }

        /// <summary>
        /// Read command - called when reading a state is requeired
        /// </summary>
        //ICommand ReadCommand { get; }

        /// <summary>
        /// Update command - called when updating a state is requeired
        /// </summary>
        ICommand UpdateCommand { get; }

        /// <summary>
        /// Delete command - called when deleting a state is requeired
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
