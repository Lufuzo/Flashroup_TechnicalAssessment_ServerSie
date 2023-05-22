using Contracts;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQL_SanitizeWords_WebApi.Model;
using Word = SQL_SanitizeWords_WebApi.Model.Word;

namespace SQL_SanitizeWords_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
     //   private readonly WordContext _dbContext;

        public readonly IWord _iword;

        public WordsController(IWord iword)
        {
            _iword = iword;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Word>>> GetWords()
        {
      
            return Ok(await _iword.GetWords());
        }
        [HttpGet("{id}")]
        public  async Task<ActionResult<Word>> GetWord(int id)
        {
            Entities.Word viewModel = new Entities.Word();
            var record = await _iword.GetWord(id);
            if (record == null)
            {
                return NotFound("Record not Found");
            }
            //viewModel.Id = record.Id;
            //viewModel.Value = record.Value;


            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<Entities.Word>> Create(Word word)
        {
            if (ModelState.IsValid)
            {
                Entities.Word  viewModel = new Entities.Word();
                viewModel.Id = word.Id;
                 viewModel.Value = word.Value;

                var newEntry = await _iword.Create(viewModel);
                return CreatedAtAction(nameof(GetWords), new { id = newEntry.Id }, newEntry);

            }
            return  BadRequest("Invalid record insert Fail!");
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWord(int id, Word word)
        {

            if (id != word.Id)
            {
                return BadRequest();
            }
            Entities.Word viewModel = new Entities.Word();
            viewModel.Id = word.Id;
            viewModel.Value = word.Value;
            var rec = await _iword.UpdateWord(id, viewModel);
    
            return Ok();
        }

       

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWord(int id)
        {
            if (_iword.DeleteWord == null)
            {
                return NotFound();
            }

            var word = await _iword.DeleteWord(id);
            if (word == null)
            {
                return NotFound();

            }    
            return Ok();
        }
    }
}
