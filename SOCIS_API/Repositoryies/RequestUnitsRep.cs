using System.Reflection.Metadata.Ecma335;

namespace SOCIS_API.Repositoryies
{
    public class RequestUnitsRep : IRequestUnitsRep
    {
        private readonly EquipmentContext _context;
        public RequestUnitsRep(EquipmentContext context)
        {
            _context = context;
        }
        #region Get
        public RequestUnit? Get(int ReqUnId)
        {
            return LoadData(_context.RequestUnits.Where(x => x.Id == ReqUnId)).FirstOrDefault();
        }

        public List<RequestUnit> GetAllByWorkOnRequest(int worId)
        {
            return LoadData(_context.RequestUnits.Where(x => x.WorkOnRequestId == worId)).ToList();
        }
        private IQueryable<RequestUnit> LoadData(IQueryable<RequestUnit> items)
        {
            return items
                .Select(x => new RequestUnitsDTO(x)
                {
                    Unit=new AccountingUnitDTO(x.Unit)
                    {
                        FullNameUnit=new FullNameUnitDTO(x.Unit.FullNameUnit)
                        {
                            Firm=x.Unit.FullNameUnit.Firm == null?null:new FirmDTO(x.Unit.FullNameUnit.Firm),
                            UnitType=new UnitTypeDTO(x.Unit.FullNameUnit.UnitType)
                        }
                    },
                    WorkOnRequest=new WorkOnRequestDTO(x.WorkOnRequest)
                });
        }
        #endregion

        #region Add
        public RequestUnit AddMy(RequestUnit requestUnit, int userId)
        {
            if (IsMyWorkOnRequest(requestUnit.WorkOnRequestId, userId))
            {
                RequestUnit newReqUn = new RequestUnit
                {
                    WorkOnRequestId = requestUnit.WorkOnRequestId,
                    UnitId = requestUnit.UnitId
                };
                _context.RequestUnits.Add(newReqUn);
                _context.SaveChanges();
                return newReqUn;
            }
            throw new Exception("WorkOnRequest is not your. You can`t add units");
        }
        #endregion

        #region Update
        public void UpdateMy(int ReqUnId, RequestUnit newReqUn, int userId)
        {
            var oldReqUn = _context.RequestUnits.Find(ReqUnId);
            if (oldReqUn is null) throw new Exception("NotFound");
            if (IsMyWorkOnRequest(oldReqUn.WorkOnRequestId, userId))
            {
                oldReqUn.UnitId = newReqUn.UnitId;
                _context.SaveChanges();
            }
            else 
            throw new Exception("WorkOnRequest is not your. Update banned");
        }
        #endregion

        #region Delete
        public void DeleteMy(int ReqUnId, int userId)
        {
            var oldReqUn = _context.RequestUnits.Find(ReqUnId);
            if (oldReqUn is null) throw new Exception("NotFound");
            if (IsMyWorkOnRequest(oldReqUn.WorkOnRequestId, userId))
            {
                _context.RequestUnits.Remove(oldReqUn);
                _context.SaveChanges();
            }
            else
            throw new Exception("WorkOnRequest is not your. Delete banned");
        }
        #endregion

        private bool IsMyWorkOnRequest(int worId,int userId)
        {
            var wor = _context.WorkOnRequests.Find(worId);
            if (wor is null) return false;
            if (wor.ImplementerId!=userId) return false;
            return true;
        }
    }
}
