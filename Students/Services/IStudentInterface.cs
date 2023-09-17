using Students.Models;

namespace Students.Services
{
	public interface IStudentInterface
	{
        Task<List<Student>> GetAll();
        Task<Student> GetById(int id);
        Task<Student> Create(Student std);
        Task<Student> Update(int id, Student std);
        Task Delete(int id);
    }
}

