using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        public int BlogId { get; set; }
        public String Baslik { get; set; }
        public String Icerik { get; set; }
        public String ResimURL { get; set; }
        public int? KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public ICollection<Yorum> Yorums { get; set; }

        public static implicit operator Blog(int v)
        {
            throw new NotImplementedException();
        }
    }
}