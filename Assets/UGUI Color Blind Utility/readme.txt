UGUI Color-Blind Utility, v1.0.0
Developed by Tylah Heil at Silver Fox Games.

-------

About: 
Easily add Color-blind support to interfaces created with UGUI, this add-on allows you to assign alternate colors and sprites to UGUI Images and RawImages as well as alternate colors and fonts to Text, this also supports the color of button target states.

This add-on also includes a color-blind simulation filter post-processing effect to be used for testing purposes (Requires Unity Pro License).

-------

Preparing a UI for Color-blind support:
To prepare your UI, simply add the 'ColorBlindUtility' component to your UI's Root object. 

Preparing a UI Image for Color-blind support:
Assign the 'ColorBlindImage' component to your Image in the UI and from there set up your alternate colors and/or alternate sprites for each type of Color-Blindness.

Preparing a UI RawImage for Color-blind support:
Assign the 'ColorBlindRawImage' component to your RawImage in the UI and from there set up your alternate colors and/or alternate textures for each type of Color-Blindness.

Preparing a UI Text for Color-blind support:
Assign the 'ColorBlindText' component to your Text in the UI and from there set up your alternate colors and/or alternate fonts for each type of Color-Blindness.

How to change the colors for UI Button states:
Assign the correct component to your graphic as directed above, and if your graphic is the target of a Button, additional color fields will be exposed on the component in the inspector.

Using the Color-Blind Filter Post-Processing Effect: 
Simply drag the Color-Blind Filter Image Effect onto your UI camera and select one of the available color-blind modes in the inspector. If you're on a Mac and you're experiencing graphic stretching around the edge of the screen you will need to go into Player Settings in the Unity Editor, uncheck 'Auto Graphics API for Mac' and drag OpenGL2 to the top of the exposed list to fix the issue.

-------

Release Notes: 

v1.0.0
- Initial Release

-------

If you have any questions or wish to request a feature for future releases of this product send us an email at contact@silverfoxgames.com with 'UGUI Color-Blind Utility' as the subject line.

-------

Visit http://www.silverfoxgames.com/ for more information on our products. 