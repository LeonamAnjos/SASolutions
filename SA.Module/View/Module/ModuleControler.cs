using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SA.Infrastructure;
using SA.General.View.MenuXml;
using SA.BreadCrumb.View;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Modularity;
using SA.BreadCrumb.Model;
using System.Windows.Data;

namespace SA.Module.View.Module
{
    public class ModuleControler : IMenuXmlControler
    {
        #region Properties
        private readonly IBreadCrumbPresenter _breadCrumb;
        private readonly IUnityContainer      _container;
        private readonly IModuleManager       _moduleManager;
        #endregion

        #region Constructor
        public ModuleControler(IBreadCrumbPresenter breadCrumb, IUnityContainer container, IModuleManager moduleManager)
        {
            this._breadCrumb    = breadCrumb;
            this._container     = container;
            this._moduleManager = moduleManager;

            //ExecuteCommand = new DelegateCommand<XmlElement>(OnExecute, CanExecute);
            ExecuteCommand = new DelegateCommand<XmlElement>(OnExecute, CanExecute);
        }
        #endregion

        #region IModelControler
        public DelegateCommand<XmlElement> ExecuteCommand { get; private set; }
        #endregion

        #region Methods
        private void OnExecute(XmlElement viewInfo) 
        {
            if (viewInfo == null)
                throw new ArgumentNullException("viewInfo");

            var atributes = viewInfo.Attributes;

            var module = atributes.GetNamedItem("Module");
            var presenter = atributes.GetNamedItem("Presenter");
            var title = atributes.GetNamedItem("Title");

            if(module == null)
                throw new ArgumentException("Atributo 'module' não encontrado!");

            if (presenter == null)
                throw new ArgumentException("Atributo 'presenter' não encontrado!");

            if (title == null)
                throw new ArgumentException("Atributo 'title' não encontrado!");

            _moduleManager.LoadModule(module.Value);
            ContainerRegistration reg = _container.Registrations.Single<ContainerRegistration>(g => g.Name == presenter.Value);
            if (reg == null)
                throw new ArgumentException(String.Format("Formulário identificado por '{0}' não encontrado!", presenter.Value));

            _breadCrumb.AddCrumb(title.Value, (_container.Resolve(reg.RegisteredType, presenter.Value) as ICrumbViewContent));
        }

        private bool CanExecute(XmlElement module)
        {
            return true;
        }
        #endregion
    }
}
