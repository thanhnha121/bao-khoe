using System.Data.Entity;
using BaoKhoe.Migrations;
using BaoKhoe.Models;
using MySql.Data.Entity;

namespace BaoKhoe
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDBContext : DbContext
    {
        public AppDBContext() : base("MyContext")
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDBContext, Configuration>());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<AssignedArticle> AssignedArticles { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<RelatedArticle> RelatedArticles { get; set; }
        public DbSet<ArticleKeyword> ArticleKeywords { get; set; }
    }
}