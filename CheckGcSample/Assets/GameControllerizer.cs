using System.Runtime.InteropServices;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;
using UnityEditor;

[InputControlLayout(stateType = typeof(GcStateTypeInfo))]
#if UNITY_EDITOR
//UnityEditor起動時に構成を登録
[InitializeOnLoad]
#endif
public class GameControllerizer : Gamepad
{
    static GameControllerizer()
    {
        //レイアウト新規登録用
        InputSystem.RegisterLayout<GameControllerizer>(
            matches: new InputDeviceMatcher()
                .WithInterface("HID")
                .WithManufacturer("Hi Score Boys")
                .WithProduct("GameControllerizer")
                .WithVersion("1")
                .WithDeviceClass("Gamepad")
                .WithCapability("vendorId", 0x1FC9)
                .WithCapability("productId", 0x8214)
        );

        //既存の機器のレイアウト変更用
        InputSystem.RegisterLayoutMatcher<GameControllerizer>(
            new InputDeviceMatcher()
                .WithInterface("HID")
                .WithManufacturer("Hi Score Boys")
                .WithProduct("GameControllerizer")
        );
    }

    //登録が走ったことを確認
    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        UnityEngine.Debug.Log("GameControllerizer is initialized");
    }
}

// ***************************************************
//  GameControllerizer Firmware version <= 1
// ***************************************************
[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct GcStateTypeInfo : IInputStateTypeInfo
{
    public FourCC format => new FourCC('H', 'I', 'D');

    //LeftStick
    [InputControl(name = "leftStick", format = "VC2S", layout = "Stick")]
    [InputControl(name = "leftStick/x", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.0,normalizeMax=1.0,normalizeZero=0.5")]
    [InputControl(name = "leftStick/left", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.0,clampMax=0.5,invert")]
    [InputControl(name = "leftStick/right", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1.0")]
    [InputControl(name = "leftStick/y", offset = 1, format = "USHT", parameters = "invert,normalize,normalizeMin=0.0,normalizeMax=1.0,normalizeZero=0.5")]
    [InputControl(name = "leftStick/up", offset = 1, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.0,clampMax=0.5,invert")]
    [InputControl(name = "leftStick/down", offset = 1, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1.0,invert=false")]
    [FieldOffset(0)] public ushort leftStickX;
    [FieldOffset(1)] public ushort leftStickY;

    //RightStick
    [InputControl(name = "rightStick", format = "VC2S", layout = "Stick")]
    [InputControl(name = "rightStick/x", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.0,normalizeMax=1.0,normalizeZero=0.5")]
    [InputControl(name = "rightStick/left", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
    [InputControl(name = "rightStick/right", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
    [InputControl(name = "rightStick/y", offset = 2, format = "USHT", parameters = "invert,normalize,normalizeMin=0.0,normalizeMax=1.0,normalizeZero=0.5")]
    [InputControl(name = "rightStick/up", offset = 2, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.0,clampMax=0.5,invert")]
    [InputControl(name = "rightStick/down", offset = 2, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1.0,invert=false")]
    [FieldOffset(2)] public ushort rightStickX;
    [FieldOffset(3)] public ushort rightStickY;

    //Dpad
    [InputControl(name = "dpad", format = "BIT", layout = "Dpad", sizeInBits = 4, defaultState = 8)]
    [InputControl(name = "dpad/up", format = "BIT", layout = "DiscreteButton", parameters = "minValue=7,maxValue=1,nullValue=8,wrapAtValue=7", bit = 0, sizeInBits = 4)]
    [InputControl(name = "dpad/right", format = "BIT", layout = "DiscreteButton", parameters = "minValue=1,maxValue=3", bit = 0, sizeInBits = 4)]
    [InputControl(name = "dpad/down", format = "BIT", layout = "DiscreteButton", parameters = "minValue=3,maxValue=5", bit = 0, sizeInBits = 4)]
    [InputControl(name = "dpad/left", format = "BIT", layout = "DiscreteButton", parameters = "minValue=5,maxValue=7", bit = 0, sizeInBits = 4)]
    [InputControl(name = "buttonWest", bit = 4)]
    [InputControl(name = "buttonSouth", bit = 5)]
    [InputControl(name = "buttonEast", bit = 6)]
    [InputControl(name = "buttonNorth", bit = 7)]
    [FieldOffset(6)] public byte buttons0;

    //Button
    [InputControl(name = "leftShoulder", bit = 0)]
    [InputControl(name = "rightShoulder", bit = 1)]
    [InputControl(name = "leftTriggerButton", layout = "Button", bit = 2)]
    [InputControl(name = "rightTriggerButton", layout = "Button", bit = 3)]
    [InputControl(name = "select", bit = 4)]
    [InputControl(name = "start", bit = 5)]
    [InputControl(name = "leftStickPress", bit = 6)]
    [InputControl(name = "rightStickPress", bit = 7)]
    [FieldOffset(7)] public byte buttons1;
}

// ***************************************************
//  GameControllerizer Firmware version >= 2
// ***************************************************
// [StructLayout(LayoutKind.Explicit, Size = 7)]
// public struct GcStateTypeInfo : IInputStateTypeInfo
// {
//     public FourCC format => new FourCC('H', 'I', 'D');

//     //LeftStick
//     [InputControl(name = "leftStick", format = "VC2S", layout = "Stick")]
//     [InputControl(name = "leftStick/x", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.0,normalizeMax=1.0,normalizeZero=0.5")]
//     [InputControl(name = "leftStick/left", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.0,clampMax=0.5,invert")]
//     [InputControl(name = "leftStick/right", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1.0")]
//     [InputControl(name = "leftStick/y", offset = 1, format = "USHT", parameters = "invert,normalize,normalizeMin=0.0,normalizeMax=1.0,normalizeZero=0.5")]
//     [InputControl(name = "leftStick/up", offset = 1, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.0,clampMax=0.5,invert")]
//     [InputControl(name = "leftStick/down", offset = 1, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1.0,invert=false")]
//     [FieldOffset(0)] public ushort leftStickX;
//     [FieldOffset(1)] public ushort leftStickY;

//     //RightStick
//     [InputControl(name = "rightStick", format = "VC2S", layout = "Stick")]
//     [InputControl(name = "rightStick/x", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.0,normalizeMax=1.0,normalizeZero=0.5")]
//     [InputControl(name = "rightStick/left", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
//     [InputControl(name = "rightStick/right", offset = 0, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
//     [InputControl(name = "rightStick/y", offset = 1, format = "USHT", parameters = "invert,normalize,normalizeMin=0.0,normalizeMax=1.0,normalizeZero=0.5")]
//     [InputControl(name = "rightStick/up", offset = 1, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.0,clampMax=0.5,invert")]
//     [InputControl(name = "rightStick/down", offset = 1, format = "USHT", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1.0,invert=false")]
//     [FieldOffset(2)] public ushort rightStickX;
//     [FieldOffset(3)] public ushort rightStickY;

//     //Dpad
//     [InputControl(name = "dpad", format = "BIT", layout = "Dpad", sizeInBits = 4, defaultState = 8)]
//     [InputControl(name = "dpad/up", format = "BIT", layout = "DiscreteButton", parameters = "minValue=7,maxValue=1,nullValue=8,wrapAtValue=7", bit = 0, sizeInBits = 4)]
//     [InputControl(name = "dpad/right", format = "BIT", layout = "DiscreteButton", parameters = "minValue=1,maxValue=3", bit = 0, sizeInBits = 4)]
//     [InputControl(name = "dpad/down", format = "BIT", layout = "DiscreteButton", parameters = "minValue=3,maxValue=5", bit = 0, sizeInBits = 4)]
//     [InputControl(name = "dpad/left", format = "BIT", layout = "DiscreteButton", parameters = "minValue=5,maxValue=7", bit = 0, sizeInBits = 4)]
//     [InputControl(name = "buttonWest", bit = 4)]
//     [InputControl(name = "buttonSouth", bit = 5)]
//     [InputControl(name = "buttonEast", bit = 6)]
//     [InputControl(name = "buttonNorth", bit = 7)]
//     [FieldOffset(5)] public byte buttons0;

//     //Button
//     [InputControl(name = "leftShoulder", bit = 0)]
//     [InputControl(name = "rightShoulder", bit = 1)]
//     [InputControl(name = "leftTriggerButton", layout = "Button", bit = 2)]
//     [InputControl(name = "rightTriggerButton", layout = "Button", bit = 3)]
//     [InputControl(name = "select", bit = 4)]
//     [InputControl(name = "start", bit = 5)]
//     [InputControl(name = "leftStickPress", bit = 6)]
//     [InputControl(name = "rightStickPress", bit = 7)]
//     [FieldOffset(6)] public byte buttons1;
// }
