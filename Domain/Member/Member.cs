using System;
namespace TI_BackEnd.Domain.Member
/* This class is used to define a member.
 * A user is defined by an id from a user, an id from a planning and a bool to differentiate which member
 * is moderator of the planning
 */
{
    public class Member
    {
        public int IdUser { get; set; }
        public int IdPlanning { get; set; }
        public bool IsGranted { get; set; }

        public Member() { }

        public Member(int idUser, int idPlanning, bool isGranted)
        {
            IdUser = idUser;
            IdPlanning = idPlanning;
            IsGranted = isGranted;
        }
    }
}