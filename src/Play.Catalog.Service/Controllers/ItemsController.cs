using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using System;
using System.Linq;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 6, DateTimeOffset.Now),
            new ItemDto(Guid.NewGuid(), "Moniter", "Restores a small amount of HP", 6, DateTimeOffset.Now),
            new ItemDto(Guid.NewGuid(), "Keyboard", "Restores a small amount of HP", 6, DateTimeOffset.Now)
        };

        //Retrive All Items
        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }


        //GET /items/12453
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = items.Where(item => item.id == id).SingleOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.Now);
            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.id }, item);
        }


        [HttpPut("{id}")]
        //IActionResult is for "hey I did my job(updated) and I no need a return"
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.Where(item => item.id == id).SingleOrDefault();

            if (existingItem == null)
            {
                return NotFound();
            }

            var updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = items.FindIndex(existingItem => existingItem.id == id);

            //Update
            items[index] = updatedItem;
            return NoContent();
        }


        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(item => item.id == id);

            if (index < 0)
            {
                return NotFound();
            }

            items.RemoveAt(index);
            return NoContent();
        }



    }
}