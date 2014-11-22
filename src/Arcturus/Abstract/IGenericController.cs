using System.Web.Mvc;

namespace Arcturus.Abstract
{
    public interface IGenericController<TEntity>
    {
        ActionResult Index();
        ActionResult Create();
        ActionResult Create(TEntity entity);
        void Delete(int pk);
        void Edit(int pk, string name, string value);
    }
}