using MISA.HCT.ETax.Infrastructure.DatabaseContext;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.HCT.ETax.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDatabaseContext DataContext { get; }
        MySqlTransaction BeginTransaction();
        void Commit();
        void RollBack();
    }
}
