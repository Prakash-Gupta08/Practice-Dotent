using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practice_WebAPICRUD.Data;
using Practice_WebAPICRUD.Interfaces;
using Practice_WebAPICRUD.Models;
using Practice_WebAPICRUD.PaginationResultRequest;
using Practice_WebAPICRUD.StudentDBContext;

namespace Practice_WebAPICRUD.Services
{
    public class StudentService : IStudentService
    {
        private readonly db_context _context;
        public StudentService(db_context sqlDBContext)
        {
            _context = sqlDBContext;
        }

        public async Task<Student> CreateStudent(Student std)
        {
            try
            {
                await _context.Student.AddAsync(std);
                await _context.SaveChangesAsync();
                return std;
            }
            catch(Exception err)
            {
                throw new Exception("Error while creating student", err);
            }
        }

        

        public async Task<PageResult<Student>> GetStudentByPage(int pageNumber, int pageSize)
        {
            try
            {

                var totalRecord = await _context.Student.CountAsync();
                var res = await _context.Student.OrderBy(s => s.student_id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PageResult<Student>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRecord = totalRecord,
                    TotalPage = (int)Math.Ceiling((double)totalRecord / pageSize),
                    Data = res
                };
            }
            catch(Exception err)
            {
                throw new Exception("Error while searching students", err);
                
            }
              
        }


        public async Task<PageResult<Student>> SearchStudent(RequestModel req)
        {
            try
            {
                var data = _context.Student.AsQueryable();
                if (!string.IsNullOrEmpty(req.student_name))
                {
                    data = data.Where(s => s.student_name.Contains(req.student_name));
                }

                int totalCount = await data.CountAsync();

                if (!string.IsNullOrEmpty(req.SortBy))
                {
                    //if (req.SortBy.ToLower() == "name")
                    //{
                    //    data = req.SortOrder == "desc"
                    //        ? data.OrderByDescending(s => s.student_name)
                    //        : data.OrderBy(s => s.student_name);
                    //}
                    //else if (req.SortBy.ToLower() == "price")
                    //{
                    //    data = req.SortOrder == "desc"
                    //        ? data.OrderByDescending(s => s.student_marks)
                    //        : data.OrderBy(s => s.student_marks);
                    //}

                    string sortBy = req.SortBy.ToLower();
                    bool isDesc = req.SortOrder == "desc";


                    if (sortBy == "name")
                    {
                        if (isDesc)
                            data = data.OrderByDescending(s => s.student_name);
                        else
                            data = data.OrderBy(s => s.student_name);
                    }
                    else if (sortBy == "marks")
                    {
                        if (isDesc)
                            data = data.OrderByDescending(s => s.student_name);
                        else
                            data = data.OrderBy(s => s.student_name);
                    }


                }
                var item = await data.Skip((req.PageNumber - 1) * req.PageSize)
                        .Take(req.PageSize)
                        .ToListAsync();
                return new PageResult<Student>
                {
                    TotalPage = totalCount,
                    Data = item
                };
            }

            catch(Exception err)
            {
                throw new Exception("Error to find the student", err);
            }
        }


        public async Task<Student> UpdateStudent(StudentUpdate res)
        {
            try
            {
                var data = await _context.Student.FindAsync(res.student_id);
                if (data == null)
                    return null;

                data.student_name = res.student_name;
                data.student_marks = res.student_marks;
                data.student_grade = res.student_grade;

                await _context.SaveChangesAsync();
                return data;
            }

            catch(Exception err)
            {
                throw new Exception("Can't update the student", err);
            }

        }

        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                var data = await _context.Student.FindAsync(id);
                if (data == null)
                {
                    return false;
                }
                _context.Student.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception err)
            {
                throw new Exception("Student not found", err);

            }

        }

        public async Task<Student> GetStudentById(int id)
        {
            try
            {
                var data = await _context.Student.FindAsync(id);
                if (data == null)
                    return null;
                return data;
            }
            catch (Exception err)
            {
                throw new Exception("Incorrect StudentID :", err);
            }
        }
    }
}
