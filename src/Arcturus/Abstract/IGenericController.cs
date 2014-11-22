using System.Web.Mvc;

namespace Arcturus.Abstract
{
    public interface IGenericController<TEntity, TViewModel>
    {
        ActionResult Index();
        ActionResult Create();
        ActionResult Create(TViewModel entity);
        void Delete(int pk);
        void Edit(int pk, string name, string value);
    }
}