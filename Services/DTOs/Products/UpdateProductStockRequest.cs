﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitectureV2.Services.DTOs.Products
{
    public record UpdateProductStockRequest(int ProductId,int Quantity);
    
}
