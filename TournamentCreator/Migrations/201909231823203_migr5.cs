namespace TournamentCreator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "IsCompleted");
        }
    }
}
