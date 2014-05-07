using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using SA.Repository.Domain;
using SA.Repository.Enums;
using System.Windows.Input;

namespace SA.Adm.View.UserGroup
{
    public interface IUserGroupPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        UsuarioGrupo Grupo { get; }
        string Descricao { get; set; }
        UserGroupType Tipo { get; set; }

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
