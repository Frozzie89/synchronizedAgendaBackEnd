namespace TI_BackEnd.Domain.Invitation
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        Invitation GetByUserRecever(int idUserRecever);
    }
}