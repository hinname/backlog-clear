using Microsoft.AspNetCore.Mvc;

namespace BacklogClear.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController: ControllerBase
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult GetItem(int id)
    {
        var response = new Item()
        {
            Id = 1,
            Name = "DeadRising",
            Description = "Description of item 1",
            Status = "Backlog",
            Type = "Game"
        };
        
        if (id == response.Id)
            return Ok(response);
        return NotFound("Item not found");
    }

    [HttpPost]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    public IActionResult PostItem([FromBody] Item item)
    {
        // Register item in DB
        return Ok(item);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    public IActionResult PutItem(int id, [FromBody] Item item)
    {
        // Update item in DB
        return Ok(item);
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteItem(int id)
    {
        // Delete item in DB
        return Ok();
    }
}