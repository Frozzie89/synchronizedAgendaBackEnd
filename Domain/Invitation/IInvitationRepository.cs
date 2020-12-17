using System.Collections.Generic;

namespace TI_BackEnd.Domain.Invitation
{
    public interface IInvitationRepository : IRepositoryGet<Invitation>, IRepositoryCreate<Invitation>, IRepositoryDelete<Invitation>
    {
        Invitation GetByUserAndPlanning(int idUserRecever, int idPlanning);
        IEnumerable<Planning.Planning> QueryPlanningsOfUserRecever(int idUserRecever);
        Invitation Create(Invitation invitation, string userEmail);

    }
}