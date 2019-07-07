namespace TDL.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoListItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(maxLength: 100),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TodoListItems");
        }
    }
}
