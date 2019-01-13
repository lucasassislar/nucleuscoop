# Nucleus Co-Op
Nucleus Co-Op is a tool for Windows that allows split-screen play on many games that do not initially support it.
Its purpose is to make it as easy as possible for the average user to play games locally.
Essentially, Nucleus opens multiple instances of the same game with sym-linked files, each with its own customized version of xinput libraries that will only answer to ony specific gamepad instance.

Support its development by donating to my Patreon! https://www.patreon.com/distro

Subscribe to our subreddit: https://www.reddit.com/r/nucleuscoop/

Join our Discord: https://discord.gg/jrbPvKW

![alt text](https://raw.githubusercontent.com/lucasassislar/nucleuscoop/gh-pages/images/ncoopv10.jpg)

# How does it work?
Starting from Alpha 4, all games use a generic handler that can handle pretty much all situations.
To add a new game, you can just create a new *.js file in the games folder, and describe what your game needs to run.
Now, what the GenericHandler actually does?

When the user hits play:
- If the game needs modifications to the save files, we backup them so when the splitscreen session ends we can return all the configurations back to normal.
- The app symlinks the entire game folder to the Data folder, so we can launch each instance of the game with custom DLLs.
- Runs the JavaScript engine, so any custom code that needs to be executed by player ID runs
- We copy a custom xinput dll specific for each gamepad: Each xinput passthroughs a specific gamepad input to the 1st gamepad (xinput1 just passthroughs, xinput2 passes to 2, i.e).
- If needed, we extract SmartSteamEmu and start the game using it.
- Now we keep track of the processes, looking for the launcher and the actual game window, so we can position it correctly on the screen.


# How to help with development?
If you want to contribute code, here is a brief summary of what you need to begin developing:
TODO: finish up this section :/

For the application:
	- Visual Studio 2015 or 2017 (Community works fine)

For the games's Javascript files
	- Visual Studio Code
	- ProcessExplorer


## Built With

* [x360ce](https://github.com/x360ce/x360ce)
* [x360ce (branch used)](https://github.com/lucasassislar/x360ce)
