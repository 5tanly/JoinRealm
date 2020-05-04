//MCCScript 1.0

MCC.LoadBot(new JoinRealm());

//MCCScript Extensions

public class JoinRealm : ChatBot{

  string enabled = string.Empty;
  string realm = string.Empty;

  public void GetSettings(){
    //Get user defined settings from joinrealm.ini
    string[] Lines = File.ReadAllLines(@"joinrealm.ini");
    enabled = Lines[1].Remove(0,8);
    realm = Lines[2].Remove(0,6);
  }

  private bool _joinedRealm = false;

  public void joinLoop(){
    //Checks if _joinedRealm is true or false and starts spamming "/server" if _joinedRealm is false
    while(true){
      if (_joinedRealm == false){
        SendText("/server "+ realm);
        Thread.Sleep(1000);
      }
      else{
        Thread.Sleep(10000);
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
      joinLoop();
    }
    else{
      LogToConsole("-------------------------------WARNING-------------------------------");
      LogToConsole("JoinRealm is not enabled, to use it please enable it in joinrealm.ini");
      LogToConsole("---------------------------------------------------------------------");
      _joinedRealm = true;
      UnloadBot();
    }
  }

  public override void GetText(string text){
    text = GetVerbatim(text);
    if (text == "(!) You have no new mail."){
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
