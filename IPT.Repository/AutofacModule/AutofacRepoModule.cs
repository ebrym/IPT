using Autofac;
using IPT.Data;
using IPT.Repository.Interface;

namespace IPT.Repository.AutofacModule
{
    public class AutofacRepoModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IPTContext>().InstancePerLifetimeScope();


            // register dependency convention
            builder.RegisterAssemblyTypes(typeof(IDependencyRegister).Assembly)
                .AssignableTo<IDependencyRegister>()
                .As<IDependencyRegister>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            base.Load(builder);
           
        }
    }    
}
