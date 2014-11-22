using Arcturus.Abstract;
using System.Web.Mvc;

namespace Arcturus.Concrete
{
    public class GenericController<TEntity, TContext> : Controller, IGenericController
    {
        public GenericController(IGenericRepository<TEntity, TContext> repository, IFieldMapper fieldMapper)
        {
            _repository = repository;
            _fieldMapper = fieldMapper;
        }

        public ActionResult Index()
        {
            var items = _repository.GetAll();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(entity);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        [HttpPost]
        public void Edit(int pk, string name, string value)
        {
            var item = _repository.Get(pk);
            _fieldMapper.SetField(item, name, value);
            _repository.Update(item);
        }

        [HttpPost]
        public void Delete(int pk)
        {
            var item = _repository.Get(pk);
            _repository.Delete(item);
        }

        private readonly IGenericRepository<TEntity, TContext> _repository;
        private readonly IFieldMapper _fieldMapper;
    }
}