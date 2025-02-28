using BookSystem.DTO;
using BookSystem.Models;

namespace BookSystem.Services.Author {
    public interface IAuthorInterface {
        Task<ResponseModel<List<AuthorResponseDTO>>> GetAuthors();
        Task<ResponseModel<AuthorResponseDTO>> GetAuthorById(Guid idAuthor);
        Task<ResponseModel<AuthorResponseDTO>> GetAuthorByBookId(Guid idBook);
        Task<ResponseModel<AuthorResponseDTO>> CreateAuthor(AuthorCreateDTO dto);
        Task<ResponseModel<AuthorResponseDTO>> UpdateAuthor(Guid id, AuthorUpdateDTO dto);
        Task<ResponseModel<AuthorResponseDTO>> DeleteAuthor(Guid id);
    }
}
