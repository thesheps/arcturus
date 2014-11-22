using Arcturus.Concrete;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Arcturus.Abstract
{
    public abstract class NinjectControllerFactory : DefaultControllerFactory
    {
        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings(_kernel);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }

        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            var t = Type.GetType(string.Format("{0}.{1}, {2}", ContainingNamespace, controllerName, ContainingAssembly), false, true);
            var controllerType = typeof(GenericController<>).MakeGenericType(t);

            return controllerType;
        }

        protected abstract void AddBindings(IKernel kernel);
        protected abstract string ContainingNamespace { get; }
        protected abstract string ContainingAssembly { get; }
        private readonly IKernel _kernel;
    }
}
