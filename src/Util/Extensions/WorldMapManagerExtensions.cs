using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace Foundation.Util.Extensions
{
    /// <summary>
    /// This class is a workaround because the api has no easy way to add waypoints
    /// </summary>
    public static class WorldMapManagerExtensions
    {
        public static WaypointMapLayer GetWaypointMapLayer(this WorldMapManager mapManager) => mapManager.MapLayers.OfType<WaypointMapLayer>().FirstOrDefault();

        public static void AddWaypointToPlayer(this WorldMapManager mapManager, Waypoint waypoint, IServerPlayer player)
        {
            var mapLayer = mapManager.GetWaypointMapLayer();
            if (mapLayer != null)
            {
                mapLayer.Waypoints.Add(waypoint);
                ResendWaypoints(mapManager, player, mapLayer);
            }
        }

        public static void ResendWaypoints(this WorldMapManager mapManager, IServerPlayer player, WaypointMapLayer mapLayer)
        {
            //copied from WaypointMapLayer because the method is not public
            Dictionary<int, PlayerGroupMembership> memberOfGroups = player.ServerData.PlayerGroupMemberships;
            List<Waypoint> hisMarkers = new List<Waypoint>();

            foreach (Waypoint marker in mapLayer.Waypoints)
            {
                if (player.PlayerUID != marker.OwningPlayerUid && !memberOfGroups.ContainsKey(marker.OwningPlayerGroupId)) continue;
                hisMarkers.Add(marker);
            }
            mapManager.SendMapDataToClient(mapLayer, player, SerializerUtil.Serialize(hisMarkers));
        }
    }
}
