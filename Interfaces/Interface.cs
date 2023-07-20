using CVAPI.Models;

namespace CVAPI.Interfaces
{
    public interface ICVInterface
    {
        Student GetCV(string StudentId);

        bool studentExists(string StudentId);
    }
}
