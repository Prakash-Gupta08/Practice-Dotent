using Microsoft.EntityFrameworkCore;
using Practice_WebAPICRUD.Data;

namespace Practice_WebAPICRUD.StudentDBContext
{
    public class db_context : DbContext
    {
        private readonly IConfiguration _configuration;
        internal object student;
        
        public db_context (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Student> Student { get; set; }
       

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var data_string = _configuration.GetConnectionString("MySqlConn");

            optionsBuilder.UseSqlServer(data_string);
        }

    }
    
    
}
