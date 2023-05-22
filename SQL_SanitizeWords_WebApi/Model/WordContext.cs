using Microsoft.EntityFrameworkCore;

namespace SQL_SanitizeWords_WebApi.Model
{
    public class WordContext : DbContext
    {
        public WordContext(DbContextOptions<WordContext> options) : base(options)
        { 
        
        }
        public DbSet<Word> Words { get; set; } 
    }
}
