using Microsoft.AspNetCore.Mvc.RazorPages;
using Practice_WebAPICRUD.Data;
using Practice_WebAPICRUD.Models;
using Practice_WebAPICRUD.PaginationResultRequest;

namespace Practice_WebAPICRUD.Interfaces
{
    public interface IStudentService
    {
        Task<Student> GetStudentById(int id);
        Task<Student> CreateStudent(Student std);
       
        Task<PageResult<Student>> GetStudentByPage(int pageNumber , int pageSize);
        Task<PageResult<Student>> SearchStudent(RequestModel req);
        Task<Student> UpdateStudent(StudentUpdate res);
        Task<bool> DeleteStudent(int id);
    }
}
