//MCCScript 1.0

MCC.LoadBot(new JoinRealm());

//MCCScript Extensions

public class JoinRealm : ChatBot{

  string enabled = string.Empty;
  string realm = string.Empty;
  private Thread joinThread;

  public void GetSettings(){
    //Get user defined settings from joinrealm.ini
    string[] Lines = File.ReadAllLines(@"joinrealm.ini");
    enabled = Lines[1].Remove(0,8).ToLower();
    realm = Lines[2].Remove(0,6).ToLower();
  }

  private bool _joinedRealm = false;

  public void joinLoop(){
    // Checks if _joinedRealm is true or false and starts spamming "/server" if _joinedRealm is false
    while(true){
      if (_joinedRealm == false){
        SendText("/server "+ realm);
        Thread.Sleep(1000);
      }
      else{
        Thread.Sleep(1000);
        // Console.WriteLine("waiting");
      }
    }
  }

  public override void Initialize(){
    GetSettings();
    if (enabled == "true"){
      LogToConsole("Sucessfully Initialized!");
      LogToConsole("Enabled: "+enabled);
      LogToConsole("Realm: "+realm);
      //Start spamming "/server"
      joinThread  = new Thread(joinLoop);
      joinThread.Start();
    }
    else{
      LogToConsole("-------------------------------WARNING-------------------------------");
      LogToConsole("JoinRealm is not enabled, to use it please enable it in joinrealm.ini");
      LogToConsole("---------------------------------------------------------------------");
      UnloadBot();
    }
  }

  public override void GetText(string text){
    text = GetVerbatim(text);
    if (text =="(!) While you were offline…"){
      //Stop spamming "/server" if the player has already joined the realm.
      LogToConsole("Realm joined!");
      _joinedRealm = true;
    }
    else if (text == "(!) You have teleported to Hub Realm!"){
      //Start spamming "/server" if the player has teleported back to hub realm.
      _joinedRealm = false;
    }
  }
}
