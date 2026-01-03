public class InitialState : State
{
    private readonly SoundEffectManager _sound;
    public InitialState(DirectorScript director, State nextState, SoundEffectManager sound) : base(director, nextState)
    {
        _sound = sound;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnEntry()
    {
        base.OnEntry();
        _sound.PlaySong("Nature", 1f, "SpaceVoice", 0.25f);
    }
}
