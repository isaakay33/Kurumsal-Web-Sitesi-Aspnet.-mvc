namespace KurumsalWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Eposta = c.String(nullable: false, maxLength: 50),
                        Sifre = c.String(nullable: false, maxLength: 50),
                        Yetki = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Baslik = c.String(),
                        Icerik = c.String(),
                        ResimURL = c.String(),
                        KategoriId = c.Int(),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.Kategori", t => t.KategoriId)
                .Index(t => t.KategoriId);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        KategoriID = c.Int(nullable: false, identity: true),
                        Kategori_ad = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(),
                    })
                .PrimaryKey(t => t.KategoriID);
            
            CreateTable(
                "dbo.Yorum",
                c => new
                    {
                        YorumId = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(nullable: false, maxLength: 50),
                        Eposta = c.String(),
                        Icerik = c.String(),
                        Onay = c.Boolean(nullable: false),
                        Blog_BlogId = c.Int(),
                        BlogId_BlogId = c.Int(),
                        Blog_BlogId1 = c.Int(),
                    })
                .PrimaryKey(t => t.YorumId)
                .ForeignKey("dbo.Blog", t => t.Blog_BlogId)
                .ForeignKey("dbo.Blog", t => t.BlogId_BlogId)
                .ForeignKey("dbo.Blog", t => t.Blog_BlogId1)
                .Index(t => t.Blog_BlogId)
                .Index(t => t.BlogId_BlogId)
                .Index(t => t.Blog_BlogId1);
            
            CreateTable(
                "dbo.Hakkimizda",
                c => new
                    {
                        HakkimizdaId = c.Int(nullable: false, identity: true),
                        Aciklama = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HakkimizdaId);
            
            CreateTable(
                "dbo.Hizmet",
                c => new
                    {
                        HizmetId = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 150),
                        Aciklama = c.String(),
                        ResimUrl = c.String(),
                    })
                .PrimaryKey(t => t.HizmetId);
            
            CreateTable(
                "dbo.Iletisim",
                c => new
                    {
                        IletisimId = c.Int(nullable: false, identity: true),
                        Adres = c.String(maxLength: 250),
                        Telefon = c.String(maxLength: 250),
                        Fax = c.String(),
                        Whatsaap = c.String(),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Instegram = c.String(),
                    })
                .PrimaryKey(t => t.IletisimId);
            
            CreateTable(
                "dbo.Kimlik",
                c => new
                    {
                        KimlikId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Keywords = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 300),
                        LogoURL = c.String(),
                        Unvan = c.String(),
                    })
                .PrimaryKey(t => t.KimlikId);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        SliderId = c.Int(nullable: false, identity: true),
                        Baslik = c.String(maxLength: 30),
                        Aciklama = c.String(maxLength: 150),
                        ResimURL = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.SliderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorum", "Blog_BlogId1", "dbo.Blog");
            DropForeignKey("dbo.Yorum", "BlogId_BlogId", "dbo.Blog");
            DropForeignKey("dbo.Yorum", "Blog_BlogId", "dbo.Blog");
            DropForeignKey("dbo.Blog", "KategoriId", "dbo.Kategori");
            DropIndex("dbo.Yorum", new[] { "Blog_BlogId1" });
            DropIndex("dbo.Yorum", new[] { "BlogId_BlogId" });
            DropIndex("dbo.Yorum", new[] { "Blog_BlogId" });
            DropIndex("dbo.Blog", new[] { "KategoriId" });
            DropTable("dbo.Slider");
            DropTable("dbo.Kimlik");
            DropTable("dbo.Iletisim");
            DropTable("dbo.Hizmet");
            DropTable("dbo.Hakkimizda");
            DropTable("dbo.Yorum");
            DropTable("dbo.Kategori");
            DropTable("dbo.Blog");
            DropTable("dbo.Admin");
        }
    }
}
