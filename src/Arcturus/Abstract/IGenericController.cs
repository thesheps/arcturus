using System.Web.Mvc;

namespace Arcturus.Abstract
{
    public interface IGenericController
    {
        void Delete(int pk);
        void Edit(int pk, string name, string value);
        ActionResult Index();
    }
}