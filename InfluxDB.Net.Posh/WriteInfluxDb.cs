﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriteInfluxDb.cs">
// </copyright>
// <summary>
//   Writes data to an InfluxDb
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Management.Automation;
using InfluxDB.Net.Models;

namespace InfluxDB.Net.Posh
{
    /// <summary>
    /// Writes data to an InfluxDb
    /// </summary>
    [Cmdlet(VerbsCommunications.Write, "InfluxDb")]
    public class WriteInfluxDb : Cmdlet
    {
        /// <summary>
        /// The connection for the InfluxDB instance
        /// </summary>
        [Parameter]
        public IInfluxDb dbConnection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string dbName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string seriesName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string[] columns { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public object[] values { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public InfluxDbApiResponse Write()
        {
            Serie serie = new Serie.Builder(seriesName)
                .Columns(columns)
                .Values(values)
                .Build();
            InfluxDbApiResponse writeResponse = dbConnection.WriteAsync(dbName, TimeUnit.Milliseconds, serie).Result;
            return writeResponse;
        }

        /// <summary>
        /// Processes the pipeline
        /// </summary>
        protected override void ProcessRecord()
        {
            var writeResponse = this.Write();
            this.WriteObject(writeResponse.ToJson());
        }
    }
}
