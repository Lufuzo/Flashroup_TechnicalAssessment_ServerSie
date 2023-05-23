using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace SQL_SanitizeWords_WebApi.Controllers
{
    public class LoadFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public void PreLoadFile(string filepath)
        {
            filepath = "C:\\Users\\Lungelo Mbalane\\Documents\\Visual Studio 2022\\SQL_Words_Application\\SQL_SanitizeWords_WebApi\\SQL_SanitizeWords_WebApi\\sql_sensitive_list.txt";

            using (StreamReader file = new StreamReader(filepath))
            {
                int counter = 0;
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    string connetionString;
                    SqlConnection cnn;
                    connetionString = @"Data Source=DESKTOP-1CBBH7T\\SQLEXPRESS;Initial Catalog=SQL_Words_Application;User ID="";Password=""";


                    using (var con = new SqlConnection(connetionString))
                    {
                        foreach (var line in ln)
                        {
                            using (var cmd = con.CreateCommand())
                            {
                                cmd.CommandText = "INSERT INTO [Words] ([Value]) VALUES (:val)";

                                var param = cmd.CreateParameter();
                                param.ParameterName = ":val";
                                param.Value = line;

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    counter++;
                }
                file.Close();

            }




        }
    }
}
