using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.Infrastructure;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.ObjectModel;
using SA.Repository.Domain;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using SA.BreadCrumb.View;
using SA.Stock.View.Group;
using SA.Stock.View.Unit;
using SA.Stock.View.Producer;
using SA.Stock.View.SubGroup;
using SA.Repository.Repositories;

namespace SA.Stock.View.Product
{
    public class ProductPresenter : IProductPresenter, IDataErrorInfo
    {
        #region Properties
        private Produto _produto;
        private ValidationResults _validationResults;
        private readonly IProductView _view;
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public ProductPresenter(IProductView view, Produto produto, IUnityContainer container)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (produto == null)
            {
                throw new ArgumentNullException("produto");
            }
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.SubmitCommand = new DelegateCommand<object>(this.Submit, this.CanSubmit);
            this.CancelCommand = new DelegateCommand<object>(this.Cancel);

            this.SearchUnitCommand = new DelegateCommand<object>(this.SearchUnit);
            this.SearchProducerCommand = new DelegateCommand<object>(this.SearchProducer);
            this.SearchGroupCommand = new DelegateCommand<object>(this.SearchGroup);
            this.SearchSubGroupCommand = new DelegateCommand<object>(this.SearchSubGroup, this.CanSearchSubGroup);

            this._view = view;
            this._view.SetPresenter(this);
            this._container = container;

            this.Produto = produto;
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }
        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IProductViewModel

        #region Commands
        /// <summary>
        /// Submit command - called when action is submited to be applied
        /// </summary>
        public ICommand SubmitCommand { get; private set; }
        /// <summary>
        /// Cancel command - called when action is canceld
        /// </summary>
        public ICommand CancelCommand { get; private set; }
        /// <summary>
        /// Search Unit command - called when searching a Unit is requeired
        /// </summary>
        public ICommand SearchUnitCommand { get; private set; }
        /// <summary>
        /// Search Producer command - called when searching a Producer is requeired
        /// </summary>
        public ICommand SearchProducerCommand { get; private set; }
        /// <summary>
        /// Search Group command - called when searching a Group is requeired
        /// </summary>
        public ICommand SearchGroupCommand { get; private set; }
        /// <summary>
        /// Search SubGroup command - called when searching a SubGroup is requeired
        /// </summary>
        public ICommand SearchSubGroupCommand { get; private set; }
        #endregion

        public Produto Produto
        {
            get 
            {
                return _produto;
            }

            private set
            {
                if ((_produto != null) && _produto.Equals(value))
                    return;

                _produto = value;

                _fabricanteID = (_produto.Fabricante != null ? _produto.Fabricante.Id : 0);
                _grupoID = (_produto.Grupo != null ? _produto.Grupo.Id : 0);
                _subGrupoID = (_produto.SubGrupo != null ? _produto.SubGrupo.Id : 0);
                _unidadeID = (_produto.Unidade != null ? _produto.Unidade.Id : 0);

                OnPropertyChanged("Produto");
            }
        }

        public string Referencia
        {
            get
            {
                if (Produto == null)
                    return String.Empty;

                return Produto.Referencia;
            }

            set
            {
                if (Produto == null)
                    return;

                if (Produto.Referencia != null && Produto.Referencia.Equals(value))
                    return;

                Produto.Referencia = value;
                OnPropertyChanged("Referencia");
            }
        }

        public string Nome
        {
            get
            {
                if (Produto == null)
                    return String.Empty;

                return Produto.Nome;
            }

            set
            {
                if (Produto == null)
                    return;

                if (Produto.Nome != null && Produto.Nome.Equals(value))
                    return;

                Produto.Nome = value;
                OnPropertyChanged("Nome");
            }
        }

        #region Unidade
        private int _unidadeID;
        public int UnidadeID
        {
            get
            {
                if (Produto.Unidade != null)
                    return Produto.Unidade.Id;
                return this._unidadeID;
            }

            set
            {
                if (this._unidadeID.Equals(value))
                    return;

                _unidadeID = value;

                if (value < 1)
                {
                    this.Unidade = null;
                    return;
                }

                try
                {
                    var repository = _container.Resolve<IUnitRepository>();
                    var reg = repository.GetById(value);
                    this.Unidade = reg;
                }
                catch (InvalidOperationException)
                {
                    this.Unidade = null;
                }
            }
        }
        public Unidade Unidade
        {
            get
            {
                return Produto.Unidade;
            }

            set
            {
                if (Produto.Unidade != null)
                    if (Produto.Unidade.Equals(value))
                        return;

                Produto.Unidade = value;
                if (value != null)
                    _unidadeID = value.Id;

                OnPropertyChanged("Unidade");
                OnPropertyChanged("UnidadeID");
                
            }
        }
        #endregion

        #region Fabricante
        private int _fabricanteID;
        public int FabricanteID
        {
            get
            {
                if (Produto.Fabricante != null)
                    return Produto.Fabricante.Id;
                return this._fabricanteID;
            }

            set
            {
                if (this._fabricanteID.Equals(value))
                    return;

                _fabricanteID = value;

                if (value < 1)
                {
                    this.Fabricante = null;
                    return;
                }

                try
                {
                    var repository = _container.Resolve<IProducerRepository>();
                    var reg = repository.GetById(value);
                    this.Fabricante = reg;
                }
                catch (InvalidOperationException)
                {
                    this.Fabricante = null;
                }
            }
        }
        public Fabricante Fabricante
        {
            get
            {
                return Produto.Fabricante;
            }

            set
            {
                if (Produto.Fabricante != null)
                    if (Produto.Fabricante.Equals(value))
                        return;

                Produto.Fabricante = value;
                if (value != null)
                  _fabricanteID = value.Id;

                OnPropertyChanged("Fabricante");
                OnPropertyChanged("FabricanteID");
            }
        }
        #endregion

        #region Grupo
        private int _grupoID;
        public int GrupoID
        {
            get
            {
                if (Produto.Grupo != null)
                    return Produto.Grupo.Id;
                return this._grupoID;
            }

            set
            {
                if (this._grupoID.Equals(value))
                    return;

                _grupoID = value;
                
                if (value < 1)
                {
                    Grupo = null;
                    return;
                }

                try
                {
                    var repository = _container.Resolve<IGroupRepository>();
                    var reg = repository.GetById(value);
                    this.Grupo = reg;
                }
                catch (InvalidOperationException)
                {
                    this.Grupo = null;
                }
            }
        }
        public Grupo Grupo
        {
            get
            {
                return Produto.Grupo;
            }

            set
            {
                if (Produto.Grupo != null)
                    if (Produto.Grupo.Equals(value))
                        return;

                SubGrupo = null;
                Produto.Grupo = value;
                
                if (value != null)
                    _grupoID = value.Id;

                OnPropertyChanged("Grupo");
                OnPropertyChanged("GrupoID");
                (this.SearchSubGroupCommand as DelegateCommand<object>).RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region SubGrupo
        private int _subGrupoID;
        public int SubGrupoID
        {
            get
            {
                if (Produto.SubGrupo != null)
                    return Produto.SubGrupo.Id;
                return this._subGrupoID;
            }

            set
            {
                if (this._subGrupoID.Equals(value))
                    return;

                _subGrupoID = value;

                if (value < 1)
                {
                    this.SubGrupo = null;
                    return;
                }

                try
                {
                    var repository = _container.Resolve<ISubGroupRepository>();
                    var reg = repository.GetById(value);
                    this.SubGrupo = reg;
                }
                catch (InvalidOperationException)
                {
                    this.SubGrupo = null;
                }
            }
        }
        public SubGrupo SubGrupo
        {
            get
            {
                return Produto.SubGrupo;
            }

            set
            {
                if (Produto.SubGrupo != null)
                    if (Produto.SubGrupo.Equals(value))
                        return;

                Produto.SubGrupo = value;
                if (value != null)
                  _subGrupoID = value.Id;

                OnPropertyChanged("SubGrupo");
                OnPropertyChanged("SubGrupoID");
            }
        }
        #endregion

        #endregion

        #region Methods
        private void SearchUnit(object param)
        {
            IUnitListPresenter presenter = this._container.Resolve<IUnitListPresenter>("IUnitListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.Unidade = presenter.Unidade;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Unidades", presenter);
        }

        private void SearchProducer(object param)
        {
            IProducerListPresenter presenter = this._container.Resolve<IProducerListPresenter>("IProducerListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.Fabricante = presenter.Fabricante;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Fabricantes", presenter);
        }

        private void SearchGroup(object param)
        {
            IGroupListPresenter presenter = this._container.Resolve<IGroupListPresenter>("IGroupListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.Grupo = presenter.Grupo;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("Grupos", presenter);
        }

        private bool CanSearchSubGroup(object param)
        {
            return (this.Grupo != null);
        }

        private void SearchSubGroup(object param)
        {
            if (Grupo == null)
                return;
            this._container.RegisterInstance<Grupo>("Grupo", Grupo);

            ISubGroupListPresenter presenter = this._container.Resolve<ISubGroupListPresenter>("ISubGroupListPresenter");
            presenter.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
            {
                if (eventArgs == null)
                    return;

                if ((eventArgs is CloseViewEventArgs) && ((eventArgs as CloseViewEventArgs).CloseViewOption == CloseViewType.Ok))
                {
                    this.SubGrupo = presenter.SubGrupo;
                }
            };

            IBreadCrumbPresenter breadCrumb = this._container.Resolve<IBreadCrumbPresenter>();
            if (breadCrumb != null)
                breadCrumb.AddCrumb("SubGrupos", presenter);
        }

        private bool CanSubmit(object parameter)
        {
            return (_validationResults == null ? true : _validationResults.IsValid);
        }
        private void Submit(object parameter)
        {
            CloseViewRequested(this, new CloseViewEventArgs(CloseViewType.Submit));
        }
        private void Cancel(object parameter)
        {
            CloseViewRequested(this, new CloseViewEventArgs(CloseViewType.Cancel));
        }
        private void Validar()
        {
            _validationResults = Validation.Validate(this.Produto);

            /// Producer Validation
            if (this.FabricanteID != 0)
            {
                if (this.Fabricante == null)
                    _validationResults.AddResult(new ValidationResult("Fabricante inválido!", this, "FabricanteID", null, null));
            }
            
            /// Group Validation
            if (this.GrupoID != 0)
            {
                if (this.Grupo == null)
                    _validationResults.AddResult(new ValidationResult("Grupo inválido!", this, "GrupoID", null, null));
            }

            /// SubGroup Validation
            if (this.SubGrupoID != 0)
            {
                if (this.Grupo == null)
                    _validationResults.AddResult(new ValidationResult("Informe um Grupo para selecionar o Sub-grupo!", this, "SubGrupoID", null, null));

                if ((this.Grupo != null) && (this.SubGrupo == null))
                    _validationResults.AddResult(new ValidationResult("Sub-grupo inválido para o Grupo informado!", this, "SubGrupoID", null, null));
            }

            /// Unit Validation
            if (this.UnidadeID != 0)
            {
                if (this.Unidade == null)
                    _validationResults.AddResult(new ValidationResult("Unidade inválida!", this, "UnidadeID", null, null));
            }
        }
        #endregion

        #region IDataErrorInfo
        public string Error
        {
            get
            {
                // Not implemented because we are not consuming it. 
                // Instead, we are displaying error messages at the item level. 
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (_validationResults == null)
                    return null;

                string resultado = Common.GetColumnValidationMessage(columnName, _validationResults);
                return resultado;
            }

            set
            {
                // Not implemented because we are using validation results
                throw new NotImplementedException();
            }
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.Validar();
            (this.SubmitCommand as DelegateCommand<object>).RaiseCanExecuteChanged();

            PropertyChangedEventHandler temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
