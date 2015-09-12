using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Models
{
    public class LineQueryResult
    {
        /// <summary>
        /// The computed line. Should be set to null 
        /// when <see pref="Success"/> is set to false.
        /// </summary>
        public Point[] Result { get; set; }

        /// <summary>
        /// True if line was computed.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error message if line was not computed.
        /// </summary>
        public string Message { get; set; }
    }
}
