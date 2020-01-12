using System.Text.RegularExpressions;

namespace Armaiti.Core
{
    public static class PreventInjectionExtensions
    {
        /// <summary>
        /// Regular expression of dangerous keywords of sql commands.
        /// </summary>
        private static readonly Regex SQL_COMMAND =
            new Regex(@"('(''|[^'])*')|(\b(SELECT|INSERT( +INTO){0,1}|UPDATE|DELETE|DROP|CREATE|ALTER|EXEC(UTE){0,1}|MERGE|UNION( +ALL){0,1})|GRANT|TRUNCATE\b)",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
        /// <summary>
        /// Regular expression of dangerous keywords of sys commands.
        /// </summary>
        private static readonly Regex SYS_COMMAND =
            new Regex(@"^'|\s?SYSOBJECTS\s?|\s?XP_.*?|\s?SYSLOGINS\s?|\s?SYSREMOTE\s?|\s?SYSUSERS\s?|\s?SYSXLOGINS\s?|\s?SYSDATABASES\s?|\s?ASPNET_.*?",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// This method checks whether the sql string is safe to run on the sql-server db engine.
        /// </summary>
        /// <param name="query">The sql string.</param>
        /// <returns>Returns true if sql string is safe, otherwise false.</returns>
        public static bool IsSqlSafe(this string query)
        {
            if (SQL_COMMAND.IsMatch(query) || SYS_COMMAND.IsMatch(query))
                return false;

            return true;
        }
    }
}
