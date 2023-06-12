using Contracts;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SQL_SanitizeWords_WebApi.Model;
using System.IO;

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
        // [HttpGet("{id:int}")]
        [HttpGet]
        [Route("{id:int}")]
        public  async Task<ActionResult<Word>> GetWord(int id)
        {
            Entities.WordEntitiess viewModel = new Entities.WordEntitiess();
            var record = await _iword.GetWord(id);
            if (record == null)
            {
                return NotFound("Record not Found");
            }
            //viewModel.Id = record.Id;
            //viewModel.Value = record.Value;


            return Ok(record);
        }
        [HttpPost]
        public async Task<ActionResult<Word>> Create(Word word)
        {
            if (ModelState.IsValid)
            {
                Word w = new Word();

                var checkWord = GetWordFromFIle();

                w.Value = word.Value;
                var result = checkWord.Where(x => x.FileValues.Any(y => y.Contains(w.Value))).ToList();
                if (result.Count == 0)
                {
                    Entities.WordEntitiess viewModel = new Entities.WordEntitiess();
                    viewModel.Id = word.Id;
                    viewModel.Value = word.Value;

                    var newEntry = await _iword.Create(viewModel);
                    return CreatedAtAction(nameof(GetWords), new { id = newEntry.Id }, newEntry);

                }
                else
                {

                   return BadRequest("Dangerous input Sensitive not inserted in the database !");

                }


               

            }
            return  BadRequest("Invalid record insert Fail!");
            
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<ActionResult> UpdateWord(int id, Word word)
        {

            if (id != word.Id)
            {
                return BadRequest();
            }
            Entities.WordEntitiess viewModel = new Entities.WordEntitiess();
           
            var checkWord = GetWordFromFIle();
        
            var result = checkWord.Where(x => x.FileValues.Any(y => y.Contains(word.Value))).ToList();
            if (result.Count == 0)
            {
                viewModel.Id = word.Id;
                viewModel.Value = word.Value;
                var rec = await _iword.UpdateWord(id, viewModel);
            }
            else
            {

                return BadRequest("Dangerous input Sensitive not inserted in the database !");

            }

            return Ok();
        }



        [HttpDelete("{id:int}")]
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

      
       // create list 

        public class FileValue
        {
            public List<string> FileValues { get; set; }

            public FileValue()
            {
                FileValues = new List<string>();
            }
        }
        // reading a file
        private static List<FileValue> GetWordFromFIle()
        {
            string path = "C:\\Users\\Lungelo Mbalane\\Documents\\Visual Studio 2022\\SQL_Words_Application\\SQL_SanitizeWords_WebApi\\SQL_SanitizeWords_WebApi\\sql_sensitive_list.txt";

            var fileValues = new List<FileValue>();
            var fileValue = new FileValue();
           
            List<String[]> arrayList = new List<String[]>();
            //string [] words = File.ReadAllLines(path).ToString();
            using (StreamReader file = new StreamReader(path))
            {
                //int counter = 0;
                string line = String.Empty;

                while (!String.IsNullOrEmpty(line = file.ReadLine()))
                {
                    fileValue.FileValues.Add(line);
                    fileValues.Add(fileValue);

                   
                }

    
                file.Close();


            }


            return fileValues;
      }

    }
}
