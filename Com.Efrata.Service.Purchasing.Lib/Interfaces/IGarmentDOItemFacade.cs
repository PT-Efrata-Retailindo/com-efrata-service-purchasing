using Com.Efrata.Service.Purchasing.Lib.Models.GarmentUnitReceiptNoteModel;
using Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels;
using Com.Efrata.Service.Purchasing.Lib.ViewModels.GarmentUnitReceiptNoteViewModels.DOItems;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Com.Efrata.Service.Purchasing.Lib.Interfaces
{
    public interface IGarmentDOItemFacade
    {
        List<object> ReadForUnitDO(string Keyword = null, string Filter = "{}");
        List<object> ReadForUnitDOMore(string Keyword = null, string Filter = "{}", int size = 100);
        List<DOItemsViewModels> GetByPO(string productcode, string po, string unitcode);
        GarmentDOItems ReadById(int id);
        Task<int> Update(int id, DOItemsRackingViewModels viewModels);
        Task<List<StellingEndViewModels>> GetStellingQuery(int id, int offset);
        MemoryStream GenerateExcel(string productcode, string po, string unitcode);
        MemoryStream GeneratePdf(List<StellingEndViewModels> stellingEndViewModels);
        MemoryStream GenerateBarcode(List<StellingEndViewModels> stellingEndViewModels);
        //Task<List<PARackingViewModel>> GetPARackingQuery(string Keyword = null);
        //Task<List<PARackingViewModel>> GetPARackingNonFabricQuery(string Keyword = null);
    }
}
