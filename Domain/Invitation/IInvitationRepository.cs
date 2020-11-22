using System.Collections.Generic;

namespace TI_BackEnd.Domain.Invitation
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        Invitation GetByUserAndPlanning(int idUserRecever, int idPlanning);
        IEnumerable<Invitation> QueryFromUserRecever(int idUserRecever);
    }
}