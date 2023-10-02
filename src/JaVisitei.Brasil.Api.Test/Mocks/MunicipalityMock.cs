using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class MunicipalityMock
    {
        public static IEnumerable<MunicipalityResponse> ReturnMunicipalityListMock()
        {
            return new List<MunicipalityResponse> {
                ReturnMunicipality1Mock(),
                ReturnMunicipality2Mock(),
                ReturnMunicipality3Mock()
            };
        }

        public static MunicipalityResponse ReturnMunicipality1Mock()
        {
            return new MunicipalityResponse
            {
                Id = "ba_camacari",
                Name = "Camaçari",
                MicroregionId = "ba_salvador_micro",
                Canvas = "M3453MasC3534636456 546dssf36sd364346s345"
            };
        }

        public static MunicipalityResponse ReturnMunicipality2Mock()
        {
            return new MunicipalityResponse
            {
                Id = "ce_aquiraz",
                Name = "Aquiraz",
                MicroregionId = "ce_fortaleza_micro",
                Canvas = "M3453MasC354s636456 546df36sd36436345"
            };
        }

        public static MunicipalityResponse ReturnMunicipality3Mock()
        {
            return new MunicipalityResponse
            {
                Id = "ac_assis_brasil",
                Name = "Assis Brasil",
                MicroregionId = "ac_brasileia_micro",
                Canvas = "M3453MsC3534636456 54d6df36sd36434345"
            };
        }
    }
}
