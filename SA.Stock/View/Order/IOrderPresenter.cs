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


namespace SA.Stock.View.Order
{
    public interface IOrderPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        Pedido Pedido { get; }

        Cadastro Cadastro { get; set; }
        DateTime Data { get; set; }
        DateTime Hora { get; set; }
        OrderType Tipo { get; set; }
        Vendedor Vendedor { get; set; }
        DateTime DataValidade { get; set; }

        PhaseType Fase { get; }

        Double Valor { get; }
        Double ValorDesconto { get; set; }
        Double ValorDescontoTotal { get; }

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
        /// Search Register command - called when searching a Register is requeired
        /// </summary>
        ICommand SearchRegisterCommand { get; }
        /// <summary>
        /// Search Vendor command - called when searching a Vendor is requeired
        /// </summary>
        ICommand SearchVendorCommand { get; }
        #endregion
    }
}
