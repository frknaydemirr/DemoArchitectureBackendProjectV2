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

        public static string OperationClaimNotExist = "The permission information you selected is not found in the permissions!";

        public static string UserNotExist = "The user whom  selected could not be found!";
    }
}
