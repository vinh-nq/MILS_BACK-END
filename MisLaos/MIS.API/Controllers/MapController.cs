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
    public class MapController : ApiController
    {
        IProvinceProvider _provinceProvider;
        IDistrictProvider _districtProvider;
        IVillageProvider _villageProvider;
        public MapController()
        {
            _provinceProvider = new ProvinceProvider();
            _districtProvider = new DistrictProvider();
            _villageProvider = new VillageProvider();
        }

        [HttpGet]
        public IHttpActionResult GetAllProvince()
        {
            List<ProvinceViewModel> provinceViewModels = _provinceProvider.GetAllProvince();
            return Ok(provinceViewModels);
        }

        [HttpGet]
        public IHttpActionResult GetAllDistrict()
        {
            List<DistrictViewModel> districtViewModels = _districtProvider.GetAllDistrict();
            return Ok(districtViewModels);
        }

        [HttpGet]
        public IHttpActionResult GetAllVillage()
        {
            List<VillageViewModel> villageViewModels = _villageProvider.GetAllVillage();
            return Ok(villageViewModels);
        }

    }
}
