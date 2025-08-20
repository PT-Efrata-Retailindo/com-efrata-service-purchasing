using Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels.DOItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text;

namespace Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels
{
    public class DOItemsRackingViewModels : IValidatableObject
    {
        public string POSerialNumber { get; set; }
        public List<RackingViewModels> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int itemErrorCount = 0;
            string itemError = "[";
            foreach (var item in Items)
            {
                
                itemError += "{";
                if (string.IsNullOrWhiteSpace(item.Colour) || item.Colour == null)
                {
                    itemErrorCount++;
                    itemError += $"Rack: 'Colour tidak boleh kosong', ";
                    
                }
                itemError += "}, ";
            }
            itemError += "]";
            if (itemErrorCount > 0)
                yield return new ValidationResult(itemError, new List<string> { "Items" });

        }
    }
}
