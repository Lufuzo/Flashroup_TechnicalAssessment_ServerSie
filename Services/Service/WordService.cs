using Entities;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class WordService : IWordService
    {
        public Task<WordEntitiess> Create(WordEntitiess word)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteWord(int id)
        {
            throw new NotImplementedException();
        }

        public Task<WordEntitiess> GetWord(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WordEntitiess>> GetWords()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateWord(int id, WordEntitiess word)
        {
            throw new NotImplementedException();
        }
    }
}
