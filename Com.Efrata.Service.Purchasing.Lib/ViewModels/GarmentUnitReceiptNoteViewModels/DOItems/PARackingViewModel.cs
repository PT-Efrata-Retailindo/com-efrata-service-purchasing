using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels.DOItems
{
    public class PARackingViewModel
    {
        public long id { get; set; }
        public string POSerialNumber { get; set; }
        public string Rack { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public string Colour { get; set; }
        public string Supplier { get; set; }
        public long ProductId { get; set; }
        public string RoNo { get; set; }

        //In
        public string ReceiptDate { get; set; }
        public decimal QtyReceipt { get; set; }
        public string UomReceipt { get; set; }

        //Out
        public string ExpenditureDate { get; set; }
        public decimal QtyExpenditure { get; set; }
        public string UomExpenditure { get; set; }

        //Remaining
        public decimal QtyRemaining { get; set; }
        public string UomRemaining { get; set; }
        public string Description { get; set; }

    }
}
