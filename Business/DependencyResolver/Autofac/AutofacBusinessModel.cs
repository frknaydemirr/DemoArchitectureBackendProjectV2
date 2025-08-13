using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Authentication;
using Business.Repository.EmailParameterRepository;
using Business.Repository.OperationClaimRepository;
using Business.Repository.UserOperationClaimRepository;
using Business.Repository.UserRepository;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Repositories.EmailParameterRepository;
using DataAccess.Repositories.OperationClaimRepository;
using DataAccess.Repositories.UserOperationClaimRepository;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;

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


            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<EmailParameterManager>().As<IEmailParameterService>();
            builder.RegisterType<EfEmailParameterDal>().As<IEmailParameterDal>();


            builder.RegisterType<AuthManager >().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new

             Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
          
        }
    }
    }

