using DataAccess.Local.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Local.Data
{
    public class Media : LongEntityBase
    {
        public Media() { }
        [Required]
        [MaxLength(300)]
        public string FilePath { get; set; }
        [MaxLength(200)]
        public string FileName { get; set; }
    }
}
