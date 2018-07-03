
using lesson_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace lesson_webapi.Core
{
    public class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> options)
            : base(options)
        { }
        
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Card> Cards { get; set; }
        



    }
}