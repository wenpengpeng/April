// 文件名：CurrentUnitOfWorkProvider.cs
// 
// 创建标识：温朋朋 2018-05-15 9:25
// 
// 修改标识：温朋朋2018-05-15 9:25
// 
// ------------------------------------------------------------------------------

using System.Threading;

namespace April.Uow.UnitOfWorks
{
    public class CurrentUnitOfWorkProvider:ICurrentUnitOfWorkProvider
    {
        public IUnitOfWork Current
        {
            get => GetCurrentUow();
            set => SetCurrentUow(value);
        }
        private static readonly AsyncLocal<LocalUowWrapper> AsyncLocalUow = new AsyncLocal<LocalUowWrapper>();
        /// <summary>
        ///     获取当前工作单元
        /// </summary>
        /// <returns></returns>
        private static IUnitOfWork GetCurrentUow()
        {
            var uow = AsyncLocalUow.Value?.UnitOfWork;
            if (uow == null)
                return null;
            if (!uow.IsDisposed) return uow;
            AsyncLocalUow.Value = null;
            return null;
        }
        /// <summary>
        ///     设置当前工作单元
        /// </summary>
        /// <param name="value"></param>
        private static void SetCurrentUow(IUnitOfWork value)
        {
            lock (AsyncLocalUow)
            {
                if (value == null)
                {
                    if (AsyncLocalUow.Value == null)
                    {
                        return;
                    }

                    if (AsyncLocalUow.Value.UnitOfWork?.Outer == null)
                    {
                        AsyncLocalUow.Value.UnitOfWork = null;
                        AsyncLocalUow.Value = null;
                        return;
                    }

                    AsyncLocalUow.Value.UnitOfWork = AsyncLocalUow.Value.UnitOfWork.Outer;
                }
                else
                {
                    if (AsyncLocalUow.Value?.UnitOfWork == null)
                    {
                        if (AsyncLocalUow.Value != null)
                        {
                            AsyncLocalUow.Value.UnitOfWork = value;
                        }

                        AsyncLocalUow.Value = new LocalUowWrapper(value);
                        return;
                    }

                    value.Outer = AsyncLocalUow.Value.UnitOfWork;
                    AsyncLocalUow.Value.UnitOfWork = value;
                }
            }
        }
        private class LocalUowWrapper
        {
            public IUnitOfWork UnitOfWork { get; set; }

            public LocalUowWrapper(IUnitOfWork unitOfWork)
            {
                UnitOfWork = unitOfWork;
            }
        }
    }
}