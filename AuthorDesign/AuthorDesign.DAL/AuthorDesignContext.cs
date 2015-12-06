using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.DAL {
    public class AuthorDesignContext : DbContext {
        public AuthorDesignContext()
            : base("AuthorDesignContext") { 
        }

        public DbSet<ActionToPage> ActionToPages { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminLoginLog> AdminLoginLogs { get; set; }
        public DbSet<AdminOperation> AdminOperations { get; set; }
        public DbSet<AdminToPage> AdminToPages { get; set; }
        public DbSet<Authory> Authories { get; set; }
        public DbSet<AuthoryToPage> AuthoryToPages { get; set; }
        public DbSet<PageAction> PageActions { get; set; }
        public DbSet<PageMenu> PageMenus { get; set; }
    }
}
