using Logic.Dtos;

namespace Logic.Interfacaes
{
    public interface IAppliedDal
    {
        void ChangeApplicantAppliedStatus(AppliedDTO appliedDTO);
    }
}