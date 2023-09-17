using Students.Models;

namespace Students.Repositories
{
	public interface IStudentRepositoryInterface
	{
        Task<Student> Create(Student student);
        Task<Student> GetById(int id);
        Task<Student> Update(int id, Student student);
        Task<List<Student>> GetAll();
        Task Delete(int id);
    }
}

