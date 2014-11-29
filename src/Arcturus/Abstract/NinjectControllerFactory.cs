using Arcturus.Concrete;
using Arcturus.Domain;
using Ninject;
using System;
using System.Configuration;
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

            var modelType = Type.GetType(string.Format("{0}.{1}, {2}", _modelNamespace, controllerName, _modelAssembly), false, true);
            modelType = modelType ?? typeof(UnknownModel);
            controllerType = typeof(GenericController<,>).MakeGenericType(modelType, typeof(TContext));
            
            return controllerType;
        }

        protected virtual void AddMappings(IFieldMapper mapper)
        {
        }

        private readonly string _modelNamespace = ConfigurationManager.AppSettings["ModelNamespace"];
        private readonly string _modelAssembly = ConfigurationManager.AppSettings["ModelAssembly"];
        private readonly IKernel _kernel;
    }
}