using AutoMapper;
using CVAPI.Interfaces;
using CVAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CV_API.Controllers
{

    [Route("API/[controller]")]
    [ApiController]
    public class CVController : Controller
    {
        private readonly ICVInterface _CVRepository;
        private readonly IMapper _mapper;

        public CVController(ICVInterface CVRepository, IMapper mapper)
        {
            _CVRepository = CVRepository;
            _mapper = mapper;
        }

        [HttpGet("{StudentId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetCV(string StudentId)
        {
            if (!_CVRepository.studentExists(StudentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var CV = _CVRepository.GetCV(StudentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(CV);
        }
    }
}