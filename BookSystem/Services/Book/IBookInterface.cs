using BookSystem.DTO;
using BookSystem.Models;

namespace BookSystem.Services.Book {
    public interface IBookInterface {
        Task<ResponseModel<List<BookModel>>> GetBooks();
        Task<ResponseModel<BookModel>> GetBookById(Guid idBook);
        Task<ResponseModel<List<BookModel>>> GetBooksByAuthorId(Guid idAuthor);
        Task<ResponseModel<BookModel>> CreateBook(BookCreateDTO dto);
        Task<ResponseModel<BookModel>> UpdateBook(Guid id, BookUpdateDTO dto);
        Task<ResponseModel<BookModel>> DeleteBook(Guid id);
    }
}
