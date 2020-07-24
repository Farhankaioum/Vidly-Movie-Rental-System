using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class NameSeedAddedInCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Update MembershipType set Name='Pay as You Go' where Id = 1");
            migrationBuilder.Sql("Update MembershipType set Name='Monthly' where Id = 2");
            migrationBuilder.Sql("Update MembershipType set Name='Monthly' where Id = 3");
            migrationBuilder.Sql("Update MembershipType set Name='Yearly' where Id = 3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
