public interface IEnquiryService
{
    Task<EnquiryDto> CreateAsync(CreateEnquiryDto dto);
    Task<IEnumerable<EnquiryDto>> GetAllAsync();
    Task<EnquiryDto?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
}