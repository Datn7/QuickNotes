using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickNotes.Data;
using QuickNotes.DTOs;
using QuickNotes.Models;
using System.Security.Claims;

namespace QuickNotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public NotesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetUserNotes()
        {
            int userId = GetUserId();

            var notes = await _context.Notes
                .Where(n => n.UserId == userId)
                .ToListAsync();

            var noteDtos = _mapper.Map<List<NoteDto>>(notes);
            return Ok(noteDtos);
        }

        // POST: api/notes
        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNote(NoteDto request)
        {
            int userId = GetUserId();

            var note = _mapper.Map<Note>(request);
            note.UserId = userId;
            note.CreatedAt = DateTime.UtcNow;

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<NoteDto>(note));
        }

        // PUT: api/notes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, NoteDto request)
        {
            int userId = GetUserId();

            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            _mapper.Map(request, note); // Only maps Title & Content
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<NoteDto>(note));
        }

        // DELETE: api/notes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            int userId = GetUserId();

            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
    }
}
