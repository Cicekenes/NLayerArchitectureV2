using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerArchitectureV2.Repositories.CoreRepository;
using NLayerArchitectureV2.Services.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitectureV2.Services.Filters
{
    public class NotFoundFilter<T, TId>(IGenericRepository<T, TId> _genericRepository) : Attribute, IAsyncActionFilter where T : class where TId : struct
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Action metod çalışmadan önce
            var idValue = context.ActionArguments.TryGetValue("id", out var idAsObject) ? idAsObject : null // parametrelerden ilkini al yani id'yi

            if (idAsObject is not TId id)
            {
                await next();
                return;
            }

            if (await _genericRepository.AnyAsync(id))
            {
                await next();
                return;
            }
            else
            {
                var entityName = typeof(T).Name;

                //action metod ismi
                var actionName = context.ActionDescriptor.RouteValues["action"];

                var result = ServiceResult.Fail($"Veri bulunamamıştır.({entityName}).({actionName})");

                context.Result = new NotFoundObjectResult(result);
            }

            await next();

            //action metod çalıştıktan sonra.
        }
    }
}
