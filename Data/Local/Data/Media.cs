using Data.Local.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Local.Data
{
    public abstract class Media :LongEntity
    {
        [Required]
        [MaxLength(300)]
        public string FilePath { get; set; }
        [MaxLength(200)]
        public string FileName { get; set; }
    }
}
