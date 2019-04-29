using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Local.Common
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
