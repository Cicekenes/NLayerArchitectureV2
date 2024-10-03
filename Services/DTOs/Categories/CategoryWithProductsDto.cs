using NLayerArchitectureV2.Services.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitectureV2.Services.DTOs.Categories
{
    public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products);

}
