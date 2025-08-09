using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Authentication;
using Business.Repository.OperationClaimRepository;
using Business.Repository.UserOperationClaimRepository;
using Business.Repository.UserRepository;
using Core.Utilities.Interceptors;
using DataAccess.Repositories.OperationClaimRepository;
using DataAccess.Repositories.UserOperationClaimRepository;
using DataAccess.Repositories.UserRepository;

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

            builder.RegisterType<AuthManager >().As<IAuthService>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new

             Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
          
        }
    }
    }

