using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryAPI.Interfaces;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IBaseRespository<People> _PeopleRepository;

        public PeopleController(IBaseRespository<People> PeopleRepository)
        {
            _PeopleRepository = PeopleRepository;
        }

        [HttpGet("getactives")]
        public async Task<IEnumerable<People>> GetAllActives()
        {
            return await _PeopleRepository.GetAll();
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<People>> GetAll()
        {
            return await _PeopleRepository.GetAll(true);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            People people = await _PeopleRepository.GetById(id);
            return people != null ? Ok(people) : NotFound();
        }

        [HttpPost]
        public IActionResult Add(People people)
        {
            return _PeopleRepository.Add(people) ? Ok() : BadRequest();
        }

        [HttpPut]
        public IActionResult Update(People people)
        {
            return _PeopleRepository.Update(people) ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            People people = await _PeopleRepository.GetById(id);

            if (people != null)
            {
                _PeopleRepository.Delete(people);
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
