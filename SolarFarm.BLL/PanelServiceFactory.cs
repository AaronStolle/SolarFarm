using SolarFarm.DAL;
using SolarFarm.Core.Interfaces;

namespace SolarFarm.BLL
{
    public class PanelServiceFactory
    {
        public static IPanelService GetPanelService()
        {
            return new PanelService();
        }
    }
}
