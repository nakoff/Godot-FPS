using System.Collections.Generic;

namespace Service;

public interface IInitService {
    void OnInit();
}

public interface IUpdateService {
    void OnUpdate(float dt);
}

public interface IPreUpdateService {
    void OnPreUpdate(float dt);
}

public interface IPostUpdateService {
    void OnPostUpdate(float dt);
}

public abstract class BaseService {
    private static List<IInitService> _inited = new List<IInitService>();
    private static List<IUpdateService> _updated = new List<IUpdateService>();
    private static List<IPreUpdateService> _preUpdated = new List<IPreUpdateService>();
    private static List<IPostUpdateService> _postUpdated = new List<IPostUpdateService>();

    public BaseService() {
        var inited = this as IInitService;
        var updated = this as IUpdateService;
        var preUpdated = this as IPreUpdateService;
        var postUpdated = this as IPostUpdateService;

        if (inited != null) BaseService._inited.Add(inited);
        if (updated != null) BaseService._updated.Add(updated);
        if (preUpdated != null) BaseService._preUpdated.Add(preUpdated);
        if (postUpdated != null) BaseService._postUpdated.Add(postUpdated);
    }

    public static void Init() {
        foreach (var inited in BaseService._inited) {
            inited.OnInit();
        }
    }

    public static void Update(float dt) {
        foreach (var updated in BaseService._preUpdated) {
            updated.OnPreUpdate(dt);
        }
        foreach (var updated in BaseService._updated) {
            updated.OnUpdate(dt);
        }
        foreach (var updated in BaseService._postUpdated) {
            updated.OnPostUpdate(dt);
        }
    }
}