public abstract class EditorInvokable<T> {
    public virtual void Initialize (T Target) {}
    public virtual void Destroy (T Target) {}

    public abstract void DrawGizmos (T Target);
    public abstract void DrawInspectorGUI (T Target);
}
