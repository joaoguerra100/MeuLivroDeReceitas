using FluentMigrator;

namespace MyRecipeBook.Infrastructure.Migrations.Versions;

[Migration(DataBaseVersions.TABLE_USER, "Create table to save the user's information")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("Users")
            .WithColumn("Name").AsAnsiString(255).NotNullable()
            .WithColumn("Email").AsAnsiString(255).NotNullable()
            .WithColumn("Password").AsAnsiString(2000).NotNullable();
    }
}