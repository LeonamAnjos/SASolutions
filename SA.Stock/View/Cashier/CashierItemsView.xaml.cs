using System;
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
using SA.Stock.ViewModel;

namespace SA.Stock.View.Cashier
{
    /// <summary>
    /// Interaction logic for CashierItemsView.xaml
    /// </summary>
    public partial class CashierItemsView : UserControl, ICashierItemsView
    {
        public CashierItemsView()
        {
            InitializeComponent();
        }

        private ICashierItemsPresenter _presenter;
        public void SetPresenter(ICashierItemsPresenter presenter)
        {
            this._presenter = presenter;
            this.DataContext = this._presenter.OrderViewModel;
        }

        private void TextBoxProduct_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Insert)
            {
                this.TextBoxQuantity.Focus();
            }
            else if (e.Key == Key.Enter)
            {
                if (((IOrderViewModel)this.DataContext).AddItemCommand.CanExecute(sender))
                    ((IOrderViewModel)this.DataContext).AddItemCommand.Execute(sender);
                this.TextBoxProduct.Clear();
            }
        }

        private void TextBoxQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.TextBoxProduct.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxProduct.Focus();
            this.TextBoxProduct.Clear();
        }

        private void TextBoxProduct_GotFocus(object sender, RoutedEventArgs e)
        {
            this.TextBoxProduct.SelectAll();
        }

        private void TextBoxQuantity_GotFocus(object sender, RoutedEventArgs e)
        {
            this.TextBoxQuantity.SelectAll();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }
        }
    }
}
