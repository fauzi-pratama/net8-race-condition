
using AutoMapper;
using StackExchange.Redis;
using apps.Models.Context;
using apps.Models.Entities;
using apps.Models.Request;

namespace apps.Services
{
    public interface IMasterService
    {
        Task<(bool status, string message)> AddMaster(List<AddMasterRequest> request);
    }

    public class MasterService(DataDbContext dbContext, IDatabase dbRedis, IMapper mapper) : IMasterService
    {
        public async Task<(bool status, string message)> AddMaster(List<AddMasterRequest> request)
        {
            List<PromoMaster> promoMaster = mapper.Map<List<PromoMaster>>(request);

            using var transaction = dbContext.Database.BeginTransaction();
            
            dbContext.PromoMaster.AddRange(promoMaster);

            await dbContext.SaveChangesAsync();
            
            var transactionRedis = dbRedis.CreateTransaction();

            Parallel.ForEach(promoMaster, loopData =>
            {
                dbRedis.StringSet(loopData.Code, loopData.Qty);
            });

            await transactionRedis.ExecuteAsync();
            transaction.Commit();

            return (true, "Success");
        }
    }
}
