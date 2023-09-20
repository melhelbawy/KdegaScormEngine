using Microsoft.Data.SqlClient;

namespace Kdega.ScormEngine.Application.Common.Models;
public class SqlServerConfiguration
{
    public string DatabaseName { get; set; } = "KdegaScormPlayerDb";
    public string ServerName { get; set; } = @".";
    public bool IsIntegratedSecurity { get; set; } = false;
    public string UserId { get; set; } = "sa";
    public string Password { get; set; } = "P@ssw0rd123";

    public string GetConnectionString()
    {
        SqlConnectionStringBuilder builder = new()
        {
            DataSource = ServerName,
            InitialCatalog = DatabaseName
        };
        if (IsIntegratedSecurity)
        {
            builder.IntegratedSecurity = IsIntegratedSecurity;
        }
        else
        {
            builder.UserID = UserId;
            builder.Password = Password;
        }
        builder.MultipleActiveResultSets = true;
        builder.ApplicationName = "Kdega Scorm Player";
        builder.Encrypt = false;

        return builder.ConnectionString.Replace("Multiple Active Result Sets", "MultipleActiveResultSets");
    }
}
