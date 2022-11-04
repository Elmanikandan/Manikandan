using JWTAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JWTAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : Controller
    {
        [HttpGet]
        public IEnumerable<TrainReservation> Get() => CreateTestReservations();

        public List<TrainReservation> CreateTestReservations()
        {
            List<TrainReservation> rList = new List<TrainReservation> {
            new TrainReservation { Id=1, Name = "Mani", StartLocation = "Chennai", EndLocation="Rameshwaram" },
            new TrainReservation { Id=2, Name = "Palani", StartLocation = "Madurai", EndLocation="Chennai" },
            new TrainReservation { Id=3, Name = "Baskar", StartLocation = "Trichy", EndLocation="Madurai" }
            };
            return rList;
        }
    }
}
