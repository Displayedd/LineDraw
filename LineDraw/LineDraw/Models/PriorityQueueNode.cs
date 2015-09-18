using C5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Models
{
    /// <summary>
    /// This class extends the <see cref="Node"/> class
    /// with a IPriorityQueueHandle to support its use
    /// with IPriorityQueue from the C5 library.
    /// </summary>
    public class PriorityQueueNode : Node
    {
        public IPriorityQueueHandle<PriorityQueueNode> Handle;

        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        public PriorityQueueNode() : base()
        {
            Handle = null;
        }
    }
}
