using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalEdge.Repository.Enitity
{
    public class Language
    {
        [Key]
        public long LanguageId { get; set; }

        public string LanguageName { get; set; }
    }
}
