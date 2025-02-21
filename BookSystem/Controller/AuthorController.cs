using BookSystem.DTO;
using BookSystem.Models;
using BookSystem.Services.Author;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSystem.Controller {
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase {
        public readonly IAuthorInterface _authorInterface;
        public AuthorController(IAuthorInterface authorInterface) {
            _authorInterface = authorInterface;
        }

        [HttpGet("GetAuthors")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> GetAuthors() {
            var authors = await _authorInterface.GetAuthors();
            return Ok(authors);
        }
        
        [HttpGet("GetAuthorById/{idAuthor}")]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> GetAuthorById(Guid idAuthor) {
            var author = await _authorInterface.GetAuthorById(idAuthor);
            return Ok(author);
        }

        [HttpGet("GetAuthorByBookId/{idBook}")]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> GetAuthorByBookId(Guid idBook) {
            var author = await _authorInterface.GetAuthorByBookId(idBook);
            return Ok(author);
        }

        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> CreateAuthor(AuthorCreateDTO dto) {
            var author = await _authorInterface.CreateAuthor(dto);
            return Ok(author);
        }

        [HttpPut("UpdateAuthor/{id}")]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> UpdateAuthor(Guid id, AuthorUpdateDTO_ dto) {
            var author = await _authorInterface.UpdateAuthor(id, dto);
            return Ok(author);
        }
    }
}
