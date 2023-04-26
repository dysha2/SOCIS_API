using Microsoft.EntityFrameworkCore;
using SOCIS_API.Model;
// See https://aka.ms/new-console-template for more information
using(EquipmentContext _context = new EquipmentContext())
{
    var a =_context.Requests.FromSqlRaw($"exec as User='GarievDenis' SELECT * FROM GetRequests()")
                .Include(x => x.Place);
    foreach(var item in a)
    {

    }
}
