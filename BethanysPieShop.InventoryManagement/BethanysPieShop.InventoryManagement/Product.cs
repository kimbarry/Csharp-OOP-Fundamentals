﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement
{
    public class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;

       

        public int Id
        {
            get { return id; }
            set 
            { 
                id = value; 
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    description = string.Empty; 
                }
                else
                {
                    description = value.Length > 50 ? value[..50] : value;
                }
            }
        }

        public UnitType UnitType { get; set; }
        public int AmountInStock { get; private set; }
        public bool IsBelowStockThreshold { get; private set; }



        public void UseProduct(int items)
        {
            if (items <= AmountInStock)
            {
                //use the items
                AmountInStock -= items;

                UpdateLowStock();

                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }
        public void IncreaseStock()
        {
            AmountInStock++;
        }

        private void DecreaseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                //decrease the stock with the specified number items
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock();

            Log(reason);
        }
        public string DisplayDetailsShort()
        {
            return $"{id}. {name} \n{AmountInStock} items in stock";
        }

        public string DisplayDetailsFull()
        {
            StringBuilder sb = new();
            //ToDo: add price here too
            sb.Append($"{id} {name} \n{description}\n{AmountInStock} item(s) in stock");

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();

        }

        private void UpdateLowStock()
        {
            if (AmountInStock < 10)//for now a fixed value
            {
                IsBelowStockThreshold = true;
            }
        }

        private void Log(string message)
        {
            //this could be written to a file
            Console.WriteLine(message);
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product {id} ({name})";
        }


    }
}
