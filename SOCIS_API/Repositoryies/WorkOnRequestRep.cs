namespace SOCIS_API.Repositoryies
{
    public class WorkOnRequestRep : IWorkOnRequestRep
    {
        private EquipmentContext _context;
        public WorkOnRequestRep(EquipmentContext equipmentContext)
        {
            _context = equipmentContext;
        }
        public WorkOnRequest Get(int id, int userId)
        {
            return _context.WorkOnRequests.FirstOrDefault(x => x.Id == id && x.Request.DeclarantId == userId);
        }
    }
}
