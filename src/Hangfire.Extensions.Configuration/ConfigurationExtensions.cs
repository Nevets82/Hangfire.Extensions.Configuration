using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Hangfire.Extensions.Configuration
{
    /// <summary>
    /// Extension methods for configuration classes.
    /// </summary>
    public static class ConfigurationExtensions
    {
        #region Constant members.
        /// <summary>
        /// Contains the default section name.
        /// </summary>
        private const string DEFAULT_SECTION_NAME = "Hangfire";

        /// <summary>
        /// Contains the default dashboard sub section name.
        /// </summary>
        private const string DEFAULT_DASHBOARD_SUB_SECTION_NAME = "Dashboard";

        /// <summary>
        /// Contains the default dashboard sub section name.
        /// </summary>
        private const string DEFAULT_SERVER_SUB_SECTION_NAME = "Server";
        #endregion

        #region Configuration extention methods.
        /// <summary>
        /// Gets the hangfire dashboard options.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The hangfire dashboard options.</returns>
        public static DashboardOptions GetHangfireDashboardOptions(this IConfiguration configuration)
        {
            return ConfigurationExtensions.GetHangfireDashboardOptions(configuration, DEFAULT_SECTION_NAME);
        }

        /// <summary>
        /// Gets the hangfire dashboard options.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="sectionName">The hangfire configuration section name.</param>
        /// <returns>The hangfire dashboard options.</returns>
        public static DashboardOptions GetHangfireDashboardOptions(this IConfiguration configuration, string sectionName)
        {
            return ConfigurationExtensions.GetHangfireDashboardOptions(configuration, sectionName, DEFAULT_DASHBOARD_SUB_SECTION_NAME);
        }

        /// <summary>
        /// Gets the hangfire dashboard options.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="sectionName">The hangfire configuration section name.</param>
        /// <param name="subSectionName">The hangfire dashboard configuration sub section name.</param>
        /// <returns>The hangfire dashboard options.</returns>
        public static DashboardOptions GetHangfireDashboardOptions(this IConfiguration configuration, string sectionName, string subSectionName)
        {
            // Validate arguments.
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (string.IsNullOrWhiteSpace(sectionName)) throw new ArgumentException("The specified string argument cannot be null, empty or whitespace.", nameof(sectionName));
            if (string.IsNullOrWhiteSpace(subSectionName)) throw new ArgumentException("The specified string argument cannot be null, empty or whitespace.", nameof(subSectionName));

            // Load options from configuration object.
            var result = ConfigurationExtensions.LoadOptions<DashboardOptions>(configuration, sectionName, subSectionName);

            // If AppPath is null or white space then...
            if (string.IsNullOrWhiteSpace(result.AppPath))
            {
                // Make white space always null.
                result.AppPath = null;
            }

            // Return the result.
            return result;
        }

        /// <summary>
        /// Gets the hangfire background job server options.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The hangfire background job server options.</returns>
        public static BackgroundJobServerOptions GetHangfireBackgroundJobServerOptions(this IConfiguration configuration)
        {
            return ConfigurationExtensions.GetHangfireBackgroundJobServerOptions(configuration, DEFAULT_SECTION_NAME);
        }

        /// <summary>
        /// Gets the hangfire background job server options.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="sectionName">The hangfire configuration section name.</param>
        /// <returns>The hangfire background job server options.</returns>
        public static BackgroundJobServerOptions GetHangfireBackgroundJobServerOptions(this IConfiguration configuration, string sectionName)
        {
            return ConfigurationExtensions.GetHangfireBackgroundJobServerOptions(configuration, sectionName, DEFAULT_SERVER_SUB_SECTION_NAME);
        }

        /// <summary>
        /// Gets the hangfire background job server options.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="sectionName">The hangfire configuration section name.</param>
        /// <param name="subSectionName">The hangfire server configuration sub section name.</param>
        /// <returns>The hangfire background job server options.</returns>
        public static BackgroundJobServerOptions GetHangfireBackgroundJobServerOptions(this IConfiguration configuration, string sectionName, string subSectionName)
        {
            // Validate arguments.
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (string.IsNullOrWhiteSpace(sectionName)) throw new ArgumentException("The specified string argument cannot be null, empty or whitespace.", nameof(sectionName));
            if (string.IsNullOrWhiteSpace(subSectionName)) throw new ArgumentException("The specified string argument cannot be null, empty or whitespace.", nameof(subSectionName));

            // Load options from configuration object.
            var result = ConfigurationExtensions.LoadOptions<BackgroundJobServerOptions>(configuration, sectionName, subSectionName);

            // If there is more than one queue then...
            if (result.Queues.Length > 1)
            {
                // remove the default queue.
                result.Queues = result.Queues.Skip(1).ToArray();
            }

            // If ServerName is null or white space then...
            if (string.IsNullOrWhiteSpace(result.ServerName))
            {
                // Make white space always null.
                result.ServerName = null;
            }

            // Return the result.
            return result;
        }
        #endregion

        #region Configuration extention methods.
        /// <summary>
        /// Creates and loads a new instance of the options type with the configuration section options.
        /// </summary>
        /// <typeparam name="TOptions">The options type.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="sectionNames">The section names.</param>
        /// <returns>A new instance of the options type with the configuration section options.</returns>
        private static TOptions LoadOptions<TOptions>(IConfiguration configuration, params string[] sectionNames)
            where TOptions : class, new()
        {
            // Validate arguments.
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (sectionNames == null) throw new ArgumentNullException(nameof(sectionNames));
            if (sectionNames.Length == 0) throw new ArgumentException("At lease one section name sould be specified.", nameof(sectionNames));

            var result = new TOptions();

            var section = configuration;

            foreach (var sectionName in sectionNames)
            {
                section = section.GetSection(sectionName);

                if (section == null)
                {
                    break;
                }
            }

            if (section != null)
            {
                section.Bind(result);
            }

            return result;
        }
        #endregion
    }
}
