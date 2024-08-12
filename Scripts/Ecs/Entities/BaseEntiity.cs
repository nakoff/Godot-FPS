using DefaultEcs;

public abstract class BaseEntity
{
    protected World world => Main.EcsWorld;

    protected Entity CreateEntity()
    {
        var entity = world.CreateEntity();
        return entity;
    }
}
