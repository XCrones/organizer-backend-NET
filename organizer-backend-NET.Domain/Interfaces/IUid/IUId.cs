﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace organizer_backend_NET.Domain.Interfaces.IUId
{
    public interface IUId
    {
        [Column("UId")]
        public int UId { get; set; }
    }
}
