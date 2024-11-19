using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryAPI.Interfaces;
using SimpleInventoryAPI.Models;
using SimpleInventoryAPI.Repositories;

namespace SimpleInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IBaseRespository<InventoryMove> _InventoryMoveRepository;

        public PurchaseController(PurchaseRepository inventoryMoveRepository)
        {
            _InventoryMoveRepository = inventoryMoveRepository;
        }
        #region get methods
        [HttpGet("getactives")]
        public async Task<IEnumerable<InventoryMove>> GetAllActives()
        {
            return await _InventoryMoveRepository.GetAll();
        }
        [HttpGet("getall")]
        public async Task<IEnumerable<InventoryMove>> GetAll()
        {
            return await _InventoryMoveRepository.GetAll(true);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryMove>> GetById(int id)
        {
            InventoryMove inventoryMove = await _InventoryMoveRepository.GetById(id);
            return inventoryMove != null ? Ok(inventoryMove) : NotFound();
        }
        #endregion

        [HttpPost]
        public IActionResult Add(InventoryMove inventoryMove)
        {
            inventoryMove.MoveType = Data.InventoryMoveType.Purchase;
            return _InventoryMoveRepository.Add(inventoryMove) ? Ok(inventoryMove) : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update(InventoryMove inventoryMove)
        {
            return _InventoryMoveRepository.Update(inventoryMove) ? Ok(inventoryMove) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            InventoryMove inventoryMove = await _InventoryMoveRepository.GetById(id);

            if (inventoryMove != null)
            {
                _InventoryMoveRepository.Delete(inventoryMove);
                return Ok();

            }
            else
            {
                return NotFound();
            }
        }

    }
}
