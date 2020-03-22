using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Laboratorio4.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Laboratorio4.Controllers
{
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        Archivo archivo = new Archivo();

        public FileUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class FileUploadAPI
        {
            public IFormFile files { get; set; }
        }

        public async Task<string> Post(FileUploadAPI objFile)
        {

            string[] lines = { "hola" };
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "Upload");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "Upload" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();



                        
                        return "\\Upload\\" + objFile.files.FileName;

                    }



                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}
