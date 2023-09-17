using Students.ExceptionHandling;
using Students.Models;
using Students.Repositories;

namespace Students.Services
{
    public class StudentService : IStudentInterface
	{
        private readonly IStudentRepositoryInterface _studentRepository;

        public StudentService(IStudentRepositoryInterface studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAll()
        {

            var students = await _studentRepository.GetAll();
            if (students == null || !students.Any()) //Controlleren op Null als lege lijst.
            {
                throw new DataNotFoundException("Student not found");
            }
            return students;

        }


        public async Task<Student> GetById(int id)
        {
            var student = await _studentRepository.GetById(id);
            if (student == null)
            {
                throw new DataNotFoundException("Student not Found");
            }
            return student;
        }


        public async Task<Student> Create(Student students)
        {
            try
            {
                var createdStudent = await _studentRepository.Create(students);
                if (createdStudent == null)
                {
                    throw new DataCreationException("Student not found");
                }
                return createdStudent;
            }
            catch(Exception ex)
            {
                throw new DataCreationException("Error creating student.", ex);
            }
        }

       

        public async Task<Student> Update(int id, Student students)
        {
            try
            {
                var updatedStudent = await _studentRepository.Update(id, students);
                if (updatedStudent == null)
                {
                    throw new DataUpdateException("Student not found");
                }
                return updatedStudent;
            }catch (Exception ex)
            {
                throw new DataUpdateException("Error updating student.", ex);
            }
        }


        
        public async Task Delete(int id)
        {
            var existingStudent =  await _studentRepository.GetById(id);
            if (existingStudent == null)
            {
                throw new DataDeleteException("Student not found.");
            }
            await _studentRepository.Delete(id);
        }
    }
}

