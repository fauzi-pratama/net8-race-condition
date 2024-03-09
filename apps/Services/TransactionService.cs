
using apps.Models.Context;
using apps.Models.Entities;
using apps.Models.Request;
using StackExchange.Redis;
using System.Reflection.Emit;

namespace apps.Services
{
    public interface ITransactionService
    {
        Task<(bool status, string message)> AddTransaction(List<AddTransactionRequest> request);
    }

    public class TransactionService(DataDbContext dbContext, IDatabase dbRedis) : ITransactionService
    {
        public async Task<(bool status, string message)> AddTransaction(List<AddTransactionRequest> request)
        {
            int tryRemaining = 3;
            var transactionRedis = dbRedis.CreateTransaction();

        checkpointTry:
            decimal totalBalance = 0;
            var transaction = dbContext.Database.BeginTransaction();
            List<PromoTransactionDetail> promoTransactionDetails = new();

            foreach (var loopData in request)
            {
                PromoTransactionDetail dataDetail = new()
                {
                    Code = loopData.Code,
                    Qty = loopData.Qty,
                    Balance = loopData.Balance
                };

                promoTransactionDetails.Add(dataDetail);
                int qtyRemaining = Convert.ToInt32(await dbRedis.StringGetAsync(loopData.Code));

                if (qtyRemaining < loopData.Qty)
                {
                    transaction.Rollback();
                    return (false, "Qty not enough");
                }

                transactionRedis.AddCondition(Condition.StringEqual(loopData.Code, dbRedis.StringGet(loopData.Code)));
                transactionRedis.StringDecrementAsync(loopData.Code, loopData.Qty);

                totalBalance += loopData.Balance;
            }

            PromoTransaction promoTransaction = new()
            {
                TotalBalance = totalBalance,
                PromoTransactionDetail = promoTransactionDetails
            };

            dbContext.PromoTransaction.Add(promoTransaction);

            await dbContext.SaveChangesAsync();

            var cekRedis = transactionRedis.Execute();

            if (!cekRedis)
            {
                transaction.Rollback();

                if (tryRemaining < 1)
                    return (false, "Failed to update Redis");

                tryRemaining--;
                goto checkpointTry;
            }

            transaction.Commit();

            return (true, "Success");
        }
    }
}
