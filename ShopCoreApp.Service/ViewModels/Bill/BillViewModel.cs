using ShopCoreApp.Data.Enums;
using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Service.ViewModels.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopCoreApp.Service.ViewModels.Bill
{
    public class BillViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string CustomerName { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { set; get; }

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { set; get; }

        public PaymentMethod PaymentMethod { set; get; }

        public BillStatus BillStatus { set; get; }

        [DefaultValue(Status.Active)]
        public Status Status { set; get; } = Status.Active;

        public Guid? CustomerId { set; get; }

        public  AppUserViewModel User { set; get; }

        public List<BillDetailViewModel> BillDetails { set; get; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
