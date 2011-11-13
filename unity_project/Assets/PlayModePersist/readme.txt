PlayModePersist Readme - AlmostLogical Software - support@almostlogical.com

Overview
------------
While in Play Mode, when you select a GameObject you will see directly below the Transform title in the inspector a new drop-down called ‘PlayModePersist’. Clicking this drop-down will allow you to select which component you wish to persist after you click stop. Once you have selected the desired checkboxes for each component within each GameObject you can then click stop. It will then automatically persist your changes.

If you have any questions/issues or feature requests please feel free to contact us at support@almostlogical.com.

Bug Fix in Version 1.5.1
------------------------
- Changed shortcut keys for open/closing PlayModePersist dropdown and persisting all components within selected GameObject to Shift+Alt+O and Shift+Alt+P respectively.

New to Version 1.5
-------------------------
- Shortcut key : Open/closing the PlayModePersist dropdown : Shift+Alt+O
- Shortcut key : Persisting all components within selected GameObject : Shift+Alt+P
- Auto Persist Settings - Ability to search for and add components you would like to always auto persist in all your projects (Window -> PlayModePerist Auto Persist Settings)
- Fix : Issue persisting multiple GameObjects at the same time
- Bug Fixes : Cleaning up warning and error messages that can occur since Unity 3.2

How to Use Auto Persist
-------------------------------
Go to Window -> PlayModePerist Auto Persist Settings. This window will allow you to select which components you wish to always auto persist within your project. You can scroll through the list and click the add button for each component you wish to Auto Persist. You can also filter the search by typing the start of the components name into the search field above. If you wish to see which components you are currently persisting, you can click the checkbox at the bottom. This will allow you to easily remove any components you no longer want to auto persist. 
Now once you click play within a scene all you need to do is at least once click on an object in your hierarchy (this activates PlayModePersist). Now all auto persist components will be checked and once you click stop they will remain.
An important note, as there is no difference between components changed in the editor vs by code, if you change an auto persisting option programatically this value will also persist.


Troubleshooting
--------------------
Q: A PlayModePersist dropdown is not appearing in the Transform Inspector
A: First check to make sure you are in PlayMode as it will only appear then. If you are in PlayMode and it still does not appear try removing all plugins/editor scripts from your project. Sometimes other scripts try to use the Transform inspector and block PlayModePersist.
Q: When check a checkbox(s) and clicking stop my settings to not persist
A: First try removing all plugins/editor scripts from your project to see if that resolves the problem. Next make sure you are not changing scenes in code. For example if you switch to another scene in code you are not longer modifying the same GameObjects as were in the editor. After clicking play if your code load another scene, then reloads the current scene these will also be new copies GameObject compared to what was in the editor so you will be unable to persist changes.
Q: Shortcut Keys are not working.
A: Try opening the edit menu and see if the two menu options appear in that list. Sometimes just opening the menu for the first time is required to activate the shortcut keys.

If you are still having problems please contact us at: support@almostlogical.com.

Advanced Users
----------------------
If you wish for an individual property to never persist (be it within a built-in Unity component or a custom component), you can manually add the to the code at simply add that property and class as an exception at the bottom of PPLocalStorageManager.cs within PlayModePersist/Editor. IMPORTANT NOTE: Updates may cause this to be overwritten so keep a backup available. Please let me know if you need to do this often as we will add creating a settings window for this task to our features list.

Thanks for purchasing PlayModePersist.

If you have any questions/issues or feature requests please feel free to contact us at support@almostlogical.com.