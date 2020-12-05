using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.HCT.ETax.Infrastructure.DatabaseContext
{
    public interface IDatabaseContextFactory
    {
        IDatabaseContext Context();
    }
}
