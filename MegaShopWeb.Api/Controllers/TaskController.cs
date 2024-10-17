using Microsoft.AspNetCore.Mvc;
using ShopDomainLayer.Models;

namespace MegaShopWeb.Api.Controllers
{
    [Route("Api/[Controller]")]
    [Controller]
    public class TaskController : Controller
    {
        List<NewProduct> Lproduct = new List<NewProduct>
        {
        new NewProduct { Id = 1,Name="Test1",Price=4000},
        new NewProduct { Id = 2,Name="Test1",Price=4000},
        new NewProduct { Id = 3,Name="Test1",Price=4000}
        };
        
        [HttpGet]
        public ActionResult<IEnumerable<NewProduct>> Get()
        {
            return Ok(Lproduct);
        }
        [HttpGet ,Route("GetById")]
        public ActionResult<NewProduct> GetById(int id)
        {
            var details = Lproduct.FirstOrDefault(x=>x.Id==id);
            if (details == null )
            {
                return NotFound();
            }
            return Ok(details);
        }

        [HttpPost]
        public ActionResult<NewProduct> Create([FromBody] NewProduct product)
        {
            product.Id = Lproduct.Count + 1;
            Lproduct.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        [HttpPut]
        public ActionResult<NewProduct> Update(int id, NewProduct dto)
        {
            var oldRec = Lproduct.FirstOrDefault(x=>x.Id==id);
            if (oldRec==null)
            {
                return NotFound();
            }
            oldRec.Name= dto.Name;
            oldRec.Price=dto.Price;
            return Ok(oldRec);
        }
        [HttpDelete]
        public  ActionResult<NewProduct> Delete(int id)
        {
            var del = Lproduct.FirstOrDefault(x=>x.Id==id);
            if (del==null)
            {
                return NotFound();
            }
            Lproduct.Remove(del);
            return NoContent();
        }
    }
}
