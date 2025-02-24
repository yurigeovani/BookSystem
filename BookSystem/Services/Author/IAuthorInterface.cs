using BookSystem.DTO;
using BookSystem.Models;

namespace BookSystem.Services.Author {
    public interface IAuthorInterface {
        Task<ResponseModel<List<AuthorModel>>> GetAuthors();
        Task<ResponseModel<AuthorModel>> GetAuthorById(Guid idAuthor);
        Task<ResponseModel<AuthorModel>> GetAuthorByBookId(Guid idBook);
        Task<ResponseModel<AuthorModel>> CreateAuthor(AuthorCreateDTO dto);
        Task<ResponseModel<AuthorModel>> UpdateAuthor(Guid id, AuthorUpdateDTO dto);
        Task<ResponseModel<AuthorModel>> DeleteAuthor(Guid id);
    }
}
