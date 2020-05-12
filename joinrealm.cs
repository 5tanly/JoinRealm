//MCCScript 1.0

MCC.LoadBot(new JoinRealm());

//MCCScript Extensions

public class JoinRealm : ChatBot{

  string enabled = string.Empty;
  string realm = string.Empty;
  string username = GetUsername();
  private Thread joinThread;
  private bool _joinedRealm;

  public void GetSettings(){
    //Get user defined settings from joinrealm.ini
    string[] Lines = File.ReadAllLines(@"joinrealm.ini");
    enabled = Lines[1].Remove(0,8).ToLower();
    realm = Lines[2].Remove(0,6).ToLower();
  }

  public void joinLoop(){
    while(true){
      if (_joinedRealm == false){
        //Checks if _joinedRealm is true or false and starts spamming "/server" if _joinedRealm is false
        SendText("/server "+ realm);
        Thread.Sleep(1000);
      }
      else{
        //Stops the loop from continuing to run in the background
        break;
      }
    }
  }

  public void createThread(){
    //Set variables and start the loop
    LogToConsole("Starting loop");
    _joinedRealm = false;
    joinThread  = new Thread(joinLoop);
    joinThread.Start();
  }

  public void killThread(){
    //Set variables to stop the loop
    LogToConsole("Stopping loop");
    _joinedRealm = true;
  }

  public override void Initialize(){
    GetSettings();
    if (enabled == "true"){
      LogToConsole("Sucessfully Initialized!");
      LogToConsole("Enabled: "+enabled);
      LogToConsole("Realm: "+realm);
      //Start the loop that sends "/server"
      createThread();
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

  public override void GetText(string text){
    text = GetVerbatim(text);
    if (text =="(!) While you were offline…"){
      //Stop spamming "/server" if the player has already joined the realm.
      LogToConsole("Realm joined!");
      killThread();
    }
    else if (text == "(!) You have teleported to Hub Realm!"){
      //Start spamming "/server" if the player has teleported back to hub realm.
      LogToConsole("Hub joined!");
      createThread();
    }
    else if (text == "[" + username + "] -> me] unload"){
      //Unload script
      LogToConsole("Unloading script");
      killThread();
      UnloadBot();
    }
  }
}
