using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.Model;
using System.ComponentModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using SA.Repository.Domain;
using System.Windows.Input;


namespace SA.Stock.View.Product
{
    public interface IProductPresenter : ICrumbViewContent, INotifyPropertyChanged
    {
        Produto Produto { get; }
        string Referencia { get; set; }
        string Nome { get; set; }
        Unidade Unidade { get; set; }
        Fabricante Fabricante { get; set; }
        Grupo Grupo { get; set; }
        SubGrupo SubGrupo { get; set; }

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
        /// Search Unit command - called when searching a Unit is requeired
        /// </summary>
        ICommand SearchUnitCommand { get; }
        /// <summary>
        /// Search Producer command - called when searching a Producer is requeired
        /// </summary>
        ICommand SearchProducerCommand { get; }
        /// <summary>
        /// Search Group command - called when searching a Group is requeired
        /// </summary>
        ICommand SearchGroupCommand { get; }
        /// <summary>
        /// Search SubGroup command - called when searching a SubGroup is requeired
        /// </summary>
        ICommand SearchSubGroupCommand { get; }
        #endregion
    }
}
