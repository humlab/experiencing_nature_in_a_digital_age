using ENIDABackendAPI.Model;
using System.Linq;

namespace ENIDABackendAPI.Repository
{
    public interface InformationRepository
    {
        IQueryable<Information> GetInformationByImageIdOrderedByOffset(string imageId);
        IQueryable<Information> GetInformation();
    }
}
