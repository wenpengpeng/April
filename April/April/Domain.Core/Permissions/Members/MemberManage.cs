// 文件名：MemberManage.cs
// 
// 创建标识：温朋朋 2018-06-01 10:49
// 
// 修改标识：温朋朋2018-06-01 10:49
// 
// ------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using April.Common.Predicates;
using April.Web.Services;
using Domain.Core.Repositories;

namespace Domain.Core.Permissions.Members
{
    public class MemberManage:IDomainService
    {
        /// <summary>
        ///     会员用户仓储
        /// </summary>
        private readonly IAprilRepository<Member, long> _memberRepository;

        public MemberManage(IAprilRepository<Member, long> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        /// <summary>
        ///     根据会员Id获取会员信息
        /// </summary>
        public async Task<Member> GetMemberByUserBaseIdAsync(long userBaseId)
        {
            var predicate = new PredicateGroup<Member>();
            predicate.AddPredicate(true, s => s.UserId == userBaseId);

            var managerList = await _memberRepository.QueryAsync(predicate.Predicates);

            return managerList.FirstOrDefault();
        }
    }
}