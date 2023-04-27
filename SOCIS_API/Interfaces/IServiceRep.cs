namespace SOCIS_API.Interfaces
{
    public interface IServiceRep
    {
        public Service Get(int id);
        public IEnumerable<Service> GetAll();
        public void Create();
    }
}
