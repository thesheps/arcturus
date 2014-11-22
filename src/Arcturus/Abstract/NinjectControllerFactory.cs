using Arcturus.Concrete;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Arcturus.Abstract
{
    public abstract class NinjectControllerFactory<TEntity, TContext> : DefaultControllerFactory
    {
        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddDefaultBindings(_kernel);
            AddCustomBindings(_kernel);
        }

        private void AddDefaultBindings(IKernel kernel)
        {
            kernel.Bind<IFieldMapper>().To<FieldMapper>();
            kernel.Bind(typeof(IGenericController<>)).To(typeof(GenericController<,>));
            kernel.Bind(typeof(IGenericRepository<,>)).To(typeof(EntityFrameworkRepository<,>));
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

        protected abstract void AddCustomBindings(IKernel _kernel);
        protected abstract string ModelNamespace { get; }
        protected abstract string ModelAssembly { get; }
        private readonly IKernel _kernel;
    }
}
