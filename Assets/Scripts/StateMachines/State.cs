
public abstract class State {
    protected Creature creature;
    protected ObjectController oc;
    public abstract void Tick();
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public virtual void OnAnimationEnd() { }
    public virtual void OnMovementEnd() { }

    public State(Creature _creature) {
        this.creature = _creature;
    }
    public State(ObjectController _oc) {
        this.oc = _oc;
    }
}
