using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;

namespace Business.DependencyResolver.Autofac
{
    //Dependency Injection yaparak -> yani interface imizin hangi classa
    //ait olduğını söyleyerekten yapıyı kurduk!
    //dependency injeciton yapılarımızı tutacak konteyner!
    //builder yapısıyla dependency injc sağladığımız için Load metodunu kullancağız;
    //metot virtual olduğ için override edilebilir
    public class AutofacBusinessModel: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();


            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();


            builder.RegisterType<IUserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();
        }
    }
}
