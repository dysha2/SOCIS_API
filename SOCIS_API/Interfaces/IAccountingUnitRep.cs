namespace SOCIS_API.Interfaces
{
    public interface IAccountingUnitRep
    {
        AccountingUnit? Get(int id);
        List<AccountingUnit> GetAll();
        List<AccountingUnit> GetAllByFullNameUnit(int fnuId);
        //List<AccountingUnit> GetAllByPlace(int placeId);
        //List<AccountingUnit> GetAllByPerson(int personId);

    }
}
