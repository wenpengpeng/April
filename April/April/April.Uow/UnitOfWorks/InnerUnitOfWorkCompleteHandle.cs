// 文件名：InnerUnitOfWorkCompleteHandle.cs
// 
// 创建标识：温朋朋 2018-05-21 9:16
// 
// 修改标识：温朋朋2018-05-21 9:16
// 
// ------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace April.Uow.UnitOfWorks
{
    public class InnerUnitOfWorkCompleteHandle:IUnitOfWorkCompleteHandle
    {
        private volatile bool _isCompleteCalled;
        private volatile bool _isDisposed;       

        public void Complete()
        {
            _isCompleteCalled = true;
        }

        public Task CompleteAsync()
        {
            _isCompleteCalled = true;
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;
            if (!_isCompleteCalled)
            {
                if (HasException())
                    return;
                throw new Exception("Did not call Complete method of a unit of work.");
            }
        }
        /// <summary>
        ///     是否有异常
        /// </summary>
        /// <returns></returns>
        private static bool HasException()
        {
            try
            {
                return Marshal.GetExceptionCode() != 0;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}