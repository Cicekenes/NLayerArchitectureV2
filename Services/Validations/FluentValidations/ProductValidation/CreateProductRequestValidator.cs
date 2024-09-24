using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NLayerArchitectureV2.Repositories.CoreRepository.Abstract.ProductRepositories;
using NLayerArchitectureV2.Services.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitectureV2.Services.Validations.FluentValidations.ProductValidation
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {

        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün ismi gereklidir.")
                .Length(3, 10).WithMessage("Ürün ismi 3 ile 10 karakter arasında olmalıdır.");



            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır");

            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Stok adedi 1 ile 100 arasında olmalıdır.");

        }
        /*MustAsync(MustUniqueProductNameAsync).WithMessage("Böyle bir ürün adı bulunmaktadır.");*/

            //.Must(MustUniqueProductName).WithMessage("Böyle bir ürün adı bulunmaktadır."); Birinci Yöntem
        //private async Task<bool> MustUniqueProductNameAsync(string name, CancellationToken cancellationToken)
        //{
        //    return  !await _productRepository.Where(x => x.Name == name).AnyAsync(cancellationToken);
        //}

        /// <summary>
        /// Birinci yöntem
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //private bool MustUniqueProductName(string name)
        //{
        //    return !_productRepository.Where(x => x.Name == name).Any();
        //}
    }
}
