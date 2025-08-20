using UnityEngine;

public class SkyboxChange : MonoBehaviour
{
public Material skyboxMaterial;

    void Start()
    {
        // if (skyboxMaterial != null)
        // {
        //     // Change the bottom color (ground color)
        //     Color newBottomColor = Color.blue; // Your desired color here
        //     skyboxMaterial.SetColor("_GroundColor", newBottomColor);

        //     // Apply it as the active skybox
        //     RenderSettings.skybox = skyboxMaterial;
        // }
        ResetSkyboxGroundColor();
    }
void ResetSkyboxGroundColor()
{
    if (RenderSettings.skybox != null && RenderSettings.skybox.HasProperty("_GroundColor"))
    {
        Color defaultGround = new Color(0.369f, 0.349f, 0.341f);
        RenderSettings.skybox.SetColor("_GroundColor", defaultGround);
        DynamicGI.UpdateEnvironment();
    }
}

}
