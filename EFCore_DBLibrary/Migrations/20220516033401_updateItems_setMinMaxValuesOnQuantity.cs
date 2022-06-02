using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore_DBLibrary.Migrations
{
    public partial class updateItems_setMinMaxValuesOnQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Items set Quantity = 0 where Quantity < 0");
            migrationBuilder.Sql("update Items set Quantity = 1000 where Quantity > 1000");
            migrationBuilder.Sql(@"if not exists(select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME='CK_Items_Quantity_Minimum')
                                 begin
                                    alter table [dbo].[Items] add constraint CK_Items_Quantity_Minimum
                                    check (Quantity >=0)
                                 end
                                 if not exists(select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME='CK_Items_Quantity_Maximum')
                                 Begin
                                    alter table [dbo].[Items] add constraint CK_Items_Quantity_Maximum
                                    check (Quantity <= 1000)
                                 end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"if exists(select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME='CK_Items_Quantity_Minimum')
                                 begin
                                    alter table [dbo].[Items] drop constraint CK_Items_Quantity_Minimum
                                 end");

            migrationBuilder.Sql(@"if exists(select * from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME='CK_Items_Quantity_Maximum')
                                 begin
                                    alter table [dbo].[Items] drop constraint CK_Items_Quantity_Maximum
                                 end");
        }
    }
}
