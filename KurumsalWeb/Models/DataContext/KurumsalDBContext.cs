using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.DataContext
{
    public partial class KurumsalDBContext : DbContext

    {
        public KurumsalDBContext(): base("KurumsalDB")
        {
            
        }

        public KurumsalDBContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public KurumsalDBContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
        {
        }

        public KurumsalDBContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public KurumsalDBContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        public KurumsalDBContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected KurumsalDBContext(DbCompiledModel model) : base(model)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Hakkimizda> Hakkimizda { get; set; }
        public virtual DbSet<Hizmet> Hizmet { get; set; }
        public virtual DbSet<Iletisim> Iletisim { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kimlik> Kimlik { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<Yorum> Yorum { get; set; }
        
    }
}