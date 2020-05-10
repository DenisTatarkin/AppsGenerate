using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq;
using WebApp.Data;
using WebApp.Models.Entities;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity> : ControllerBase
        where TEntity : class, IHaveId, new()
    {
        protected DbSet<TEntity> _db;
        
        protected ApplicationDbContext _appDbContext;
        
        public BaseController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> Get()
        {
            return _db.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<TEntity> Get(long id)
        {
            return _db.FirstOrDefault(x => x.Id == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromForm]TEntity entity)
        {
            _db.Add(entity);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromForm] TEntity entity)
        {
            _db.Update(entity);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _db.Remove(new TEntity{Id = id});
        }
        
        [Route("filter")]
        public ActionResult<IEnumerable<TEntity>> Filter([FromForm]string query)
        {
            Object data = new TEntity();
            return _db.Where(x => x.GetType().GetFields().Any(f => f.GetValue(data).ToString() == query)).ToList();
        } 
    }
}