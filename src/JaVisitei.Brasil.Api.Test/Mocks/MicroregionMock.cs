using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class MicroregionMock
    {
        public static IEnumerable<MicroregionResponse> ReturnMicroregionListMock()
        {
            return new List<MicroregionResponse> {
                ReturnMicroregion1Mock(),
                ReturnMicroregion2Mock(),
                ReturnMicroregion3Mock()
            };
        }

        public static MicroregionResponse ReturnMicroregion1Mock()
        {
            return new MicroregionResponse
            {
                Id = "ba_porto_seguro_micro",
                Name = "Porto Seguro",
                MacroregionId = "ba_sul_baiano_macro",
                Canvas = "M3453MasC353d463656 546df36sd364sd346345"
            };
        }

        public static MicroregionResponse ReturnMicroregion2Mock()
        {
            return new MicroregionResponse
            {
                Id = "al_litoral_norte_alagoano_micro",
                Name = "Litoral Norte Alagoano",
                MacroregionId = "al_leste_alagoano_macro",
                Canvas = "M3453MasC3534636456 546df36sd343463sdf45"
            };
        }

        public static MicroregionResponse ReturnMicroregion3Mock()
        {
            return new MicroregionResponse
            {
                Id = "ac_brasileia_micro",
                Name = "Brasiléia",
                MacroregionId = "ac_vale_do_acre_macro",
                Canvas = "M3453MasC334636456 546df36sd364sa346345"
            };
        }
    }
}