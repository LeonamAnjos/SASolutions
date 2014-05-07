using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using SA.Repository.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SA.Stock.ViewModel
{
    public interface IOrderViewModel : INotifyPropertyChanged
    {
        #region Order
        Cadastro Cadastro { get; set; }
        DateTime Data { get; set; }
        DateTime Hora { get; set; }

        OrderType Tipo { get; set; }
        Vendedor Vendedor { get; set; }
        DateTime DataValidade { get; set; }

        PhaseType Fase { get; set; }

        Double Valor { get; set; }
        #endregion

        #region Item
        string Referencia { get; set; }
        double Quantidade { get; set; }
        double ValorUnitario { get; }
        double ValorTotal { get; }
        #endregion

        #region Itens
        ObservableCollection<PedidoItem> Itens { get; }
        PedidoItem Item { get; set; }
        #endregion

        #region Commands
        ICommand AddItemCommand { get; }
        ICommand RemoveItemCommand { get; }
        ICommand UpdateOrderCommand { get; }
        #endregion
        
    }
}
