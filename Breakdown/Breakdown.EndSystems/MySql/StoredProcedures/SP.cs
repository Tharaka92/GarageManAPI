using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public abstract class SP
    {
        protected SP() { }

        public virtual string GetName()
        {
            return GetType().Name;
        }
    }
}
