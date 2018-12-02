declare module server {
	/** Holds information about our custom x360ce/xinput hook dll */
	interface GameHookInfo {
		/** If the game should be run using our custom version of x360ce for gamepad control.Enabled by default as the majority of our games need it */
		customDllEnabled: boolean;
		/** If the game supports direct input joysticks */
		dInputEnabled: boolean;
		/** If we should completely remove support for DirectInput input from the game */
		dInputForceDisable: boolean;
		/** If the game supports xinput joysticks */
		xInputEnabled: boolean;
		xInputNames: string[];
		xInputCopies: string[];
		/** If xinput is enabled, if rerouting should be enabled (basically is we'll reroute directinput back to xinput,so we can track more than 4 gamepads on xinput at once) */
		xInputReroute: boolean;
		/** If our custom dll should set the window size and position locally, instead of the handler(inconsistent with some window sizes, look at Borderlands2.js for an example usage) */
		setWindowSize: boolean;
		/** If our custom DLL should hook into the game's window and fake Window's eventsso we never leave focus. Depends on the ForceFocusWindowRegex variable(used for games that don't work when out of focus. See Borderlands.js) */
		forceFocus: boolean;
		/** If force focus is enabled, this is the window we are attaching ourselves toand the window we are going to keep on top.This is used in a very specific case even out */
		ForceFocusWindowRegex: string;
		blockMouseEvents: boolean;
		blockKeyboardEvents: boolean;
		blockInputEvents: boolean;
	}
}
