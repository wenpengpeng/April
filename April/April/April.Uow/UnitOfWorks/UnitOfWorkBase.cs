// 文件名：UnitOfWorkBase.cs
// 
// 创建标识：温朋朋 2018-05-09 17:35
// 
// 修改标识：温朋朋2018-05-09 17:35
// 
// ------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace April.Uow.UnitOfWorks
{
    public abstract class UnitOfWorkBase:IUnitOfWork
    {
        /// <summary>
        ///     是否释放
        /// </summary>
        private bool _disposed;

        public bool IsDisposed => _disposed;

        /// <summary>
        ///     SaveChanges
        /// </summary>
        public abstract void SaveChanges();
        /// <summary>
        ///     SaveChangesAsync
        /// </summary>
        /// <returns></returns>
        public abstract Task SaveChangesAsync();
        /// <summary>
        ///     CompleteUnit
        /// </summary>
        protected abstract void CompleteUnit();
        /// <summary>
        ///     CompleteUnitAsync
        /// </summary>
        protected abstract Task CompleteUnitAsync();
        /// <summary>
        /// DisposeUnit
        /// </summary>
        protected abstract void DisposeUnit();
        /// <summary>
        ///     BeginUnit
        /// </summary>
        protected abstract void BeginUnit();
        /// <summary>
        ///     Begin
        /// </summary>
        public  void Begin()
        {
            BeginUnit();
        }

        public IUnitOfWork Outer { get; set; }

        /// <summary>
        ///     Complete
        /// </summary>
        public void Complete()
        {
            CompleteUnit();
        }
        /// <summary>
        ///     CompleteAsync
        /// </summary>
        public async Task CompleteAsync()
        {
            await CompleteUnitAsync();
        }
        /// <summary>
        ///     Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        ///     Dispose
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (!disposing) return;
            try
            {
                DisposeUnit();
                _disposed = true;
            }
            catch(Exception ex)
            {
                throw new Exception($"dispose unitofwork is error, error reason is {ex}");
            }
        }
    }
}