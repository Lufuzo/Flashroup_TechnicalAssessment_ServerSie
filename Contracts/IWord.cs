using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWord
    {
        Task<IEnumerable<Word>> GetWords();
        Task<Word> GetWord(int id);
        Task<Word> Create(Word word);
        Task<string> UpdateWord(int id, Word word);
        Task<string>  DeleteWord(int id);
        
    }
}
