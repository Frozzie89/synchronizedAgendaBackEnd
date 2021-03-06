using System.Collections.Generic;

namespace TI_BackEnd.Domain.Member
{
    public interface IMemberRepository
    {
        IEnumerable<Member> Query();
        IEnumerable<Member> QueryFromPlanning(int idPlanning);
        IEnumerable<Planning.Planning> QueryPlanningsFromMember(int idUser, bool isGranted);
        IEnumerable<Member> QueryFromUser(int idUser);
        IEnumerable<Member> QueryFromGrantedUser(int idUser);
        Member Create(Member member);
        bool Delete(int idUser, int idPlanning);
        Member Get(int idUser, int idPlanning);
        bool Update(int idUser, int idPlanning, Member member);
    }
}