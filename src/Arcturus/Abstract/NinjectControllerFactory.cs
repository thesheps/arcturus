using Arcturus.Concrete;
using Ninject;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;

namespace Arcturus.Abstract
{
    public abstract class NinjectControllerFactory<TContext> : DefaultControllerFactory
        where TContext : DbContext
    {
        public NinjectControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
            _kernel.Bind<IFieldMapper>().To<FieldMapper>();
            _kernel.Bind(typeof(IGenericController<>)).To(typeof(GenericController<,>));
            _kernel.Bind(typeof(IGenericRepository<>)).To(typeof(EntityFrameworkRepository<>));
            AddMappings(_kernel.Get<IFieldMapper>());
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }

        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            var controllerType = base.GetControllerType(requestContext, controllerName);
            if (controllerType != null)
            {
                return controllerType;
            }

            var modelType = Type.GetType(string.Format("{0}.{1}, {2}", ModelNamespace, controllerName, ModelAssembly), false, true);
            controllerType = typeof(GenericController<,>).MakeGenericType(modelType, typeof(TContext));
            
            return controllerType;
        }

        protected abstract void AddMappings(IFieldMapper mapper);
        protected abstract string ModelNamespace { get; }
        protected abstract string ModelAssembly { get; }
        private readonly IKernel _kernel;
    }
}