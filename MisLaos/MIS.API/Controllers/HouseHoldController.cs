using MIS.Data.Providers;
using MIS.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MIS.API.Controllers
{
    public class HouseHoldController : ApiController
    {
        IHouseHoldProvider _houseHoldProvider;
        public HouseHoldController()
        {
            _houseHoldProvider = new HouseHoldProvider();
        }

        [HttpGet]
        public IHttpActionResult SearchHouseHold()
        {
            List<HouseHoldViewModel> houseHoldViewModels = _houseHoldProvider.SearchHouseHold("", "", "", "", -1, -1, "");
            return Ok(houseHoldViewModels);
        }

    }
}
