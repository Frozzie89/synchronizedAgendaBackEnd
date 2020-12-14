using System.Collections.Generic;

namespace TI_BackEnd.Domain.Invitation
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        Invitation GetByUserAndPlanning(int idUserRecever, int idPlanning);
        IEnumerable<Planning.Planning> QueryPlanningsOfUserRecever(int idUserRecever);
        // IEnumerable<Invitation> QueryFromUserRecever(int idUserRecever);
        Invitation Create(Invitation invitation, string userEmail);

    }
}