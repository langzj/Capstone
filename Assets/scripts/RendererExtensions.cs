//The script was derived from the Unity2D Tutorial at http://pixelnest.io/tutorials/2d-game-unity/background-and-camera/

using UnityEngine;

// This allows the the camera scene bounds that is provided by the camera so that
//   Player can stay within the camera boundaries and enemies can only be activated when the camera arrives
public static class RendererExtensions {

    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
