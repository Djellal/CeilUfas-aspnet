using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace CeilUfas.models;
public class Session
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Code { get; set; }=string.Empty;
        [MaxLength(300)]
        public string Name { get; set; }=string.Empty;
        [MaxLength(300)]
        public string NameAr { get; set; }=string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }