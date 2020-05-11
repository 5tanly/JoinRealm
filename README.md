## What?

This is an advanced JoinRealm script designed to replace the standard [AntiAFK] function in [Minecraft Console Client](https://github.com/ORelio/Minecraft-Console-Client/ "Minecraft Console Client") specifically made for SaicoPvP Realms, I can not guarantee it works for any other servers.

## Why do I need this?

If you are playing factions on SaicoPvP you know that whenever you fire any cannon that uses more than 1 sand and/or TNT entity, the server drops to 5 ticks per second. When this happens al the alt accounts spamming `/server` get kicked because when the server finally catches up, those accounts are sending about 10 messages per second and the server ends their life for spamming.
[Take a look at this forum post.](https://saicopvp.com/forums/threads/saicopve-in-a-nutshell.230634/ "Take a look at this forum post.")
What this does is stop spamming `/server` once you join the realm, so when it drops down to 5 ticks per second, your alt accounts won't lag and get kicked for spam.

## What can I do about this?
You? Nothing, I have already done it for you. What you can do is install this script I have been working on for the past couple of days to stop spamming `/server` once your account joins a realm and start spamming again when it is in a hub realm.

## Why should I trust you that this ain't a rat?
I admire your online security concerns, but, the code is open source and you can read through it to see what it does, along with comments to aid you along your way. I assure you this is safe.

## How do I use this?
Follow these steps and you will be well on your way

------------


##### 1. Download the files from the GitHub repo

##### 2. Place the files in the directory where you run MinecraftClient.exe from; i.e. working directory, the only files you need are the .cs and .ini files

##### 3. Make sure [ScriptScheduler] is enabled in MinecraftClient.ini

	[ScriptScheduler]
	enabled=true
	tasksfile=tasks.ini

##### 4 .Make sure [AntiAFK] is disabled in MinecraftClient.ini (or at least not spamming /server [...])

	[AntiAFK]
	enabled=false

##### 5. Make sure botmessagedelay is set to 1 or less in MinecraftClient.ini

	botmessagedelay=1

##### 6. Add the following tasks to tasks.ini; if tasks.ini does not exist, create it

	[Task]
	triggerOnFirstLogin=true
	script=joinrealm.cs

##### 7. Enable and make sure your realm is set properly in joinrealm.ini make sure there are no spaces before or after the equals sign

	[JoinRealm]
	enabled=true
	realm=skeleton
	
## Troubleshooting
###### I get an error when I join the server and the script doesnt start
The script was designed around build 275 of [Minecraft Console Client](https://github.com/ORelio/Minecraft-Console-Client), if you use an older version of MCC you may not have all the features that this script take advantage of. Download and run the latest version of MCC to fix this.
