using Microsoft.EntityFrameworkCore;
using Mounret.API.Data;
using Mounret.API.DTOs;
using Mounret.API.Interfaces;
using Mounret.API.Models;

public class EnquiryService : IEnquiryService
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public EnquiryService(ApplicationDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    // ✅ CREATE
    public async Task<EnquiryDto> CreateAsync(CreateEnquiryDto dto)
    {
        var enquiry = new Enquiry
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Country = dto.Country,
            UserType = dto.UserType,
            ProductId = dto.ProductId
        };

        _context.Enquiries.Add(enquiry);
        await _context.SaveChangesAsync();

        // ✅ Email to user
        await _emailService.SendEmailAsync(
            dto.Email,
            "We received your request",
            $"<h2>Hi {dto.FirstName},</h2><p>We will contact you soon regarding your request.</p>"
        );

        // ✅ Email to admin
        await _emailService.SendEmailAsync(
            "admin@mounret.com",
            "New Enquiry",
            $"New enquiry from {dto.FirstName} {dto.LastName}"
        );

        // ✅ RETURN DTO (IMPORTANT FIX)
        return new EnquiryDto
        {
            Id = enquiry.Id,
            FirstName = enquiry.FirstName,
            LastName = enquiry.LastName,
            Email = enquiry.Email,
            Phone = enquiry.Phone,
            Country = enquiry.Country,
            UserType = enquiry.UserType,
            ProductId = enquiry.ProductId,
            CreatedAt = enquiry.CreatedAt
        };
    }

    // ✅ GET ALL (Admin)
    public async Task<IEnumerable<EnquiryDto>> GetAllAsync()
    {
        return await _context.Enquiries
            .OrderByDescending(e => e.CreatedAt)
            .Select(e => new EnquiryDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Country = e.Country,
                UserType = e.UserType,
                ProductId = e.ProductId,
                CreatedAt = e.CreatedAt
            })
            .ToListAsync();
    }

    // ✅ GET BY ID
    public async Task<EnquiryDto?> GetByIdAsync(int id)
    {
        return await _context.Enquiries
            .Where(e => e.Id == id)
            .Select(e => new EnquiryDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Country = e.Country,
                UserType = e.UserType,
                ProductId = e.ProductId,
                CreatedAt = e.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    // ✅ DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var enquiry = await _context.Enquiries.FindAsync(id);

        if (enquiry == null)
            return false;

        _context.Enquiries.Remove(enquiry);
        await _context.SaveChangesAsync();

        return true;
    }
}