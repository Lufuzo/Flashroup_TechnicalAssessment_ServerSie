using Contracts;
using Domain;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class WordRepository : IWord
    {

        private readonly WordContext _dbContext;

        public WordRepository(WordContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<WordEntitiess>> GetWords()
        {
            return await _dbContext.Words.ToListAsync();
        }

        public async Task<WordEntitiess> Create(WordEntitiess word)
        {
            _dbContext.Words.Add(word);
            await _dbContext.SaveChangesAsync();
            return word;
        }

        public async Task<WordEntitiess> GetWord(int id)
        {
            var record = await _dbContext.Words.FindAsync(id);
          

            return record;
        }

        public async Task<string> UpdateWord(int id, WordEntitiess word)
        {

            if (id != word.Id)
            {
                string mess = "Id are not the same";
                return mess;
            }
            _dbContext.Entry(word).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordAvailable(id))
                {
                    string mess = "Word not available";
                    return mess;

                }
                else
                {
                    throw;
                }
            }
            string message = "Successfully Updated!";
            return message;

        }

        private bool WordAvailable(int id)
        {
            return (_dbContext.Words?.Any(x => x.Id == id)).GetValueOrDefault();
        }
        public async Task<string> DeleteWord(int id)
        {
            if (_dbContext.Words == null)
            {
                string message = "Not Found";
               return message;
            }

              var wordToDelete = await _dbContext.Words.FindAsync(id);
            if (wordToDelete == null)
            {
                 string message = "Not Found1";
                return message;

            }
            _dbContext.Words.Remove(wordToDelete);

             await _dbContext.SaveChangesAsync();

            string mess = "Successfully Deleted!";

            return mess;

        
        }
    } 
}
