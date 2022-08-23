using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers{

    [ApiController]
    public class HomeController : ControllerBase
   {
        [HttpGet]
        [Route("/")]
        public List<TodoModels> Get([FromServices] AppDbContext context)
        {

            return context.Todos.ToList();
        }

        [HttpGet("/{id:int}")]
        public TodoModels GetById(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {

            return context.Todos.FirstOrDefault(x=>x.Id == id);
        }

        [HttpPost()]
        [Route("/")]
        public TodoModels Post ([FromBody]TodoModels todo, [FromServices] AppDbContext context)
        {

            context.Todos.Add(todo);
            context.SaveChanges();
            
                return todo;
        }

        [HttpPut("/{id:int}")]
        public TodoModels Put ([FromBody]TodoModels todo,
         [FromRoute] int id,
         [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x=>x.Id == id);
            if(model == null)
                return todo;
            model.Title = todo.Title;
            model.Done = todo.Done;
            context.SaveChanges();
                return model;
        }
   }
}