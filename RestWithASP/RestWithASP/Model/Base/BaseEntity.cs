﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASP.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
