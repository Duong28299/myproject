using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Mintax.ETax.Infrastructure.DatabaseContext
{
    public interface IDatabaseContextFactory
    {
        IDatabaseContext Context();
    }
}
