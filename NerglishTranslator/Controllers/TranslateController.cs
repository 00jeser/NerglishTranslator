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
using LemmaSharp;


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
            return "use post mrthod";
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
            ILemmatizer lmtz = new LemmatizerPrebuiltCompact(LemmaSharp.LanguagePrebuilt.English);
            foreach (string s in bodyStr.Split(new char[] { ' ', ',', '.', ')', '(' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var lema = lmtz.Lemmatize(s).ToLower();
                int index = Static.en.IndexOf(lema);
                if (index == -1)
                    rez += lema + " ";
                else
                {
                    if (s.Last() == 's' && lema.Last() != 's')
                        rez += Static.ner[index] + " ";
                    if (s.EndsWith("est") && !lema.EndsWith("est"))
                        rez += Static.ner[index] + " ";
                    rez += Static.ner[index] + " ";
                }
            }
            return rez;
        }
    }
}
