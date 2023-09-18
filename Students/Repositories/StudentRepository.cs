using Students.Models;
using Microsoft.EntityFrameworkCore;
using Students.Data;

namespace Students.Repositories
{
    public class StudentRepository : IStudentRepositoryInterface
    {
        private readonly StudentDbContext _context;


        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAll()
        {

            var students = await _context.Students.AsNoTracking().ToListAsync().ConfigureAwait(false);

            return students;
        }

        public async Task<Student> GetById(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                throw new DataNotFoundException($"Student with id {id} not found");
            }

            return student;
        }

        public async Task<Student> Create(Student std)
        {
            _context.Students.Add(std);
            await _context.SaveChangesAsync();
            return std;
        }

        public async Task<Student> Update(int id, Student std)
        {
            if (id != std.Id)
            {
                throw new ArgumentException($"Student with id {id} not found");
            }

            _context.Entry(std).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistingStudent(id))
                {
                    throw new DataNotFoundException($"Student with id {id} not found");
                }
                else
                {
                    throw;
                }
            }

            return std;
        }

        public async Task Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                throw new DataNotFoundException("Student with id {id} was not found.");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> ExistingStudent(int id)
        {
            return await _context.Students.AnyAsync(e => e.Id == id);
        }
    }
}


