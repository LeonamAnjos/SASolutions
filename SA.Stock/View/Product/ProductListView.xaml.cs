﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using SA.Infrastructure;

namespace SA.Stock.View.Product
{
    /// <summary>
    /// Interaction logic for ProductListView.xaml
    /// </summary>
    public partial class ProductListView : UserControl, IProductListView
    {
        public ProductListView()
        {
            InitializeComponent();
        }

        #region IProductListView
        public void SetPresenter(IProductListPresenter presenter) 
        {
            this.DataContext = presenter;
        
        }
        #endregion

        private void ListaProdutos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as IProductListPresenter).CloseCommand.Execute(CloseViewType.Ok);
        }
    }
}
