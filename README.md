# Cube Fight

### Multiplayer augmented reality game powered by [PubNub](https://www.pubnub.com/?devrel_gh=Cube-Fight).

<img src="/Magic-Leap-Multiplayer-Demo-op.gif?raw=true" alt="Magic Leap Multiplayer Demo" width="350" align="right" />

Augmented reality is a fresh new layer for gaming that promises new experiences for users and more revenue for game studios. However, AR brings new challenges to traditional game design when you start to build multi-user games and need to sync content between them. Even in single player games where there's no content to sync, there's a significant demand from users to add social experiences like <a href="https://www.pubnub.com/products/chatengine/" target="_blank" rel="noopener">chat</a> and <a href="https://www.pubnub.com/blog/realtime-highscores-leaderboards-in-unity/" target="_blank" rel="noopener">leaderboards</a>.

The objective of <a href="https://github.com/chandler767/Cube-Fight" target="_blank" rel="noopener">Cube Fight</a> is to take cubes from other players by using the <a href="https://creator.magicleap.com/learn/tutorials/eye-gaze-unity" target="_blank" rel="noopener">Magic Leap Eye Gaze</a> feature to select a cube and the trigger on the controller to take it. PubNub will handle transmission of messages regarding the ownership state of each cube.

Learn more about how to build your own multiplayer AR game with Magic Leap and PubNub from the [tutorial](https://www.pubnub.com/blog/Multiplayer-Augmented-Reality-Game-Magic-Leap-unity/).

<a href="https://www.pubnub.com/blog/Multiplayer-Augmented-Reality-Game-Magic-Leap-unity/?devrel_gh=Cube-Fight">
    <img alt="PubNub Blog" src="https://i.imgur.com/aJ927CO.png" width=260 height=98/>
</a>

## Why PubNub and Magic Leap?
<em>This is Part three of my Magic Leap series. Check out the other projects/posts <a href="https://www.pubnub.com/blog/getting-started-with-magic-leap-and-unity?devrel_gh=cube-fight" target="_blank" rel="noopener">Getting Started with Magic Leap and PubNub</a> and <a href="https://www.pubnub.com/blog/magic-leap-controlling-internet-connected-devices-lights-doors-with-hand-gestures/" target="_blank" rel="noopener">Controlling Internet-connected Devices with Magic Leap Hand Gestures</a>.</em>

Developers have been building multiplayer games and other multi-user experiences with PubNub for years, and <a href="https://www.pubnub.com/blog/getting-started-with-magic-leap-and-unity" target="_blank" rel="noopener">PubNub definitely sees AR as next on the horizon</a>. PubNub is a natural fit in the AR world and their technology can power the realtime interaction between AR headsets or physical objects in the same location, or even across the Earth.

For instance, when a Magic Leap user throws a ball in the virtual world, that motion is synchronized in realtime across every other connected user. Or if a user uses a hand gesture to turn on a light, <a href="https://github.com/chandler767/Magic-Leap-IoT-Example" target="_blank" rel="noopener">PubNub is sending the message to that light to turn on</a>. Multi-user experiences, or the relationship between the AR headset and the physical world around us, is where PubNub is required and excels.

## How it Works
This game uses <a href="https://creator.magicleap.com/learn/tutorials/eye-gaze-unity" target="_blank" rel="noopener">Magic Leap Eye Gaze</a> to select game objects (the cubes) and the trigger on the controller to take them. Additionally, there's an eye position indicator to aid the user in knowing where the device thinks they are looking.

There are three states/colors each cube can be in:

* Not Focused (<strong>red</strong>) - The user is <strong>not looking</strong> directly at the cube, has <strong>not taken</strong> the cube, and<strong> another player owns</strong> the cube.

<img src="https://www.pubnub.com/blog/wp-content/uploads/2018/10/image-red-1024x768.jpg" alt="Not Focused Cube Fight Cube" width="300" align="center" />

 * Focused (<strong>dark red</strong>) - The user has <strong>looked at</strong>, but <strong>not taken</strong> the cube and<strong> another player owns</strong> the cube. Notice the eye position indicator is on the cube (the blue sphere).
 
 <img src="https://www.pubnub.com/blog/wp-content/uploads/2018/10/selected-1024x768.jpg" alt="Focused Cube Fight Cube" width="300" align="center" />
 
 * Owned (<strong>blue</strong>) - The user has <strong>looked at</strong> and<strong> took the cube</strong> by pulling the trigger.
 
 <img src="https://www.pubnub.com/blog/wp-content/uploads/2018/10/image-blue-1024x768.jpg" alt="Owned Cube Fight Cube" width="300" align="center" />
 
Each cube has the script <a href="https://github.com/chandler767/Cube-Fight/blob/master/Cube-Fight/Assets/EyeSelection.cs" target="_blank" rel="noopener">"EyeSelection"</a> as a component. This script handles eye tracking and controller events. When the player looks directly at a cube and pulls the trigger a message is published to PubNub to inform other players of the ownership change. When the players receive a message the cube color is updated to reflect the current state of the cube.

## How to Build 

The tutorial [*Create a Multiplayer Augmented Reality Game with Magic Leap*](https://www.pubnub.com/blog/Multiplayer-Augmented-Reality-Game-Magic-Leap-unity/) details how to build and run this project in full.

## What's Next?
The augmented world is your oyster with Magic Leap + PubNub. Here are a few ideas to get you started:
<ul>
 	<li>Build a AR chess game.</li>
 	<li>Create a AR zombie shooter.</li>
 	<li><a href="https://github.com/chandler767/Magic-Leap-Gesture-IoT-Example" target="_blank" rel="noopener">Use gesture recognition </a>to control devices like a lamp or fan.</li>
</ul>

<strong>Have suggestions or questions about the content of this post? Reach out at <a href="mailto:devrel@pubnub.com" target="_blank" rel="noopener" data-rawhref="mailto:devrel@pubnub.com">devrel@pubnub.com</a>.</strong>

<em>This is Part three of my Magic Leap series. Check out the other projects/posts <a href="https://www.pubnub.com/blog/getting-started-with-magic-leap-and-unity?devrel_gh=cube-fight" target="_blank" rel="noopener">Getting Started with Magic Leap and PubNub</a> and <a href="https://www.pubnub.com/blog/magic-leap-controlling-internet-connected-devices-lights-doors-with-hand-gestures/" target="_blank" rel="noopener">Controlling Internet-connected Devices with Magic Leap Hand Gestures</a>.</em>
