public class AtMenu : State {

    public AtMenu(Creature creature) : base(creature) { }

    public override void OnStateEnter() {
        creature.ma.Reveal();
        creature.anim.enabled = false;
    }

    public override void OnStateExit() {
        creature.anim.enabled = true;
    }

    public override void Tick() {
        if (creature.activeGame.Value == 1) creature.stateLock = false;
    }
}
