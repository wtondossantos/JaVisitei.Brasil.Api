using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class MacroregionMock
    {
        public static IEnumerable<MacroregionResponse> ReturnMacroregionListMock()
        {
            return new List<MacroregionResponse> {
                ReturnMacroregion1Mock(),
                ReturnMacroregion2Mock(),
                ReturnMacroregion3Mock()
            };
        }

        public static MacroregionResponse ReturnMacroregion1Mock()
        {
            return new MacroregionResponse
            {
                Id = "ma_leste_maranhense_macro",
                Name = "Leste Maranhense",
                StateId = "ma_maranhao_estado",
                Canvas = "M3453MasC353d4636456 546df36sd364sd346345"
            };
        }

        public static MacroregionResponse ReturnMacroregion2Mock()
        {
            return new MacroregionResponse
            {
                Id = "ce_metropolitana_de_fortaleza_macro",
                Name = "Metropolitana de Fortaleza",
                StateId = "ce_ceara_estado",
                Canvas = "M3453MasC3534636456 546df36sd3643463sdf45"
            };
        }

        public static MacroregionResponse ReturnMacroregion3Mock()
        {
            return new MacroregionResponse
            {
                Id = "ac_vale_do_acre_macro",
                Name = "Vale do Acre",
                StateId = "ac_acre_estado",
                Canvas = "M3453MasC3534636456 546df36sd364sa346345"
            };
        }
    }
}