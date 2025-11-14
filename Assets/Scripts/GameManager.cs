using UnityEngine;


public enum Game_State
{
    None,
    Main_Menu,
    Enter_Room,
    Enter_Name,
    Choose_Image, // MC
    Wait_For_Play, // Play
    SetUPImage,
    Play,
    Game_Over
}


public class GameManager : MonoBehaviour
{
    private EvenCollect evenCollect;
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public SpriteShow[] spriteShows;
    private static bool applicationIsQuitting = false;


    private void Awake()
    {
        // if (_instance != null || _instance != this)
        //     Destroy(this.gameObject);
        // else
        _instance = this;
        evenCollect = GetComponent<EvenCollect>();
    }

    // private void OnDestroy()
    // {
    //     if (_instance == this)
    //     {
    //         _instance = null;
    //     }
    // }

    // private void Start()
    // {
    //     if (RoomData.Instance.isAdmin)
    //     {
    //         StartState(Game_State.Choose_Image);
    //     }
    //     else
    //     {
    //         StartState(Game_State.Enter_Name);
    //     }
    // }

    [SerializeField] private Game_State game_State = Game_State.None;
    public void StartState(Game_State _new_State)
    {
        EndState();
        game_State = _new_State;
        switch (game_State)
        {
            case Game_State.Enter_Name:
                evenCollect.enterName.Raise(this, -999);
                break;
            case Game_State.Choose_Image:
                evenCollect.chooseImage.Raise(this, -999);
                break;
            case Game_State.Wait_For_Play:
                evenCollect.waitForPlay.Raise(this, -999);
                break;

            case Game_State.SetUPImage:
                evenCollect.setUpImage.Raise(this, -999);
                break;
            case Game_State.Play:
                evenCollect.play.Raise(this, -999);
                break;
            case Game_State.Game_Over:
                evenCollect.gameover.Raise(this, -999);
                break;
        }
    }

    public void EndState()
    {

        switch (game_State)
        {
            case Game_State.Main_Menu:
                break;
            case Game_State.Enter_Room:
                break;
            case Game_State.Enter_Name:

                break;
            case Game_State.Choose_Image:
                break;
            case Game_State.Wait_For_Play:
                break;
            case Game_State.Play:

                break;
            case Game_State.Game_Over:
                break;
        }
    }
    public void UpdateState()
    {

        switch (game_State)
        {
            case Game_State.Main_Menu:
                break;
            case Game_State.Enter_Room:
                break;
            case Game_State.Enter_Name:

                break;
            case Game_State.Choose_Image:
                break;
            case Game_State.Wait_For_Play:
                break;
            case Game_State.Play:

                break;
            case Game_State.Game_Over:
                break;
        }
    }

    //Call with btn
    public void ConfirmBTN()
    {
        StartState(Game_State.Play);


    }
    public Sprite[] sss;
    public void Pixlatale()
    {
        var rdn = RNG.Instance.GetRandomNumber();
        var pxe = PixelatedHandle.Instance;
        var sc = SpriteShow.currentSpriteSelete;
        sss = sc;
        pxe.SetOriginalTexturn(sc[rdn].texture);
        pxe.Pixelate(SpriteShow.currentIndex, rdn);
    }


}
