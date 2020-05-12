//MCCScript 1.0

MCC.LoadBot(new JoinRealm());

//MCCScript Extensions

public class JoinRealm : ChatBot{

  string enabled = string.Empty;
  string realm = string.Empty;
  private bool _joinedRealm = true; //Set to true by default so when loaded inside a realm it won't start spamming until hub is joined
  int count = 0;

  public void GetSettings(){
    //Get user defined settings from joinrealm.ini
    string[] Lines = File.ReadAllLines(@"joinrealm.ini");
    enabled = Lines[1].Remove(0,8).ToLower();
    realm = Lines[2].Remove(0,6).ToLower();
  }

  public void startLoop(){
    //Set variables and start the loop
    _joinedRealm = false;
  }

  public void stopLoop(){
    //Set variables to stop the loop
    _joinedRealm = true;
  }

  public override void Initialize(){
    GetSettings();
    if (enabled == "true"){
      LogToConsole("Sucessfully Initialized!");
      LogToConsole("Enabled: "+enabled);
      LogToConsole("Realm: "+realm);
      //Start the loop that sends "/server"
      startLoop();
    }
    else{
      //Print to console if the script settings are disabled
      LogToConsole("-------------------------------WARNING-------------------------------");
      LogToConsole("JoinRealm is not enabled, to use it please enable it in joinrealm.ini");
      LogToConsole("---------------------------------------------------------------------");
      //Unload the script
      UnloadBot();
    }
  }

  public override void Update(){
    if (enabled == "true"){
      if (count > 10 && _joinedRealm == false){
        //Checks if _joinedRealm is true or false and starts spamming "/server" if _joinedRealm is false
        count = 0;
        SendText("/server "+ realm);
        // Console.WriteLine("SENDING /SERVER");
      }
      else if (_joinedRealm == true){
        //pass
      }
      else count++;
    }
  }

  public override void GetText(string text){
    text = GetVerbatim(text);
    if (text =="(!) While you were offline…"){
      //Stop spamming "/server" if the player has already joined the realm.
      stopLoop();
    }
    else if (text == "(!) You have teleported to Hub Realm!"){
      //Start spamming "/server" if the player has teleported back to hub realm.
      startLoop();
    }
  }
}
