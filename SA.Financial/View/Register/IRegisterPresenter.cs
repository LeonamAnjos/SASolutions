using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using SA.Repository.Domain;
using System.Windows.Input;
using SA.Repository.Enums;
using System.Collections.ObjectModel;


namespace SA.Financial.View.Register
{
    public interface IRegisterPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        Cadastro Cadastro { get; }
        PersonType Tipo { get; set; }
        ActiveInactiveType Situacao { get; set; }
        string Nome { get; set; }
        string RazaoSocial { get; set; }
        string Contato { get; set; }
        string CPF { get; set; }
        string RG { get; set; }

        Cep CorrespCep { get; set; }
        int CorrespNumero { get; set; }
        string CorrespComplemento { get; set; }

        string Telefone { get; set; }
        string Celular { get; set; }
        string Fax { get; set; }
        string EMail { get; set; }

        DateTime DataNascimento { get; set; }
        DateTime DataInclusao { get; }
        DateTime DataAlteracao { get; }

        ObservableCollection<ContaFinanceira> ContasFinanceiras { get; }
        ContaFinanceira ContaFinanceira { get; set; }

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
        /// Search Zip Code Mail command - called when searching a Zip Code Mail is requeired
        /// </summary>
        ICommand SearchZipCodeMailCommand { get; }
        /// <summary>
        /// Add Financial Account command - called when adding a Financial Account is requeired
        /// </summary>
        ICommand AddFinancialAccountCommand { get; }
        /// <summary>
        /// Update Financial Account command - called when updating a Financial Account is requeired
        /// </summary>
        ICommand UpdateFinancialAccountCommand { get; }
        
        #endregion
    }
}
