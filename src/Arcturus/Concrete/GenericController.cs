using Arcturus.Abstract;
using Arcturus.Attributes;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Arcturus.Concrete
{
    [RequiresLicense]
    public class GenericController<TEntity, TViewModel> : Controller, IGenericController<TViewModel>
    {
        public GenericController(IGenericRepository<TEntity> repository, IFieldMapper fieldMapper)
        {
            _repository = repository;
            _fieldMapper = fieldMapper;
        }

        public ActionResult Index()
        {
            var items = _repository.GetAll();
            var result = _fieldMapper.Map<IList<TViewModel>>(items);
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TViewModel viewModel)
        {
            var entity = _fieldMapper.Map<TEntity>(viewModel);
            if (ModelState.IsValid)
            {
                _repository.Insert(entity);
                return RedirectToAction("Index");
            }
            return View(viewModel);
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

        private readonly IGenericRepository<TEntity> _repository;
        private readonly IFieldMapper _fieldMapper;
    }
}