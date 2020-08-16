using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace NerglishTranslator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslateController : ControllerBase
    {
        private readonly ILogger<TranslateController> _logger;

        public TranslateController(ILogger<TranslateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            string bodyStr;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEndAsync().Result;
            }
            string rez = "";
            foreach (string s in bodyStr.Split(' '))
            {
                int index = Static.en.IndexOf(s.ToLower());
                if (index == -1)
                    rez += s + " ";
                else
                    rez += Static.ner[index] + " ";
            }
            return rez;
        }
        [HttpPost]
        public string Post()
        {
            string bodyStr;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEndAsync().Result;
            }
            string rez = "";
            foreach (string s in bodyStr.Split(' '))
            {
                int index = Static.en.IndexOf(s);
                if (index == -1)
                    rez += s + " ";
                else
                    rez += Static.ner[index] + " ";
            }
            return rez;
        }
    }
}
