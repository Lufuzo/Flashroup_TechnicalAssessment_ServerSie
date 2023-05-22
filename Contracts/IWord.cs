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
        Task<IEnumerable<WordEntitiess>> GetWords();
        Task<WordEntitiess> GetWord(int id);
        Task<WordEntitiess> Create(WordEntitiess word);
        Task<string> UpdateWord(int id, WordEntitiess word);
        Task<string>  DeleteWord(int id);
        
    }
}
