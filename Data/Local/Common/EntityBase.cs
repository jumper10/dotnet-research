using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Local.Common
{
    public abstract class EntityBase<T>
    { 
        [Key]
        public T Id { get; set; }
        public DateTimeOffset LastUpdateTime { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
    public abstract class StringEntityBase : EntityBase<string>
    {
     
    }
    public abstract class LongEntityBase : EntityBase<long>
    {
        
    }
}
