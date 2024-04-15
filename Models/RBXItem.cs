using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace RBXStudio.Models
{
    public enum RBXClass
    {
        StatsItem,
Part,
TextLabel,
MeshPart,
ImageLabel,
WedgePart,
Frame,
Weld,
Model,
ImageButton,
ModuleScript,
Vector3Value,
RemoteEvent,
Attachment,
Texture,
ParticleEmitter,
Seat,
UIAspectRatioConstraint,
RunningAverageItemInt,
Decal,
TotalCountTimeIntervalItem,
CornerWedgePart,
ScreenGui,
TextButton,
Sound,
UISizeConstraint,
LocalScript,
Folder,
UnionOperation,
Motor6D,
NumberValue,
UIGridLayout,
WeldConstraint,
BindableEvent,
StringValue,
CoreScript,
ScrollingFrame,
UIListLayout,
SurfaceGui,
RemoteFunction,
SpecialMesh,
PointLight,
BillboardGui,
Script,
BoolValue,
TouchTransmitter,
UIPadding,
Accessory,
Tween,
RunningAverageItemDouble,
ImageHandleAdornment,
LocalizationTable,
TextBox,
StarterGear,
Trail,
Tool,
Backpack,
PathfindingService,
SpawnerService,
GroupService,
AdService,
TestService,
BindableFunction,
ReplicatedStorage,
Teams,
GuidRegistryService,
Visit,
ClientReplicator,
NetworkClient,
HapticService,
HumanoidController,
ControllerService,
SolidModelContentProvider,
MeshContentProvider,
GamepadService,
ChangeHistoryService,
RobloxReplicatedStorage,
RuntimeScriptService,
Selection,
ThirdPartyUserService,
SunRaysEffect,
ColorCorrectionEffect,
Sky,
BloomEffect,
BlurEffect,
Lighting,
HttpService,
ScriptContext,
AnalyticsService,
TouchInputService,
AssetService,
ScriptService,
ContextActionService,
VRService,
MouseService,
KeyboardService,
UserInputService,
CookiesService,
Debris,
GamePassService,
InsertService,
FriendService,
BadgeService,
PhysicsService,
CollectionService,
JointsService,
TeleportService,
LocalizationService,
StarterGui,
StarterPack,
StarterPlayer,
TextService,
TweenService,
HttpRbxApiService,
ReplicatedFirst,
NotificationService,
PointsService,
MarketplaceService,
KeyframeSequenceProvider,
ContentProvider,
LogService,
CSGDictionaryService,
NonReplicatedCSGDictionaryService,
SoundService,
TimerService,
Stats,
GuiService,
RunService,
SpawnLocation,
Terrain,
Pants,
Shirt,
BodyColors,
Status,
Humanoid,
Workspace,
PlayerGui,
PlayerScripts,
DataModel,
VirtualInputManager,
PlayerMouse,
Player,
Animation,
AnimationTrack,
Translator,

    }
    public class RBXItem
    {
        List<RBXItem> items;
        public RBXClass rbclass;

        public Dictionary<string, object> properties =
                new Dictionary<string, object>();
        public List<RBXItem> Items { get { return items; } }

        public string Name => properties["Name"] as string;
        public RBXItem(XElement element)
        {
            string rclassstr = element.Attribute("class")?.Value;
            rbclass = (RBXClass)Enum.Parse(typeof(RBXClass), rclassstr);

            int val = 0;
            foreach (XElement xitem in element.Elements("Item"))
            {
                if (items == null)
                    items = new List<RBXItem>();
                items.Add(new RBXItem(xitem));
            }
            XElement propertiesElem = element.Element("Properties");
            foreach (XElement xprop in propertiesElem.Elements())
            {
                string propname = xprop.Attribute("name")?.Value;
                string proptype = xprop.Name.LocalName;
                switch (proptype)
                {
                    case "bool":
                        properties[propname] = bool.Parse(xprop.Value);
                        break;
                    case "float":
                        properties[propname] = xprop.Value == "inf" ? float.PositiveInfinity : float.Parse(xprop.Value);
                        break;
                    case "int":
                        properties[propname] = int.Parse(xprop.Value);
                        break;
                    case "string":
                        properties[propname] = xprop.Value;
                        break;
                }
            }
        }
    }
}
