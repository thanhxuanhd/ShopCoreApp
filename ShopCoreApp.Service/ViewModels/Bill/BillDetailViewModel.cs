using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Service.ViewModels.Bill
{
    public class BillDetailViewModel
    {
        public int BillId { set; get; }

        public int ProductId { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        public virtual BillViewModel Bill { set; get; }

        public ProductViewModel Product { set; get; }

        public virtual ColorViewModel Color { set; get; }

        public virtual SizeViewModel Size { set; get; }
    }
}
