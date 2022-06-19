using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class ArchipelagoMock
    {
        public static List<ArchipelagoResponse> ReturnArchipelagoListMock()
        { 
            return new List<ArchipelagoResponse> {
                ReturnArchipelago1Mock(),
                ReturnArchipelago2Mock(),
                ReturnArchipelago3Mock()
            };
        }

        public static ArchipelagoResponse ReturnArchipelago1Mock()
        { 
            return new ArchipelagoResponse
            {
                Id = "ba_arquipelago_de_abrolhos_ilha",
                Name = "Arquipélago de Abrolhos",
                MacroregionId = "ba_metropolitana_de_salvador_macro"
            };
        }

        public static ArchipelagoResponse ReturnArchipelago2Mock()
        {
            return new ArchipelagoResponse
            {
                Id = "es_arquipelago_de_trindade_e_martim_vaz_ilha",
                Name = "Arquipélago de Trindade e Martim Vaz",
                MacroregionId = "es_central_espirito_santense_macro"
            };
        }

        public static ArchipelagoResponse ReturnArchipelago3Mock()
        {
            return new ArchipelagoResponse
            {
                Id = "pe_arquipelago_de_sao_pedro_e_sao_paulo_ilha",
                Name = "Arquipélago de São Pedro e São Paulo",
                MacroregionId = "pe_metropolitana_do_recife_macro"
            };
        }
    }
}
