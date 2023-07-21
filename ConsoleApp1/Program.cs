using System.Text;
using ConsoleApp1;
using Ekko;
using LobbyReveal;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class Userinfo
{
    public string userinfo { get; set; }
}
public class currentPlatformId
{
    public string platform { get; set; }
}
public class Lol
{
    public string cpid { get; set; }
}

public class UserinfoActual
{
    public Lol lol { get; set; }

}
class gameData
{
    public string gamedata { get; set; }
}



public class Program
{
    public void Start()
    {
        new Thread(async () => { await Main(); })
        {
            IsBackground = true
        }.Start();
    }
    public async static Task Main(string[] args)
    {
        Program program = new Program();
        program.Start();
        while (true)
        {
            var input = Console.ReadKey(true);
        }
    }
    public async Task Main()
    {

        Console.Title = "Hi :)";
        Console.ForegroundColor = ConsoleColor.White;
        var token = new CancellationTokenSource();
        var watcher = new LeagueClientWatcher();
        watcher.OnLeagueClient += async (clientWatcher, client) =>
        {


            Console.Clear();
            token.Cancel();
            var api1 = new LeagueApi(client.ClientAuthInfo.RiotClientAuthToken, client.ClientAuthInfo.RiotClientPort);
            var api2 = new LeagueApi(client.ClientAuthInfo.RemotingAuthToken, client.ClientAuthInfo.RemotingPort);

            Regions regions = new Regions();
            string region = await regions.GetRegion(api1);

            Names inGameNames = new Names(api2);

            var result = await inGameNames.GetNames();
            if (result == (null, 0,null))
            {
                Console.WriteLine("Please run the program when you are loading in or ingame");
                Console.WriteLine("You can now close this window");
            }
            else
            {
                string msg = CreateMsg(result,region);
                string address = File.ReadAllText("address.txt");
                myWebhook webhook = new myWebhook();
                webhook.sendWebHook(address, msg, "Captain Hook");
                Console.Clear();
                Console.WriteLine("Voice Channel Created");
                Console.WriteLine("You can now close this window");
            }
            

            Console.ReadLine();
            Environment.Exit(0);
        };
        Console.WriteLine("Waiting for league client!");
        await watcher.Observe(token.Token);
        await Task.Delay(-1);
    }

    private string CreateMsg((List<PlayerChampionSelection>,long,string) result,string region)
    {
        var players = result.Item1;
        var gamid = result.Item2;
        string msg = $"[{region}]";
        msg += ";";
        msg += result.Item3;
        msg += ";";
        msg += gamid;
        msg += ";";
        foreach (var player in players)
        {
            msg += player.summonerInternalName;
            if (!(players.IndexOf(player) == players.Count - 1))
            {
                msg += ",";
            }
        }
        msg += ";";
        foreach (var player in players)
        {

            ChampionIds champion = (ChampionIds)Enum.Parse(typeof(ChampionIds), player.championId.ToString());


            msg += champion.ToString();
            if (!(players.IndexOf(player) == players.Count - 1))
            {
                msg += ",";
            }
        }
        return msg;
    } 
    enum ChampionIds
    {
        Aatrox = 266,
        Ahri = 103,
        Akali = 84,
        Akshan = 166,
        Alistar = 12,
        Amumu = 32,
        Anivia = 34,
        Annie = 1,
        Aphelios = 523,
        Ashe = 22,
        AurelionSol = 136,
        Azir = 268,
        Bard = 432,
        Belveth = 200,
        Blitzcrank = 53,
        Brand = 63,
        Braum = 201,
        Caitlyn = 51,
        Camille = 164,
        Cassiopeia = 69,
        Chogath = 31,
        Corki = 42,
        Darius = 122,
        Diana = 131,
        Draven = 119,
        DrMundo = 36,
        Ekko = 245,
        Elise = 60,
        Evelynn = 28,
        Ezreal = 81,
        Fiddlesticks = 9,
        Fiora = 114,
        Fizz = 105,
        Galio = 3,
        Gangplank = 41,
        Garen = 86,
        Gnar = 150,
        Gragas = 79,
        Graves = 104,
        Gwen = 887,
        Hecarim = 120,
        Heimerdinger = 74,
        Illaoi = 420,
        Irelia = 39,
        Ivern = 427,
        Janna = 40,
        JarvanIV = 59,
        Jax = 24,
        Jayce = 126,
        Jhin = 202,
        Jinx = 222,
        Kaisa = 145,
        Kalista = 429,
        Karma = 43,
        Karthus = 30,
        Kassadin = 38,
        Katarina = 55,
        Kayle = 10,
        Kayn = 141,
        Kennen = 85,
        Khazix = 121,
        Kindred = 203,
        Kled = 240,
        KogMaw = 96,
        KSante = 897,
        Leblanc = 7,
        LeeSin = 64,
        Leona = 89,
        Lillia = 876,
        Lissandra = 127,
        Lucian = 236,
        Lulu = 117,
        Lux = 99,
        Malphite = 54,
        Malzahar = 90,
        Maokai = 57,
        MasterYi = 11,
        Milio = 902,
        MissFortune = 21,
        MonkeyKing = 62,
        Mordekaiser = 82,
        Morgana = 25,
        Nami = 267,
        Nasus = 75,
        Nautilus = 111,
        Neeko = 518,
        Nidalee = 76,
        Nilah = 895,
        Nocturne = 56,
        Nunu = 20,
        Olaf = 2,
        Orianna = 61,
        Ornn = 516,
        Pantheon = 80,
        Poppy = 78,
        Pyke = 555,
        Qiyana = 246,
        Quinn = 133,
        Rakan = 497,
        Rammus = 33,
        RekSai = 421,
        Rell = 526,
        Renata = 888,
        Renekton = 58,
        Rengar = 107,
        Riven = 92,
        Rumble = 68,
        Ryze = 13,
        Samira = 360,
        Sejuani = 113,
        Senna = 235,
        Seraphine = 147,
        Sett = 875,
        Shaco = 35,
        Shen = 98,
        Shyvana = 102,
        Singed = 27,
        Sion = 14,
        Sivir = 15,
        Skarner = 72,
        Sona = 37,
        Soraka = 16,
        Swain = 50,
        Sylas = 517,
        Syndra = 134,
        TahmKench = 223,
        Taliyah = 163,
        Talon = 91,
        Taric = 44,
        Teemo = 17,
        Thresh = 412,
        Tristana = 18,
        Trundle = 48,
        Tryndamere = 23,
        TwistedFate = 4,
        Twitch = 29,
        Udyr = 77,
        Urgot = 6,
        Varus = 110,
        Vayne = 67,
        Veigar = 45,
        Velkoz = 161,
        Vex = 711,
        Vi = 254,
        Viego = 234,
        Viktor = 112,
        Vladimir = 8,
        Volibear = 106,
        Warwick = 19,
        Xayah = 498,
        Xerath = 101,
        XinZhao = 5,
        Yasuo = 157,
        Yone = 777,
        Yorick = 83,
        Yuumi = 350,
        Zac = 154,
        Zed = 238,
        Zeri = 221,
        Ziggs = 115,
        Zilean = 26,
        Zoe = 142,
        Zyra = 143
    }
}