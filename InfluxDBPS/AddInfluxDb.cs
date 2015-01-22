﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddInfluxDb.cs">
// </copyright>
// <summary>
//   Creates a db in InfluxDb
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
    /// Add a db to an InfluxDB
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "InfluxDb")]
    public class AddInfluxDb : Cmdlet
    {
        /// <summary>
        /// The connection object for the InfluxDB instance
        /// </summary>
        [Parameter]
        public IInfluxDb dbConnection { get; set; }

        /// <summary>
        /// The name of the DB to create
        /// </summary>
        [Parameter]
        public string name { get; set; }

        /// <summary>
        /// Create a Db in an InfluxDB instance
        /// </summary>
        /// <returns></returns>
        public InfluxDbApiCreateResponse CreateDB()
        {
            InfluxDbApiCreateResponse createResponse = this.dbConnection.CreateDatabaseAsync(this.name).Result;

            return createResponse;
        }

        /// <summary>
        /// Processes the pipeline
        /// </summary>
        protected override void ProcessRecord()
        {
            var response = this.CreateDB();

            this.WriteObject(response.ToJson());
        }
    }
}
