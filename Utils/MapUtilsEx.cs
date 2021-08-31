using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.MathTools;

namespace Foundation.Utils
{
    public static class MapUtilsEx
    {
        public static Vec3i ChunkPosFromChunkIndex3D(long chunkIndex3d, int ChunkMapSizeX, int ChunkMapSizeZ)
        {
            return new Vec3i((int)(chunkIndex3d % (long)ChunkMapSizeX), (int)(chunkIndex3d / (long)(ChunkMapSizeX * ChunkMapSizeZ)), (int)(chunkIndex3d / (long)ChunkMapSizeX % (long)ChunkMapSizeZ));
        }

        /// <summary>
        /// Todo: omg I can't wait for tuples - out parameters are bad.
        /// </summary>
        /// <param name="chunkIndex2d"></param>
        /// <param name="chunkMapSizeX"></param>
        /// <param name="chunkX"></param>
        /// <param name="chunkZ"></param>
        public static void ChunkPosFromChunkIndex2DL(long chunkIndex2d, int chunkMapSizeX, out int chunkX, out int chunkZ)
        {
            var retVec = new Vec2i();
            MapUtil.PosInt2d(chunkIndex2d, chunkMapSizeX, retVec);

            chunkX = retVec.X;
            chunkZ = retVec.Y;
        }
    }
}
