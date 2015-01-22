// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetVersionInfluxDb.cs">
// </copyright>
// <summary>
//   Get the version an InfluxDb
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace InfluxDBPS
{
    using System;
    using System.Management.Automation;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using InfluxDB.Net;
    using InfluxDB.Net.Models;

    /// <summary>
    /// Get the version an InfluxDb
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "VersionInfluxDb")]
    public class GetVersionInfluxDb : Cmdlet
    {
        /// <summary>
        /// A connection object to the InfluxDB instance
        /// </summary>
        [Parameter]
        public IInfluxDb dbConnection { get; set; }

        /// <summary>
        /// Get the version of the db 
        /// </summary>
        /// <returns>A version string</returns>
        private string Version()
        {
            var version = this.dbConnection.VersionAsync().Result;

            return version;
        }

        /// <summary>
        /// Processes the pipeline
        /// </summary>
        protected override void ProcessRecord()
        {
            var version = this.Version();
            this.WriteObject(version.ToJson());
        }
    }
}
