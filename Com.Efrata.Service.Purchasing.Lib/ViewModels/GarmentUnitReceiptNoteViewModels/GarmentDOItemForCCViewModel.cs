using Com.Efrata.Service.Purchasing.Lib.Utilities;
using Com.Efrata.Service.Purchasing.Lib.ViewModels.NewIntegrationViewModel;

namespace Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels
{
    public class GarmentDOItemForCCViewModel : BaseViewModel
 
    {
        public long GarmentPRId { get; set; }
        public long GarmentPRItemId { get; set; }

        public string POSerialNumber { get; set; }
        public string DOItemNo { get; set; }
        public string RONo { get; set; }

        public string CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }

        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductRemark { get; set; }

        public long UOMId { get; set; }
        public string UOMUnit { get; set; }

        public decimal RemainingQuantity { get; set; }
        public double BudgetPrice { get; set; }

    }
}
