using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using SA.Repository.Domain;
using System.Windows.Input;


namespace SA.Stock.View.SubGroup
{
    public interface ISubGroupPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        SubGrupo SubGrupo { get; }
        string Descricao { get; set; }
        string GrupoDescricao { get; }

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
