using CarRentalSystem.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarRentalSystem.Filters
{
    public class TransactionLogAttribute:ActionFilterAttribute
    {
        //private readonly TransactionLogService _transactionLogService;

        //public TransactionLogAttribute(TransactionLogService transactionLogService)
        //{
        //    _transactionLogService = transactionLogService;
        //}

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var transactionService = context.HttpContext.RequestServices.GetService<TransactionLogService>();

            if (transactionService is not null)
            {
                transactionService.LogTransaction(context);
            }

            //await _transactionLogService.LogTransaction(context);
        }
    }
}
