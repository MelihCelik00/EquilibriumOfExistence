public class VignetteController : MonoBehaviour
{
    private ChromaticAberration chromaticAberration;
    public float chromaticAberrationIntensity = 0.5f;

    private PostProcessVolume volume;
    private Bloom bloom;
    private Vignette vignette;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.sharedProfile.TryGetSettings<Bloom>(out bloom);
        chromaticAberration = volume.sharedProfile.GetSettings<Vignette>();
        chromaticAberration.intensity.value = 0.5f;
    }



    private void Test()
    {
        //bloom.scatter.value = 0.1f;
        vignette.intensity.value = chromaticAberrationIntensity;
    }
}
