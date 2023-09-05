﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 Karakter Olmalıdır.")]
        public String Eposta { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 Karakter Olmalıdır.")]
        public String Sifre { get; set; }

        public String Yetki { get; set; }
    }
}