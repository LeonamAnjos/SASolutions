using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace SA.General.Service.Database
{
    public static class Connection
    {
        public static void ProtectConnectionString(string exePath)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            setConnectionStringProtection(config, true);
        }
        
        public static void ProtectConnectionString()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            setConnectionStringProtection(config, true);
        }

        public static void UnprotectConnectionString(string exePath)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            setConnectionStringProtection(config, false);
        }

        public static void UnprotectConnectionString()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            setConnectionStringProtection(config, false);
        }

        public static bool IsConnectionStringProtected 
        { 
            get
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (config == null)
                    throw new ApplicationException("Arquivo de configuração da aplicação inválido!");

                ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
                return section.SectionInformation.IsProtected;
            }
        }

        public static bool IsConnectionStringDefined(string name)
        {
            return (ConfigurationManager.ConnectionStrings[name] != null);
        }

        public static string DefaultDatabaseName
        {
            get
            {
                DatabaseSettings settings = DatabaseSettings.GetDatabaseSettings(new SystemConfigurationSource());
                return settings.DefaultDatabase;
            }
        }

        public static bool isPasswordDefined(string databaseName)
        {
            return (!string.IsNullOrEmpty(GetPassword(databaseName)));
        }

        private static string GetPassword(string databaseName)
        {
            DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
            builder.ConnectionString = GetConnectionString(databaseName);

            string result = null;

            if (builder.ContainsKey("password"))
                result = (string)builder["password"];

            return result;
        }

        public static string GetConnectionString(string databaseName)
        {
            string returnValue = null;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config == null)
                throw new ApplicationException("Arquivo de configuração da aplicação inválido!");

            var section = config.GetSection("connectionStrings") as ConnectionStringsSection;
            if (section != null) 
                returnValue = section.ConnectionStrings[databaseName].ConnectionString;

            return returnValue;
        }

        private static void setConnectionStringProtection(Configuration config, bool protect)
        {
            if (config == null)
                throw new ApplicationException("Arquivo de configuração da aplicação inválido!");

            var section = config.GetSection("connectionStrings") as ConnectionStringsSection;

            if ((section == null) ||
                (section.ElementInformation.IsLocked) ||
                (section.SectionInformation.IsLocked))
                return;

            if (protect)
            {
                if (section.SectionInformation.IsProtected)
                    return;

                //section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            }
            else
            {
                if (!(section.SectionInformation.IsProtected))
                    return;

                section.SectionInformation.UnprotectSection();
            }

            section.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Full);
        }
    }
}
