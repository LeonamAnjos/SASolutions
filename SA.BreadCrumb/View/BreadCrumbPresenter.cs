using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA.BreadCrumb.Model;
using System.Collections.ObjectModel;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Composite.Regions;
using SA.Infrastructure;
using Microsoft.Practices.Unity;

namespace SA.BreadCrumb.View
{
    public class BreadCrumbPresenter : IBreadCrumbPresenter
    {
        #region Properties
        private readonly IRegionManager _regionManager;
        private readonly IBreadCrumbView _view;

        private ObservableCollection<ICrumb> _crumbs;
        private DelegateCommand<ICrumb> _crumbAccessedCommand;

        #endregion

        #region Constructors
        public BreadCrumbPresenter(IBreadCrumbView view, IRegionManager regionManager)
        {
            this._view = view;
            this._view.SetPresenter(this);

            this._regionManager = regionManager;

            _crumbs = new ObservableCollection<ICrumb>();
            _crumbAccessedCommand = new DelegateCommand<ICrumb>(OnCrumbAccessed);
        }
        #endregion

        #region ICrumbViewContent
        public object View 
        {
            get { return _view; } 
        }

        public event EventHandler CloseViewRequested = delegate { };

        #endregion

        #region IBreadCrumbPresenter
        public ObservableCollection<ICrumb> Crumbs 
        {
            get { return _crumbs; }
            private set { _crumbs = value; }
        }
        public DelegateCommand<ICrumb> CrumbAccessedCommand 
        {
            get { return _crumbAccessedCommand; }
            private set { _crumbAccessedCommand = value; }
        }
        #endregion

        #region IBreadCrumb
        public int AddCrumb(ICrumb crumb)
        {
            if (crumb == null)
                throw new ArgumentNullException("Crumb");

            Crumbs.Add(crumb);
            AddViewToRegion(crumb.Content);
            return (Crumbs.Count);
        }

        public int AddCrumb(string title, ICrumbViewContent content)
        {
            if (title == null)
                throw new ArgumentNullException("Title");

            if (content == null)
                throw new ArgumentNullException("Content");

            ICrumb crumb = new Crumb(title, content);
            return AddCrumb(crumb);
        }

        public int RemoveCrumb()
        {
            if (Crumbs.Count < 1)
                return -1;

            Crumbs.RemoveAt(Crumbs.Count - 1);

            ICrumb crumb = Crumbs.ElementAt<ICrumb>(Crumbs.Count - 1);
            AddViewToRegion(crumb.Content);

            return Crumbs.Count;
        }

        public int SetCrumbDescription(string description) 
        {
            if (Crumbs.Count < 1)
                return -1;
            
            ICrumb crumb = Crumbs.ElementAt<ICrumb>(Crumbs.Count - 1);
            crumb.Description = description;
            return Crumbs.Count;
        
        }
        #endregion

        #region Methods
        private void OnCrumbAccessed(ICrumb crumb)
        {
            IRegion region = this._regionManager.Regions[RegionNames.MainRegion];

            var indexInicial = Crumbs.IndexOf(crumb);

            // Remove na ordem inversa
            for (int i = (Crumbs.Count - 1); i > indexInicial; i--)
            {
                ICrumb c = Crumbs.ElementAt<ICrumb>(i);
                region.Deactivate(c.Content.View);
                region.Remove(c.Content.View);
                Crumbs.RemoveAt(i);
            }

            region.Activate(crumb.Content.View);
        }

        private void AddViewToRegion(ICrumbViewContent content)
        {
            IRegion region = this._regionManager.Regions[RegionNames.MainRegion];

            foreach (var v in region.Views)
            {
                region.Deactivate(v);
            }

            if (!region.Views.Contains(content.View)) 
            {
                content.CloseViewRequested += delegate(object sender, EventArgs eventArgs)
                {
                    region.Deactivate(content.View);
                    region.Remove(content.View);
                    this.RemoveCrumb();
                };
                
                region.Add(content.View);
            }
                
            region.Activate(content.View);
        }


        #endregion

    }
}
