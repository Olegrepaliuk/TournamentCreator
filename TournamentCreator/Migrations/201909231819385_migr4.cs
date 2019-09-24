namespace TournamentCreator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Matches", "IsCompleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Matches", "IsCompleted", c => c.Boolean(nullable: false));
        }
    }
}
