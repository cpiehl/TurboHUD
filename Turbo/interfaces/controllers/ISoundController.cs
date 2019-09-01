using System.Media;

namespace Turbo.Plugins
{
    public enum VolumeMode { AutoMaster, AutoMasterAndEffects, Constant }

    public interface ISoundController
    {
        bool IsSpeakEnabled { get; set; }
        IWatch LastSpeak { get; }
        void Speak(string text);
        void StopSpeak();

        bool IsIngameSoundEnabled { get; }
        int IngameMasterVolume { get; }
        int IngameEffectsVolume { get; }

        VolumeMode VolumeMode { get; set; } // default VolumeMode.AutoMasterAndEffects

        // used when Mode == AutoMaster or Mode == AutoMasterAndEffects
        // AutoMaster final volume is calculated by this: (IngameMasterVolume / 100) * VolumeMultiplier
        // AutoMasterAndEffects final volume is calculated by this: (IngameMasterVolume / 100) * (IngameEffectsVolume / 100) * VolumeMultiplier
        double VolumeMultiplier { get; set; } // default 1.0

        // used when Mode == Constant
        int ConstantVolume { get; set; } // default 100. 0 = no sound, 100 = max sound

        SoundPlayer LoadSoundPlayer(string fileName); // filename is relative to the \sounds folder
    }
}