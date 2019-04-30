using Data.Local.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Local.Data
{
    public enum LogType
    {
        Information,
        Warning,
        Erro
    }
    public class AppLog:LongEntityBase
    {
        public bool IsRead { get; set; }
        [Required]
        [MaxLength(50)]
        public string User { get; set; }

        [Required]
        public LogType Type { get; set; }

        [Required]
        [MaxLength(50)]
        public string Source { get; set; }

        [Required]
        [MaxLength(50)]
        public string Action { get; set; }

        [Required]
        [MaxLength(400)]
        public string Message { get; set; }

        [MaxLength(4000)]
        public string Description { get; set; }
    }
}
