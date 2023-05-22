using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public  class WordContext : DbContext
    {
        public WordContext(DbContextOptions<WordContext> options) : base(options)
        {

        }
        public DbSet<Word> Words { get; set; }


    }
}
