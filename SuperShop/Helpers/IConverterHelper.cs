using SuperShop.Data.Entities;
using SuperShop.Models;

namespace SuperShop.Helpers
{
    public interface IConverterHelper
    {
        //converter do viewModel para Product
        Product ToProduct(ProductViewModel model, string path, bool isNew);

        ProductViewModel ToProductViewModel(Product product);
    }
}
