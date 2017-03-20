﻿using Red_software.Model;
using Red_software.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EntityLayer;
using BusinessLayer;
using Red_software.Views;

namespace Red_software.ViewModel
{
    public class ProductsViewModel : TableModel<ProductEntity>
    {
        protected override void DeleteItem(object parameter)
        {
            ManageProducts.DeleteProduct(SelectedItem);
            RefreshList(parameter);
        }

        protected override void EditItem(object parameter)
        {
            ProductEntity Item = new ProductEntity();
            EntityCloner.CloneProperties<ProductEntity>(Item, SelectedItem);
            EditProductViewModel EPVM = new EditProductViewModel(Item, false);
            EditItemView EIV = new EditItemView() { DataContext = EPVM };
            EIV.ShowDialog();
            if (EPVM.SaveEdit)
            {
                Item = EPVM.Item;
                ManageProducts.ModifyProduct(Item);
                RefreshList(parameter);
                foreach (var p in List)
                    if (Item.Id == p.Id)
                        SelectedItem = p;
            }
        }

        protected override void NewItem(object parameter)
        {
            ProductEntity Item = new ProductEntity();
            EditProductViewModel EPVM = new EditProductViewModel(Item, true);
            EditItemView EIV = new EditItemView() { DataContext = EPVM };
            EIV.ShowDialog();
            if (EPVM.SaveEdit)
            {
                ManageProducts.NewProduct(EPVM.Item);
                RefreshList(parameter);
                foreach (var p in List)
                    if (Item.Id == p.Id)
                        SelectedItem = p;
            }
        }

        protected override void RefreshList(object parameter)
        {
            this.List = ManageProducts.ListProducts();
        }
    }

}
