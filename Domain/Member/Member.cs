using System;
namespace TI_BackEnd.Domain.Member
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