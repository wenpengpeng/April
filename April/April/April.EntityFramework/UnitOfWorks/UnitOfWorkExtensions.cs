// 文件名：UnitOfWorkExtensions.cs
// 
// 创建标识：温朋朋 2018-05-21 10:22
// 
// 修改标识：温朋朋2018-05-21 10:22
// 
// ------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using April.Uow.UnitOfWorks;

namespace April.EntityFramework.UnitOfWorks
{
    public static class UnitOfWorkExtensions
    {
        public static TDbContext GetDbContext<TDbContext>(this IActiveUnitOfWork unitOfWork)
            where TDbContext:DbContext
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (!(unitOfWork is EfUnitOfWork))
            {
                throw new ArgumentException("unitOfWork is not type of " + typeof(EfUnitOfWork).FullName, nameof(unitOfWork));
            }

            return (unitOfWork as EfUnitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}