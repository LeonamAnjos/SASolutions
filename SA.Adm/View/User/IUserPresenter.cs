using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Infrastructure;
using Microsoft.Practices.Composite.Presentation.Commands;

using SA.BreadCrumb.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SA.Repository.Domain;
using SA.Repository.Enums;

namespace SA.Adm.View.User
{
    public interface IUserPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        Usuario Usuario { get; }
        string Login    { get; set; }
        string Senha    { get; }
        string SenhaConfirmacao { get; }
        string Nome     { get; set; }
        string Email    { get; set; }
        ActiveInactiveType Situacao { get; set; }

        int Grupo { get; }
        UsuarioGrupo UsuarioGrupo { get; }

        #region Commands
        /// <summary>
        /// Submit command - called when action is submited to be applied
        /// </summary>
        DelegateCommand<object> SubmitCommand { get; }
        /// <summary>
        /// Cancel command - called when action is canceld
        /// </summary>
        DelegateCommand<object> CancelCommand { get; }
        /// <summary>
        /// Search user group command - called when searching a user group is requeired
        /// </summary>
        DelegateCommand<object> SearchUserGroupCommand { get; }
        #endregion

    }
}
