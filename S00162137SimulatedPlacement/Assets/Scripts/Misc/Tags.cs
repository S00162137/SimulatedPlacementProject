using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags  {

    //Used to simplify finding objects of Tag x
    #region Tags
    //NPCS
    public static string PLAYER_TAG = "Player";
    public static string PLAYERWEAPON_TAG = "PlayerWeapon";
    public static string MILITIA_TAG = "Militia";
    public static string VILLAGER_TAG = "Villager";
    public static string ENEMY_TAG = "Enemy";
    public static string ENEMY_PLAYER = "EnemyPlayer";


    //Buildings
    #region FriendlyBuildings Or Player1

    public static string BUILDING_TAG = "Building";
    public static string TEMPLATEBUILDING_TAG = "TemplateBuilding";

    public static string TOWNHALL_TAG = "TownHall";
    public static string RALLYPOINT_TAG = "RallyPoint";


    public static string ENEMYBUILDING_TAG = "EnemyBuilding";
    #endregion


    //Controllers
    public static string GAMECONTROLLER_TAG = "GameController";
    public static string SCENECONTROLER_TAG = "SceneController";
    public static string LEVELCONTROLLER_TAG = "LevelController";
    public static string PLAYER_CONTROLLER_TAG = "PlayerController";
    public static string ENEMY_CONTROLLER_TAG = "EnemyController";
    public static string MINIMAP_CAMERA_TAG = "MinimapCamera";


    #region environmentTags
    public static string GROUND_TAG = "Ground";
    public static string ENVIRNMENT_TAG = "Environment";
    public static string BORDER_TAG = "BorderBounds";

    //used for AI
    public static string TREES_TAG = "Trees";
    public static string VEINS_TAG = "Veins";
    public static string FOOD_OBJ =  "FoodGenObj";


    public static string UNDERGROUND_ENTRANCE_TAG = "UndergroundEntrance";


    #endregion


    #endregion

    #region Neutral Enemy Tags
    public static string GIBBLERHOVEL_TAG = "GibblerHovel";
    public static string GIBBLER_TAG = "Gibbler";

    public static string NEUTRALENEMY_TAG = "NeutralEnemy";
    public static string NEUTRALBASE_TAG = "NeutralBase";

    #endregion






    //Used for finding Specific Objects that are ChildObjects Or File Locations
    #region Directories

    //Dir Loading Screen
    public static string FadeScreen = "/Canvas/LoadingPanel";
    //Dir Texts
    public static string DirFoodText = "/Canvas/BottomBar/InfoPanel/ResourcesPanel/FoodDisplay/TextImage/FoodText";
    public static string DirWoodText = "/Canvas/BottomBar/InfoPanel/ResourcesPanel/WoodDisplay/TextImage/WoodText";
    public static string DirStoneText = "/Canvas/BottomBar/InfoPanel/ResourcesPanel/StoneDisplay/TextImage/StoneText";
    public static string DirMetalText = "/Canvas/BottomBar/InfoPanel/ResourcesPanel/MetalDisplay/TextImage/MetalText";
    public static string DirPeopleText = "/Canvas/BottomBar/InfoPanel/ResourcesPanel/PeopleDisplay/TextImage/PeopleText";
    public static string DirUnemployedPeopleText = "/Canvas/BottomBar/InfoPanel/ResourcesPanel/UnEmployedDisplay/TextImage/UnemployedText";

    public static string DirErrorMessageText = "/Canvas/ErrorMesage";


    //Buttons
    public static string DirBuildPanelHouse = "/Canvas/BottomBar/ConstructionPanel/BuildHouse";
    public static string DirBuildPanelLumbermill = "/Canvas/BottomBar/ConstructionPanel/BuildWoodCutters";
    public static string DirBuildPanelMine = "/Canvas/BottomBar/ConstructionPanel/BuildMine";
    public static string DirBuildPanelFarm = "/Canvas/BottomBar/ConstructionPanel/BuildFarm";
    public static string DirBuildPanelBarracks = "/Canvas/BottomBar/ConstructionPanel/BuildBarracks";
    public static string DirBuildPanelArchery = "/Canvas/BottomBar/ConstructionPanel/BuildArchery";
    public static string DirBuildPanelGateway = "/Canvas/BottomBar/ConstructionPanel/BuildGateway";
    public static string DirBuildPanelTower = "/Canvas/BottomBar/ConstructionPanel/BuildTower";
    public static string DirBuildPanelCancelBTN = "/Canvas/BottomBar/ConstructionPanel/CancelBuild";
    public static string DirBuildDisplay = "/Canvas/BottomBar/BuildingInfo";

    //Milita and Player Directories
    public static string DirSoldierBTNS = "/Canvas/BottomBar/ActivePanel/MilitiaPanel/SoldierDisplay/SoldierButtons";
    public static string DirPikemenBTNS = "/Canvas/BottomBar/ActivePanel/MilitiaPanel/PikemanDisplay/PikemanButtons";
    public static string DirShieldbearerBTNS = "/Canvas/BottomBar/ActivePanel/MilitiaPanel/ShieldBearerDisplay/ShieldBearerButtons";
    public static string DirMlitaryBTNS = "/Canvas/BottomBar/ActivePanel/MilitiaPanel/AllDisplay/AllUnitButtons";

    public static string DirDeathPanel = "/Canvas/deathpanel";
    public static string DirEndScreen = "/Canvas/EndScreen";


    //Minimap
    public static string DirMoveLeftBTN = "/Canvas/BottomBar/MinimapPanel/MoveLeftBTN";
    public static string DirMoveRightBTN = "/Canvas/BottomBar/MinimapPanel/MoveRightBTN";
    public static string DirMoveLevelBTN = "/Canvas/BottomBar/MinimapPanel/MoveLevelBTN";



    //Dir Options

    public static string OptionsDirOptionPanel = "/Canvas/OptionsMenu";
    public static string OptionsDirMusic = "/Canvas/OptionsMenu/Panel/OptionsPanel/MusicOption";
    public static string OptionsDirSliderMusic = "/Canvas/OptionsMenu/Panel/OptionsPanel/MusicOption/MusicSound";

    public static string OptionsDirSoundEffect = "/Canvas/OptionsMenu/Panel/OptionsPanel/SFXOption";
    public static string OptionsDirSliderSFX = "/Canvas/OptionsMenu/Panel/OptionsPanel/SFXOption/SFXSound";

    public static string OptionsDirSoundAmbiant = "/Canvas/OptionsMenu/Panel/OptionsPanel/AmbiantOption";
    public static string OptionsDirSliderAmbiant = "/Canvas/OptionsMenu/Panel/OptionsPanel/AmbiantOption/AmbiantSound";




    public static string OptionsDirMainMenuBTN = "/Canvas/OptionsMenu/Panel/MainMenuButton";
    public static string OptionsDirApplyBTN = "/Canvas/OptionsMenu/Panel/ApplyButton";
    public static string OptionsDirCancelBTN = "/Canvas/OptionsMenu/Panel/CancelButton";


    public static string OptionDirBTNToggle = "/Canvas/Button Panels/OptionMenu";



    #endregion

}
