using Genkit;

namespace XRL.World.ZoneBuilders {

    /// <summary>
    /// Custom ZoneBuilder that creates a vertical fortress surrounded by air.
    /// </summary>
    public class CrabEmperor_BestiaryExpanded_CitadelBuilder : ZoneBuilderSandbox
    {
        public bool BuildZone(Zone Z)
        {
            // Get center of map
            Point2D center = Location2D.Get(40, 12).Point;

            // Radius of pit is dependent on depth: radius=10 at Z=10, radius=4 at Z=15
            // Radius at each intermediate floor is determined by linearly interpolating between
            // these two values.
            float startFloor = 5f, endFloor = 10f;
            float startRadius = 6f, endRadius = 16f;
            float Radius = ((float)Z.Z - startFloor) * endRadius + (endFloor - (float)Z.Z) * startRadius;
            Radius = Radius / (endFloor - startFloor);

            foreach(Cell C in Z.GetCells())
            {
                if (C.CosmeticDistanceTo(center) < Radius)
                {
                    continue;
                }

                C.RemoveObjects((GameObject o) => true).ForEach(delegate(GameObject o)
                {
                    o.Obliterate();
                });
                C.Clear();
                C.AddObject("Air");
            }

            ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, pathWithNoise: true);

            return true;
        }
   }
}