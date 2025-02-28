using BookSystem.DTO;
using BookSystem.Models;

namespace BookSystem.Services.Book {
    public interface IBookInterface {
        Task<ResponseModel<List<BookResponseDTO>>> GetBooks();
        Task<ResponseModel<BookResponseDTO>> GetBookById(Guid idBook);
        Task<ResponseModel<List<BookResponseDTO>>> GetBooksByAuthorId(Guid idAuthor);
        Task<ResponseModel<BookResponseDTO>> CreateBook(BookCreateDTO dto);
        Task<ResponseModel<BookResponseDTO>> UpdateBook(Guid id, BookUpdateDTO dto);
        Task<ResponseModel<BookResponseDTO>> DeleteBook(Guid id);
    }
}
