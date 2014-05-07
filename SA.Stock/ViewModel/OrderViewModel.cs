using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Repository.Domain;
using SA.Repository.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SA.Repository.Extensions;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Unity;
using SA.Repository.Repositories;

namespace SA.Stock.ViewModel
{
    public class OrderViewModel : IOrderViewModel
    {
        #region Properties
        private IUnityContainer _container;
        private Pedido _pedido;

        #endregion

        #region Constructors
        public OrderViewModel(IUnityContainer container, Pedido pedido)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            
            if (pedido == null)
                throw new ArgumentNullException("pedido");

            this._container = container;

            this._addItemCommand = new DelegateCommand<object>(AddItem);
            this._removeItemCommand = new DelegateCommand<PedidoItem>(RemoveItem);
            this._updateOrderCommand = new DelegateCommand<object>(UpdateOrder);

            this._pedido = pedido;
        }
        #endregion

        #region IOrderViewModel

        #region Commands
        private ICommand _addItemCommand;
        public ICommand AddItemCommand 
        {
            get
            {
                return this._addItemCommand;
            } 
        }

        private ICommand _removeItemCommand;
        public ICommand RemoveItemCommand 
        {
            get
            {
                return this._removeItemCommand;
            }
        }

        private ICommand _updateOrderCommand;
        public ICommand UpdateOrderCommand 
        {
            get
            {
                return this._updateOrderCommand;
            }
        }
        #endregion

        #region Order
        public Cadastro Cadastro 
        {
            get
            {
                return this._pedido.Cadastro;
            }
            set
            {
                if (_pedido.Cadastro != null)
                    if (_pedido.Cadastro.Equals(value))
                        return;

                _pedido.Cadastro = value;
                OnPropertyChanged("Cadastro");
            }
        }
        public DateTime Data 
        {
            get
            {
                return this._pedido.Data;
            }
            set
            {
                if (_pedido.Data != null)
                    if (_pedido.Data.Equals(value.Date))
                        return;

                _pedido.Data = value.Date;
                OnPropertyChanged("Data");
            }
        }
        public DateTime Hora
        {
            get
            {
                return this._pedido.Hora;
            }
            set
            {
                if (_pedido.Hora != null)
                    if (_pedido.Hora.Equals(value))
                        return;

                _pedido.Hora = value;
                OnPropertyChanged("Hora");
            }
        }

        public OrderType Tipo 
        {
            get
            {
                return this._pedido.Tipo;
            }
            set
            {
                if (_pedido.Tipo.Equals(value))
                    return;

                _pedido.Tipo = value;
                OnPropertyChanged("Tipo");
            }
        }
        public Vendedor Vendedor 
        {
            get
            {
                return this._pedido.Vendedor;
            }
            set
            {
                if (_pedido.Vendedor != null)
                    if (_pedido.Vendedor.Equals(value))
                        return;

                _pedido.Vendedor = value;
                OnPropertyChanged("Vendedor");
            }
        }
        public DateTime DataValidade 
        {
            get
            {
                return this._pedido.DataValidade;
            }
            set
            {
                if (_pedido.DataValidade != null)
                    if (_pedido.DataValidade.Equals(value.Date))
                        return;

                _pedido.DataValidade = value.Date;
                OnPropertyChanged("DataValidade");
            }
        }

        public PhaseType Fase 
        {
            get
            {
                return this._pedido.Fase;
            }
            set
            {
                if (_pedido.Fase.Equals(value))
                    return;

                _pedido.Fase = value;
                OnPropertyChanged("Fase");
            }
        }

        public Double Valor 
        {
            get
            {
                return this._pedido.Valor;
            }
            set
            {
                if (_pedido.Valor.Equals(value))
                    return;

                _pedido.Valor = value;
                OnPropertyChanged("Valor");
            }
        }
        #endregion

        #region Itens
        public ObservableCollection<PedidoItem> Itens 
        {
            get
            {
                return new ObservableCollection<PedidoItem>(this._pedido.Itens);
            } 
        }

        private PedidoItem _item;
        public PedidoItem Item
        {
            get
            {
                return _item;
            }
            set
            {
                if (_item != null)
                {
                    if (_item.Equals(value))
                        return;
                }
                _item = value;
                OnPropertyChanged("Item");
            }
        }
        #endregion

        #region Item
        private string _referencia = String.Empty;
        public string Referencia 
        {
            get
            {
                return _referencia;
            }

            set 
            {
                if (_referencia != null)
                    if (_referencia.Equals(value))
                        return;

                _referencia = value;
                OnPropertyChanged("Referencia");
            } 
        }

        private double _quantidade = 1.0;
        public double Quantidade 
        {
            get
            {
                return _quantidade;
            }
            set
            {
                if (_quantidade.Equals(value))
                    return;

                _quantidade = value;
                OnPropertyChanged("Quantidade");
            } 
        }

        public double ValorUnitario 
        {
            get
            {
                if (Item == null)
                    return 0;

                return Item.ValorUnitario;
            }
        }

        public double ValorTotal
        {
            get
            {
                if (Item == null)
                    return 0;

                return Item.Valor;
            }
        }
        #endregion

        #endregion

        #region Methods
        private void AddItem(object sender)
        {
            if ((this.Referencia == null) || (this.Referencia.Equals(String.Empty)))
                return;

            try
            {
                var repository = _container.Resolve<IProductRepository>();
                var produto = repository.GetByReference(this.Referencia);
                if (produto == null)
                    return;

                this.Item = new PedidoItem();
                this.Item.Produto = produto;
                this.Item.Quantidade = this.Quantidade;
                this.Item.Pedido = this._pedido;
                this.Item.ValorUnitario = 10;
                this.Item.Valor = this.Item.ValorUnitario * this.Item.Quantidade;
                this._pedido.Itens.Add(this.Item);
                this.UpdateOrder(sender);

                OnPropertyChanged("ValorUnitario");
                OnPropertyChanged("ValorTotal");
                OnPropertyChanged("Itens");
            }
            catch (InvalidOperationException)
            {
            }

            this.Quantidade = 1.0;
        }

        private void RemoveItem(PedidoItem item)
        {

        }

        private void UpdateOrder(object sender)
        {
            this._pedido.Atualizar();
            OnPropertyChanged("Valor");
        }

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
