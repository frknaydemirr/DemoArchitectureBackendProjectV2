using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserOperationClaimRepository.Constans
{
    public  class UserOperationClaimMessage
    {

        public static string Added = "Permission successfully created.";

        public static string Updated = "Permission successfully updated.";

        public static string Deleted = "Permission successfully deleted.";

        public static string OperationClaimSetExist   = "This user has been assigned this permission before!";
    }
}
