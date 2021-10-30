
using AutoMapper;
using Library.Helper;
using Library.Models.RequsetDTO;
using Library.Models.ResponseDTO;
using Library.Repository.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly ErrorClass errorClass;
        private readonly IMapper mapper;
        private readonly IAuthorRepository authorRepository;

        public AuthorController(ErrorClass errorClass, IMapper mapper, IAuthorRepository authorRepository)
        {
            this.errorClass = errorClass;
            this.mapper = mapper;
            this.authorRepository = authorRepository;
        }
        [HttpGet(Name = "GetAll")]
        public ActionResult GetAll(PagingDTO paging)
        {
            var Result = authorRepository.GetAll(paging, Url);
            return Ok(Result);
        }
        [HttpGet("{id}")]
        public ActionResult GetAuthorById([FromQuery] int id)
        {
            string ErrorCode = " ";
            var result = authorRepository.GetAuthorById(id, out ErrorCode);
            if (!string.IsNullOrWhiteSpace(ErrorCode))
            {
                errorClass.loadError(ErrorCode);
                ModelState.AddModelError(errorClass.ErrorProp, errorClass.ErrorMassege);
                return ValidationProblem();
            }
            //Mapping Domain Model To Response Model

            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddAuthor([FromBody] AuthorAddRequsetDTO Newauthors)
        {
            string ErrorCode = " ";
            var result = authorRepository.AddAuthor(Newauthors, out ErrorCode);
            if (!string.IsNullOrWhiteSpace(ErrorCode))
            {
                errorClass.loadError(ErrorCode);
                ModelState.AddModelError(errorClass.ErrorProp, errorClass.ErrorMassege);
                return ValidationProblem();
            }
            return CreatedAtAction(nameof(GetAuthorById), new { id = result.AuthorID }, mapper.Map<AuthorResponseDTO>(result));
        }
        [HttpPut("{id}")]
        public ActionResult UpdateAuthor([FromQuery] int id, [FromBody] AuthorUpdateRequsetDTO authorUpdateRequsetDTO)
        {
            string ErrorCode = " ";
            authorRepository.UpdateAuthor(id, authorUpdateRequsetDTO, out ErrorCode);
            if (!string.IsNullOrWhiteSpace(ErrorCode))
            {
                errorClass.loadError(ErrorCode);
                ModelState.AddModelError(errorClass.ErrorProp, errorClass.ErrorMassege);
                return ValidationProblem();
            }
            return NoContent();

        }
        [HttpDelete("{id}")]
        public ActionResult DeletedAuthor([FromQuery] int id)
        {
            string ErrorCode = " ";
            authorRepository.DeletedAuthor(id, out ErrorCode);
            if (!string.IsNullOrWhiteSpace(ErrorCode))
            {
                errorClass.loadError(ErrorCode);
                ModelState.AddModelError(errorClass.ErrorProp, errorClass.ErrorMassege);
                return ValidationProblem();
            }
            return NoContent();
        }

    }
}

