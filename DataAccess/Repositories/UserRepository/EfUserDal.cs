using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Repositories.UserRepository
{
    public class EfUserDal : EfEntityRepositoryBase<User, SimpleContextDb>, IUserDal
    {

        public List<OperationClaim> GetUserOperationClaims(int UsertId)
        {
            using (var context = new SimpleContextDb())
            {

                var result = from UserOperationClaim in context.UserOperationClaims.Where(p => p.UserId == UsertId)
                             join OperationClaim in context.OperationClaims on UserOperationClaim.OperationClaimId equals
                             OperationClaim.Id
                             select new OperationClaim
                             {
                                 Id = OperationClaim.Id,
                                 Name = OperationClaim.Name,
                             };
                          return result.OrderBy(p=>p.Name).ToList();

            }

        }
    }
}