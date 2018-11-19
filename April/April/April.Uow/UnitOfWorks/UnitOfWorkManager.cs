// 文件名：UnitOfWorkManager.cs
// 
// 创建标识：温朋朋 2018-05-15 9:20
// 
// 修改标识：温朋朋2018-05-15 9:20
// 
// ------------------------------------------------------------------------------
using April.Core.Ioc;

namespace April.Uow.UnitOfWorks
{
    public class UnitOfWorkManager:IUnitOfWorkManager
    {
        private readonly IIocResolve _iocResolve;
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public UnitOfWorkManager(IIocResolve iocResolve, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _iocResolve = iocResolve;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public IActiveUnitOfWork Current => _currentUnitOfWorkProvider.Current;
        /// <summary>
        ///     Begin
        /// </summary>
        /// <returns></returns>
        public IUnitOfWorkCompleteHandle Begin()
        {
            var outerUow = _currentUnitOfWorkProvider.Current;
            if (outerUow != null)
                return new InnerUnitOfWorkCompleteHandle();
            var uow = _iocResolve.Resolve<IUnitOfWork>();
            uow.Begin();
            _currentUnitOfWorkProvider.Current = uow;
            return uow;
        }
    }
}