using BookSystem.DTO;
using BookSystem.Models;
using BookSystem.Services.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSystem.Controller {
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase {
        public readonly IBookInterface _bookInterface;

        public BookController(IBookInterface bookInterface) {
            _bookInterface = bookInterface;
        }

        [HttpGet("GetBooks")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> GetBooks() {
            var books = await _bookInterface.GetBooks();
            return Ok(books);
        }

        [HttpGet("GetBookById/{idBook}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> GetBookById(Guid idBook) {
            var book = await _bookInterface.GetBookById(idBook);
            return Ok(book);
        }

        [HttpGet("GetBooksByAuthorId/{idAuthor}")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> GetBooksByAuthorId(Guid idAuthor) {
            var books = await _bookInterface.GetBooksByAuthorId(idAuthor);
            return Ok(books);
        }

        [HttpPost("CreateBook")]
        public async Task<ActionResult<ResponseModel<BookModel>>> CreateBook(BookCreateDTO dto) {
            var book = await _bookInterface.CreateBook(dto);
            return Ok(book);
        }

        [HttpPut("UpdateBook/{id}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> UpdateBook(Guid id, BookUpdateDTO dto) {
            var book = await _bookInterface.UpdateBook(id, dto);
            return Ok(book);
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> DeleteBook(Guid id) {
            var book = await _bookInterface.DeleteBook(id);
            return Ok(book);
        }
    }
}
