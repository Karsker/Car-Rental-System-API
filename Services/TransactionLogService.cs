using System.Security.Claims;
using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarRentalSystem.Services
{
    public class TransactionLogService
    {
        private readonly AppDbContext _dbContext;

        public TransactionLogService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogTransaction(ActionExecutingContext context)
        {
            var userIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            
            var userId = userIdentity?.FindFirst("userId")?.Value ?? "0";
            var userName = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
            var endpoint = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;

            var newTransaction = new TransactionLog()
            {
                UserId = int.Parse(userId),
                UserName = userName,
                Endpoint = endpoint,
                Method = method,
                Time = DateTime.Now,
            };

            _dbContext.TransactionLogs.Add(newTransaction);
            _dbContext.SaveChanges();
            return;
        }
    }
}
